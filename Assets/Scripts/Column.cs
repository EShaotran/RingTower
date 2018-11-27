/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour {

	float YPos;
//	bool playerDead = false; //Player dead?
//	int deadTimer; //Timer for falling, before destroy gameObject
//	public int fallSpeed;

	void Start () {
		YPos = gameObject.transform.position.y;
	}

	void Update () {
		if ((PlayerPrefs.GetInt ("Score") * 5 - 15) > YPos) {
			Destroy (this.gameObject);
		}

		/*
		if (PlayerPrefs.GetString ("Death") == "True")
			playerDead = true;

		if (playerDead == true) {
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y - (fallSpeed * Time.deltaTime),
				transform.position.z);
			deadTimer++;
			if (deadTimer > 200)
				Destroy (this.gameObject);
		}
		*/
	}


}
