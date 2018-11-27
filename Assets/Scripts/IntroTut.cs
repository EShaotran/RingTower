/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTut : MonoBehaviour {
	string RoL; //Is first ring going to be on the player's Right or Left side?
	GameObject FirstRing;
	GameObject Player;
	public GameObject InstructionText;
	public float NumBeforeJumping; //What's the distance(angle) of rotation when the player should jump?
	public bool ActivatedYet = false;

	//Very beginning (freeze-frame, Tap To Begin)
	public GameObject TapToBeginText;

	void Start () {
		if (PlayerPrefs.GetInt ("New") == 0) { //If it's an Intro Tap
			TapToBeginText.SetActive(true);
			//Freeze both rings
			Invoke ("FreezeBothRings", 0.2f);

		}
	}

	void ScreenTapped () {
		if (PlayerPrefs.GetInt ("New") == 0) { //If it's an Intro Tap
			GameObject.Find ("1").GetComponent<Ring> ().UnfreezeRing ();
			GameObject.Find ("2").GetComponent<Ring> ().UnfreezeRing ();
			TapToBeginText.SetActive(false);
			Invoke ("FindFirstRing", 0.1f);

		}
	}



	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown (0)) {
			ScreenTapped ();
		}
			

		if (PlayerPrefs.GetInt ("New") == 0) {
			if (Mathf.Abs (FirstRing.transform.rotation.z * 360) < NumBeforeJumping && ActivatedYet == false) { //Time to freeze the frame
				FirstRing.GetComponent<Ring>().FreezeRing();


				if (RoL == "Right") {
					gameObject.GetComponent<Animator> ().SetTrigger ("Right");
					InstructionText.SetActive (true);
					InstructionText.GetComponent<Animator> ().SetTrigger ("Right");
				} else {
					gameObject.GetComponent<Animator> ().SetTrigger ("Left");
					InstructionText.SetActive (true);
					InstructionText.GetComponent<Animator> ().SetTrigger ("Left");
				}
			
				if (Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown (0)) {
					PlayerPrefs.SetInt ("TutorialTap", 1);
					ActivatedYet = true;
					FirstRing.GetComponent<Ring>().UnfreezeRing();
					Player.GetComponent<Player> ().TutorialResumeJump ();
					gameObject.SetActive (false);
					//Continued on the Player class -- when lands, "Congratulations Message"
				}
			}

		}
	}

	void FreezeBothRings () {
		GameObject.Find ("1").GetComponent<Ring> ().FreezeRing ();
		GameObject.Find ("2").GetComponent<Ring> ().FreezeRing ();
	}

	void FindFirstRing () { //Delayed because spawning of the ring takes some time
		FirstRing = GameObject.Find ("1");
		RoL = FirstRing.GetComponent<Ring> ().ROL;

		Player = GameObject.Find ("PlayerNormal(Clone)");
	}
}
