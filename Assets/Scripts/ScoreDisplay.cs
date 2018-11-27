/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.name == "Score") 
			gameObject.GetComponent<Text> ().text = PlayerPrefs.GetInt ("Score") + "";
		if (gameObject.name == "HighScore") 
			gameObject.GetComponent<Text> ().text = PlayerPrefs.GetInt ("HighScore") + "";
		if (gameObject.name == "Coins")
			gameObject.GetComponent<Text> ().text = "$" + PlayerPrefs.GetInt ("Coins");
	}
}
