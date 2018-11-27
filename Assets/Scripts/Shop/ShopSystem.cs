/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopSystem : MonoBehaviour {

	public GameObject MPText; //MultiPurposeText
	public GameObject MPButton; //MultiPurposeButton
	public GameObject CoinText; //CoinText

	public Material BASE;
	public Material RED;
	public Material LIGHTBLUE;
	public Material BLUE;
	public Material PURPLE;
	public Material GREEN;
	public Material SUNSET;
	public Material SALMONPINK;
	public Material STRANGEPURPLE;

	int REDPRICE = 30;
	int LBPRICE = 50;
	int BLUEPRICE = 30;
	int PURPLEPRICE = 50;
	int GREENPRICE = 20;
	int SUNSETPRICE = 80;
	int SALMONPINKPRICE = 80;
	int STRANGEPURPLEPRICE = 100;

	int ButtonState = 0;
	string CurrentSelectedColor;

	void Start () {
		//Button Not Showing:
		MPButton.SetActive(false);


		switch (PlayerPrefs.GetString ("Color")) {
		case "Base":
			gameObject.GetComponent<MeshRenderer> ().material = BASE;
			break;
		case "Red":
			gameObject.GetComponent<MeshRenderer> ().material = RED;
			break;
		case "LightBlue":
			gameObject.GetComponent<MeshRenderer> ().material = LIGHTBLUE;
			break;
		case "Blue":
			gameObject.GetComponent<MeshRenderer> ().material = BLUE;
			break;
		case "Purple":
			gameObject.GetComponent<MeshRenderer> ().material = PURPLE;
			break;
		case "Green":
			gameObject.GetComponent<MeshRenderer> ().material = GREEN;
			break;
		case "Sunset":
			gameObject.GetComponent<MeshRenderer> ().material = SUNSET;
			break;
		case "SalmonPink":
			gameObject.GetComponent<MeshRenderer> ().material = SALMONPINK;
			break;
		case "StrangePurple":
			gameObject.GetComponent<MeshRenderer> ().material = STRANGEPURPLE;
			break;
		}
	}
	
	void Update () {
		gameObject.transform.RotateAround (transform.position, Vector3.up, 1);
		CoinText.GetComponent<Text> ().text = "" + PlayerPrefs.GetInt ("Coins");
	}

	public void DemoColor (string col) {
		CurrentSelectedColor = col;

		//Set Demo Color
		switch (col) {
		case "Base":
			gameObject.GetComponent<MeshRenderer> ().material = BASE;
			break;
		case "Red":
			gameObject.GetComponent<MeshRenderer> ().material = RED;
			break;
		case "LightBlue":
			gameObject.GetComponent<MeshRenderer> ().material = LIGHTBLUE;
			break;
		case "Blue":
			gameObject.GetComponent<MeshRenderer> ().material = BLUE;
			break;
		case "Purple":
			gameObject.GetComponent<MeshRenderer> ().material = PURPLE;
			break;
		case "Green":
			gameObject.GetComponent<MeshRenderer> ().material = GREEN;
			break;
		case "Sunset":
			gameObject.GetComponent<MeshRenderer> ().material = SUNSET;
			break;
		case "SalmonPink":
			gameObject.GetComponent<MeshRenderer> ().material = SALMONPINK;
			break;
		case "StrangePurple":
			gameObject.GetComponent<MeshRenderer> ().material = STRANGEPURPLE;
			break;
		}


		//Set Button State
		MPButton.SetActive(true);
		if (PlayerPrefs.GetInt (col + "Unlocked") == 0) { //Locked
			ButtonState = 1;
			MPText.GetComponent<Text> ().text = "$" + ColorToPrice (col);
		} else if (PlayerPrefs.GetString ("Color") == col) { //==1, Unlocked AND COLOR CURRENTLY EQUIPPED
			ButtonState = 2;
			MPText.GetComponent<Text> ().text = "Equipped";
		} else { //==1, Unlocked AND COLOR NOT EQUIPPED CURRENTLY
			ButtonState = 3;
			MPText.GetComponent<Text> ().text = "Equip";
		}
			
	}

	public void MPButtonClicked () { //Multipurpose Button Clicked
		if (ButtonState == 1) { //Color Currently Locked => Buy It
			if (PlayerPrefs.GetInt ("Coins") >= ColorToPrice (CurrentSelectedColor)) { //If can afford
				PlayerPrefs.SetInt(CurrentSelectedColor + "Unlocked", 1);
				PlayerPrefs.SetInt ("Coins", PlayerPrefs.GetInt ("Coins") - ColorToPrice (CurrentSelectedColor));
				MPText.GetComponent<Text> ().text = "Equip";
				ButtonState = 3;
			} else {
				MPText.GetComponent<Text> ().text = "Not Enough $";
			}
		} else if (ButtonState == 2) { //Color Unlocked and Equipped => Do Nothing
			//DO NOTHING
		} else if (ButtonState == 3) { //Color Unlocked and Unequipped => Equip the Color
			PlayerPrefs.SetString("Color", CurrentSelectedColor);
			MPText.GetComponent<Text> ().text = "Equipped";
			ButtonState = 2;
		}


	}


	int ColorToPrice(string col) {
		int price = 0;
		switch (col) {
		case "Red":
			price = REDPRICE;
			break;
		case "LightBlue":
			price = LBPRICE;
			break;
		case "Blue":
			price = BLUEPRICE;
			break;
		case "Purple":
			price = PURPLEPRICE;
			break;
		case "Green":
			price = GREENPRICE;
			break;
		case "Sunset":
			price = SUNSETPRICE;
			break;
		case "SalmonPink":
			price = SALMONPINKPRICE;
			break;
		case "StrangePurple":
			price = STRANGEPURPLEPRICE;
			break;
		} 

		return price;
	}
		

	//UI Elements:
	public void ExitShop () {
		SceneManager.LoadScene ("Game");
	}




}
