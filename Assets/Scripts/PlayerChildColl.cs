/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerChildColl : MonoBehaviour {
	bool inCollision = false;
	GameObject cam;
	GameObject SpawnerGO;

	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera");
		SpawnerGO = GameObject.Find ("Spawn");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Collisions:
	void OnCollisionEnter (Collision coll) {
		//Debug.Log ("Collision with " + coll.gameObject.name);
		if (coll.collider.tag == "Death") { 
			//Death (coll.gameObject.name);
			Debug.Log ("Death collision with: " + coll.gameObject.name);
			SceneManager.LoadScene ("Game");
		} else if (coll.collider.tag == "Safe" && inCollision == false) { //Landed
			inCollision = true;
			PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") +1); //Score + 1
			SpawnerGO.GetComponent<RingSpawn> ().Spawn (); //Spawn New Ring
			GameObject.Find(PlayerPrefs.GetInt("Score") + "").GetComponent<Ring>().StopSpin(); //Stop Ring from Spinning
			cam.GetComponent<CamMovement>().Jumped(); //Tell camera to move up
		}
	}
}
