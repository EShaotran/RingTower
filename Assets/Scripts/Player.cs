/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float JumpSpeed;
	public float Gravity;

	private Vector3 _Move = Vector3.zero;
	private CharacterController _Controller;
	GameObject SpawnerGO;
	GameObject cam;
	GameObject DeathCanvas;
	GameObject InstructionText;
	public bool inCollision;
	bool dead = false; //Used for seeing if a coin is valid (only valid if player is still alive)

	void Awake() {
		PlayerPrefs.SetInt ("Score", 0);
	}

	private void Start()
	{
		_Controller = GetComponent<CharacterController>();
		cam = GameObject.Find ("Main Camera");
		SpawnerGO = GameObject.Find ("Spawn");
		DeathCanvas = GameObject.Find ("DeathCanvas");
		InstructionText = GameObject.Find ("InstructionalText");
		InstructionText.SetActive (false);
		inCollision = false;
		PlayerPrefs.SetString ("Death", "False");
		dead = false;
	}
		

	void Update()
	{

		if ((Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown(0)) && PlayerPrefs.GetInt("TutorialTap") == 1) 
			inCollision = false; //Reset Collision variable

		
		if (_Controller.isGrounded)
		{
			_Move = Vector3.zero;

			if ((Input.GetButton ("Jump") || Input.GetMouseButtonDown(0)) && PlayerPrefs.GetInt("TutorialTap") == 1) 
				_Move.y = JumpSpeed;

		}
		else if ((_Controller.collisionFlags & CollisionFlags.Above) != 0)
			_Move.y = 0;
		else if ((_Controller.collisionFlags & CollisionFlags.Sides) != 0)
			Debug.Log("You Lose");

		_Move.y -= Gravity * Time.deltaTime;
		_Controller.Move(_Move * Time.deltaTime);


		//If there is a glitch in the game that makes player fall
		if (gameObject.transform.position.y < -8)
			KillPlayer ();


	}

	public void TutorialResumeJump () {
		inCollision = false; //Reset Collision variable
		_Move.y = JumpSpeed;
	}


	//Collisions:
	void OnCollisionEnter (Collision coll) {
		//Debug.Log ("Collision with " + coll.gameObject.name);
		if (coll.collider.tag == "Safe" && inCollision == false) { //Landed
			inCollision = true;
			PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + 1); //Score + 1
			SpawnerGO.GetComponent<RingSpawn> ().Spawn (); //Spawn New Ring
			GameObject.Find (PlayerPrefs.GetInt ("Score") + "").GetComponent<Ring> ().StopSpin (); //Stop Ring from Spinning
			cam.GetComponent<CamMovement> ().Jumped (); //Tell camera to move up

			//Tutorial Stuff:
			if (PlayerPrefs.GetInt ("New") == 0) {
				InstructionText.GetComponent<Text> ().text = "Great job! \n Keep it up!";
				Invoke ("HideInstructionText", 1);
				PlayerPrefs.SetInt ("New", 1);
			}
			GameObject.Find (PlayerPrefs.GetInt ("Score") + "").GetComponent<Ring> ().Bounce ();
		} else if (coll.collider.tag == "Death") { 
			Debug.Log ("Death collision with: " + coll.gameObject.name);
			KillPlayer ();
		} else if (coll.collider.tag == "Coin" && dead == false) {
			PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") + 1);
			Destroy(coll.gameObject);
		}
	}

	void KillPlayer () {
		dead = true;
		StartCoroutine(SplitMesh(true)); //PLAYER EXPLOSION ANIMATION

		if (PlayerPrefs.GetInt ("Score") > PlayerPrefs.GetInt ("HighScore"))
			PlayerPrefs.SetInt ("HighScore", PlayerPrefs.GetInt ("Score"));
		
		if (PlayerPrefs.GetInt ("Score") == 0) //If it's on first tap, auto-death (no delay for falling)
			DelayedDeathMessage ();
		else //Delay for falling
			Invoke ("DelayedDeathMessage", 1);
	}


	void DelayedDeathMessage () { //Shows the Message ~ seconds after death to show player falling
		DeathCanvas.GetComponent<DeathScreen> ().DeathAppear (); //Death Screen appears

	}

	void HideInstructionText () { //Inactivates InstructionText gameobject after a second (for tutorial)
		InstructionText.SetActive(false);
	}




	//KILL PLAYER MESH ANIMATION
	public IEnumerator SplitMesh (bool destroy)    {

		if(GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null) {
			yield return null;
		}

		if(GetComponent<Collider>()) {
			GetComponent<Collider>().enabled = false;
		}

		Mesh M = new Mesh();
		if(GetComponent<MeshFilter>()) {
			M = GetComponent<MeshFilter>().mesh;
		}
		else if(GetComponent<SkinnedMeshRenderer>()) {
			M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
		}

		Material[] materials = new Material[0];
		if(GetComponent<MeshRenderer>()) {
			materials = GetComponent<MeshRenderer>().materials;
		}
		else if(GetComponent<SkinnedMeshRenderer>()) {
			materials = GetComponent<SkinnedMeshRenderer>().materials;
		}

		Vector3[] verts = M.vertices;
		Vector3[] normals = M.normals;
		Vector2[] uvs = M.uv;
		for (int submesh = 0; submesh < M.subMeshCount; submesh++) {

			int[] indices = M.GetTriangles(submesh);

			for (int i = 0; i < indices.Length; i += 3)    {
				Vector3[] newVerts = new Vector3[3];
				Vector3[] newNormals = new Vector3[3];
				Vector2[] newUvs = new Vector2[3];
				for (int n = 0; n < 3; n++)    {
					int index = indices[i + n];
					newVerts[n] = verts[index];
					newUvs[n] = uvs[index];
					newNormals[n] = normals[index];
				}

				Mesh mesh = new Mesh();
				mesh.vertices = newVerts;
				mesh.normals = newNormals;
				mesh.uv = newUvs;

				mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

				GameObject GO = new GameObject("Triangle " + (i / 3));
				GO.layer = LayerMask.NameToLayer("Particle");
				GO.transform.position = transform.position;
				GO.transform.rotation = transform.rotation;
				GO.AddComponent<MeshRenderer>().material = materials[submesh];
				GO.AddComponent<MeshFilter>().mesh = mesh;
				GO.AddComponent<BoxCollider>();
				Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
				GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(300,500), explosionPos, 5);
				Destroy(GO, 5 + Random.Range(0.0f, 5.0f));
			}
		}
		GetComponent<Renderer>().enabled = false;

		yield return new WaitForSeconds(1.0f);
		if(destroy == true) {
			Destroy(gameObject);
		}
	}





}
