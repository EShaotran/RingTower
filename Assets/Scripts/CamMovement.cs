/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using UnityEngine;
using System.Collections;

public class CamMovement : MonoBehaviour {

	int currentRing; //ring that the player is currently on
	public int RingSpace;
	public float moveSpeed;
	float startingYPos; //Y-interceptor... starting Y Position

	void Start () {
		startingYPos = gameObject.transform.position.y;
	}

	void Update () { //Difference? 1st one, if more than one Ring, moves the Ring up more (+ startingYPos-RingSpace)
		if ((currentRing > 0) && transform.position.y < ((currentRing+1) * RingSpace) + (startingYPos-RingSpace)) {
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y + (moveSpeed * Time.deltaTime),
				transform.position.z);
			
		} else if ((currentRing == 0) && transform.position.y < ((currentRing+1) * RingSpace)) { 
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y + (moveSpeed * Time.deltaTime),
				transform.position.z);

		}
	}

	public void Jumped() { //If the player successfully jumped, move the camera up.
		currentRing++;
	}
}
