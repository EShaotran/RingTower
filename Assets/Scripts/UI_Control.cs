/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Control : MonoBehaviour {
	bool GameOn = false;
	int TapIconTimer; //0 to 35, 35 to 70, flashes the Tap Icon

	public GameObject TitleText;
	//public GameObject ShaotranText;
	public GameObject ScoreText;
	public GameObject TapImage;
	public GameObject HighScoreText;
	public GameObject CoinsText;


	void Start () {
		
	}

	void Update () {
		if (Input.GetButtonDown ("Jump") || Input.GetMouseButtonDown (0))
			GameOn = true;
			
		//Transition of Icons
		if (GameOn == true && ScoreText.GetComponent<RectTransform>().anchoredPosition.y > -90) { //-130 is target position of ScoreText
			TitleText.transform.position = new Vector3 (TitleText.transform.position.x, //Up
				TitleText.transform.position.y + (450 * Time.deltaTime),
				TitleText.transform.position.z);
		//	ShaotranText.transform.position = new Vector3 (ShaotranText.transform.position.x - (450 * Time.deltaTime), //Down
		//		ShaotranText.transform.position.y,
		//		ShaotranText.transform.position.z);
			ScoreText.transform.position = new Vector3 (ScoreText.transform.position.x, //To Left
				ScoreText.transform.position.y - (250 * Time.deltaTime),
				ScoreText.transform.position.z);
			HighScoreText.transform.position = new Vector3 (HighScoreText.transform.position.x, //To Left
				HighScoreText.transform.position.y - (150 * Time.deltaTime),
				HighScoreText.transform.position.z);
			CoinsText.transform.position = new Vector3 (CoinsText.transform.position.x, //To Left
				CoinsText.transform.position.y - (150 * Time.deltaTime),
				CoinsText.transform.position.z);
			TapImage.SetActive (false);
		}
	}

	public void StartGame() {
		GameOn = true;

	}
}

