/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {

	public string ROL; //Right Or Left rotation?
	public float speed;
	public bool spin = true;
	public float ringSpace;
	public int fallSpeed;
	public int COINSPAWNRATE_1_OF; //1 out of _____ ?
	bool dead = false; //Should the ring be dead? Is the player past this ring?


	bool HighScoreRing; //Is this ring the "HighScore" ring?
	public MeshRenderer childOne;
	public MeshRenderer childTwo;
	public MeshRenderer childThree;
	public MeshRenderer childFour;

	public GameObject Fireworks;
	GameObject cam;
	int HighScoreTrigger = 0; //0=False, 1=Triggered


	float targetYPos;
	int RingNum;

	public int deathFallTimer; //When it is no longer needed, timer counts up before deleting GameObject
	public bool Freeze = false;

	float RingDip = 0; //0: Not Dipping (default), 1-50 down, 50-100 up


	void Start () {
		cam = GameObject.Find ("Main Camera");
		RingNum = int.Parse (gameObject.name);
		targetYPos = RingNum * ringSpace;

		if (RingNum == PlayerPrefs.GetInt ("HighScore") + 1) 
			HighScoreRing = true;

		//Decide if have a coin or not
		if (RingNum % COINSPAWNRATE_1_OF != 0) //Not supposed to have coin:
			Destroy(gameObject.transform.GetChild(4).gameObject); //Destroy coin
	}

	void Update () {

	}


	void LateUpdate () {


		if (HighScoreRing == true && PlayerPrefs.GetInt("HighScore") != 0 && PlayerPrefs.GetInt("Score") != RingNum) {
			childOne.material.color = Color.Lerp (Color.white, Color.black, Mathf.PingPong (Time.time, 1));
			childTwo.material.color = Color.Lerp (Color.white, Color.black, Mathf.PingPong (Time.time, 1));
			childThree.material.color = Color.Lerp (Color.white, Color.black, Mathf.PingPong (Time.time, 1));
			childFour.material.color = Color.Lerp (Color.white, Color.black, Mathf.PingPong (Time.time, 1));
		}

		if (HighScoreRing == true && PlayerPrefs.GetInt("HighScore") != 0 && PlayerPrefs.GetInt("Score") == RingNum) { //Player lands on HighScore Ring
			childOne.material.color = Color.Lerp (Color.magenta, Color.yellow, Mathf.PingPong (Time.time, 1));
			childTwo.material.color = Color.Lerp (Color.magenta, Color.yellow, Mathf.PingPong (Time.time, 1));
			childThree.material.color = Color.Lerp (Color.magenta, Color.yellow, Mathf.PingPong (Time.time, 1));
			childFour.material.color = Color.Lerp (Color.magenta, Color.yellow, Mathf.PingPong (Time.time, 1));

			//FIREWORKS
			if (HighScoreTrigger == 0) {
				Instantiate (Fireworks,
					new Vector3 (0, cam.transform.position.y - 21, 0),
					Quaternion.Euler(-90,0,0));
				HighScoreTrigger = 1; //Prevents Fireworks from re-spawning, only spawns once
			}
		}

		//Movement:
		if (spin == true && Freeze == false) {
			if (ROL == "Right") { //Right
				gameObject.transform.RotateAround (Vector3.zero, Vector3.up, speed);
			} else { //Left
				gameObject.transform.RotateAround (Vector3.zero, Vector3.down, speed);
			}
		}


		//Fallng to Position:
		if (targetYPos < gameObject.transform.position.y) { //If targetPos < currentPos, continue moving down
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y - (fallSpeed * Time.deltaTime),
				transform.position.z);
		}

		//If player is on this ring, and space is pressed, ring is dead.
		if (RingNum == PlayerPrefs.GetInt ("Score") && (Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown(0))) {
			dead = true;
		}
			

		//Falling To Death;
		if ((RingNum <= PlayerPrefs.GetInt ("Score")) && dead == true) {
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y - (fallSpeed * Time.deltaTime),
				transform.position.z);
			deathFallTimer++;
			if (deathFallTimer > 55)
				Destroy (this.gameObject);
		}



		//LAND DIP: -- when player lands on Ring, it should dip a bit
		if (RingDip >= 1) 
			RingDip += 5f; //200 frame dip
		

		//NOTE: We have added 5 to give the game time to let the player jump first
		if (RingDip >= 5 && RingDip <= 45) //1-40 (incl)
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.03f, transform.position.z);
		else if (RingDip > 45 && RingDip <= 55) // (excl) 40-50 (incl)
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f, transform.position.z);
		else if (RingDip > 55 && RingDip < 65) // (excl) 50-60 (excl)
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f, transform.position.z);
		else if (RingDip >= 65 && RingDip <= 104) // (incl) 60-99 (incl)
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.03f, transform.position.z);
		/*
		else if (RingDip >= 100 && RingDip <= 114) // (incl) 60-114 (incl)
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f, transform.position.z);
		else if (RingDip > 115 && RingDip <= 129) // (excl) 40-50 (incl)
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f, transform.position.z);
		*/

		if (RingDip == 100) 
			RingDip = 0;
		
		

	}
		

	public void StopSpin () { //When player lands on ring, it should stop spinning
		spin = false;
	}

	public void FreezeRing () {
		Freeze = true;
	}

	public void UnfreezeRing () {
		Freeze = false;
	}

	public void Bounce () { //Player sends this to the ring
		RingDip = 1;
	}

}
