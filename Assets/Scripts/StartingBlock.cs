/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBlock : MonoBehaviour {

	public float fallSpeed;
	int deathFallTimer;
	bool dead = false; //Is the player no longer on the block?

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerPrefs.GetInt ("Score") > 0)
			dead = true;

		if (dead == true) {
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y - (fallSpeed * Time.deltaTime),
				transform.position.z);
			deathFallTimer++;
			if (deathFallTimer > 100)
				Destroy (this.gameObject);
		}
		
	}
}
