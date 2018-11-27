/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour {

	Animator anim;
	public GameObject DeathScore;
	public GameObject HighScoreText;
	public GameObject NextFlap;
	public GameObject RateFlap;

	public Color Lev1;
	public Color Lev2;
	public Color Lev3;
	public Color Lev4;
	public Color Lev5;
	public Color LevGod;

	public Color Rate1;
	public Color Rate2;
	public Color Rate3;
	public Color Rate4;
	public Color Rate5;
	public Color RateGod;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void Replay () {
		SceneManager.LoadScene ("Game");
	}

	public void Rate () {
		Application.OpenURL ("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?" +
			"pageNumber=0&sortOrdering=1&type=Purple+Software&mt=8&id=1236587360");
	}

	public void Store () {
		SceneManager.LoadScene ("Store");
	}

	public void DeathAppear () {
		DeathScore.GetComponent<Text> ().text = PlayerPrefs.GetInt ("Score") + "";

		if (PlayerPrefs.GetInt ("Score") == PlayerPrefs.GetInt ("HighScore")) //Is this a new highscore?
			HighScoreText.GetComponent<Text> ().text = "New Highscore!";
		else
			HighScoreText.GetComponent<Text> ().text = "highscore " + PlayerPrefs.GetInt ("HighScore");

		switch (PlayerPrefs.GetInt ("RingLevel")) {
		case 1:
			NextFlap.GetComponent<Image> ().color = Lev1;
			RateFlap.GetComponent<Image> ().color = Rate1;
			break;
		case 2:
			NextFlap.GetComponent<Image> ().color = Lev2;
			RateFlap.GetComponent<Image> ().color = Rate2;
			break;
		case 3:
			NextFlap.GetComponent<Image> ().color = Lev3;
			RateFlap.GetComponent<Image> ().color = Rate3;
			break;
		case 4:
			NextFlap.GetComponent<Image> ().color = Lev4;
			RateFlap.GetComponent<Image> ().color = Rate4;
			break;
		case 5:
			NextFlap.GetComponent<Image> ().color = Lev5;
			RateFlap.GetComponent<Image> ().color = Rate5;
			break;
		case 6:
			NextFlap.GetComponent<Image> ().color = LevGod;
			RateFlap.GetComponent<Image> ().color = RateGod;
			break;
		}

		//Play "Appear" animation
		anim.SetTrigger ("Death");
	}
}
