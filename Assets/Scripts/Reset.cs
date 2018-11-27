/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {

	void Start () {
		PlayerPrefs.SetInt ("HighScore", 0);
		PlayerPrefs.SetInt ("New", 0); //New Player? Used for NotificationSystem and for all of Tutorial
		PlayerPrefs.SetInt ("TutorialTap", 0); //Used to differentiate Tutorial "Tap To Begin" (0) and regular Taps (1)
		PlayerPrefs.SetString ("Color", "Base"); //Color is currently the base color

		PlayerPrefs.SetInt ("Coins", 10000);
		PlayerPrefs.SetInt ("RedUnlocked", 0); //0=Locked, 1=Unlocked
		PlayerPrefs.SetInt ("LightBlueUnlocked", 0);
		PlayerPrefs.SetInt ("BlueUnlocked", 0);
		PlayerPrefs.SetInt ("PurpleUnlocked", 0);
		PlayerPrefs.SetInt ("GreenUnlocked", 0);
		PlayerPrefs.SetInt ("SunsetUnlocked", 0);
	}
	
	void Update () {
		
	}
}
