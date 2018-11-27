/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using UnityEngine;
using System.Collections;

public class RingSpawn : MonoBehaviour {

	//Ring Vars: ------------------------------
	public GameObject Ring;
	public float ringSpace; //space between rings (y-axis), also Number of Columns per Ring
	public int numRings; //number of rings
	public int rSpawn_Offset; //Offset so that the Ring can fall to the designated height
	private static float zOffset = 0.25f; //to correct rotation around 0,0,0
	public GameObject RingClone; //Stores access of each temporary ring

	//Ring Adjustment Rotation Vars: ------------------------------
	float prevCurrentRotation = 0; //Current Rotation of Previous Ring
	public int offset; //Space Player gets between the rings
	string prevDirection = "Left"; //Direction of previous ring
	public int selectionZone; //Number of degrees for randomization

	//NewRing-Specific of Above
	float newRotation; //The rotation the new Ring will be at
	string newDirection; //The direction the new Ring will be at

	//Process-Specific of Above
	float AngleA;
	float AngleB;
	int RandomNum;

	//Player Vars:------------------------------
	public GameObject PlayerPref;
	public GameObject MusicPlayer;

	//Color Editor Vars:
	public float time = 1f;
	private HSBColor hsbc; //Rings

	//Column Vars:
	private HSBColor hsbc_columns; //Columns
	public GameObject columnPiece;
	public GameObject ColumnClone; //Stores access of each temporary column
	int numColumns;

	//Starting Colors of Columns:
	public Color Level1; //0-10
	public Color Level2; //10-20
	public Color Level3; //20-30
	public Color Level4; //30-40
	public Color Level5; //40-50
	public Color GodLevel; //50+

	//Colors of Rings:
	public Color Ring1;
	public Color Ring2;
	public Color Ring3;
	public Color Ring4;
	public Color Ring5;
	public Color RingGod;

	//Skyboxes:
	public Material Skybox1;
	public Material Skybox2;
	public Material Skybox3;
	public Material Skybox4;
	public Material Skybox5;
	public Material SkyboxGod;

	//Player Colors:
	public Material BASEMAT;
	public Material REDMAT;
	public Material LBMAT;
	public Material BLUEMAT;
	public Material PURPLEMAT;
	public Material GREENMAT;
	public Material SUNSETMAT;
	public Material SALMONPINKMAT;
	public Material STRANGEPURPLEMAT;


	void Start () {
		//Reset Skybox Blends:
		Skybox1.SetFloat("_Blend", 0);
		Skybox2.SetFloat("_Blend", 0);
		Skybox3.SetFloat("_Blend", 0);
		Skybox4.SetFloat("_Blend", 0);


		Application.targetFrameRate = 80;
		numRings = 0;

		hsbc = HSBColor.FromColor (Ring1); //Color Editor - Rings
		hsbc_columns = HSBColor.FromColor (Level1); //Color Editor - Columns

		//Instantiate Player:
		GameObject playerobj = Instantiate (PlayerPref,
			new Vector3 (0, 2, -5.8f),
			Quaternion.identity);
		playerobj.GetComponent<MeshRenderer> ().material = SetMyColor ();


		Spawn ();
		Spawn ();
		//Spawn ();
		//Spawn ();

		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns (); 

		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns (); 

		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns (); 

		//ADD MUSIC: <If no music gameobject already exists>
		if (GameObject.Find ("MusicPlayer(Clone)") == null) {
			GameObject musicobj = Instantiate (MusicPlayer,
				                     new Vector3 (0, 0, 0),
				                     Quaternion.identity);
			DontDestroyOnLoad (musicobj);
		}

	}

	public void Spawn () {
		
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns ();
		SpawnColumns (); 


		//Set Variables:
		if (numRings > 0) {
			prevCurrentRotation = GameObject.Find (numRings + "").transform.eulerAngles.y;
			prevDirection = GameObject.Find (numRings + "").GetComponent<Ring> ().ROL;
		}

		//Randomize Direction:
		RandomNum = Random.Range (1, 3);
		if (RandomNum == 1)
			newDirection = "Right";
		else
			newDirection = "Left";

		//Process:
		if (prevDirection == "Right" && newDirection == "Right") {
			AngleA = (prevCurrentRotation - offset + 360) % 360;
			AngleB = (AngleA - selectionZone + 360) % 360;
			if (AngleA > AngleB) //Normal Situation
				newRotation = Random.Range (AngleA, AngleB);
			else {
				do
					newRotation = Random.Range (0, 360);
				while (newRotation < AngleB && newRotation > AngleA);
			}
		} else if (prevDirection == "Right" && newDirection == "Left") {
			AngleA = ((360 - prevCurrentRotation) + offset + 360) % 360;
			AngleB = (AngleA + selectionZone + 360) % 360;
			if (AngleB > AngleA) //Normal Situation
				newRotation = Random.Range (AngleB, AngleA);
			else {
				do
					newRotation = Random.Range (0, 360);
				while (newRotation < AngleA && newRotation > AngleB);
			}
		} else if (prevDirection == "Left" && newDirection == "Right") {
			AngleA = ((360 - prevCurrentRotation) - offset + 360) % 360;
			AngleB = (AngleA - selectionZone + 360) % 360;
			if (AngleA > AngleB) //Normal Situation
				newRotation = Random.Range (AngleA, AngleB);
			else {
				do
					newRotation = Random.Range (0, 360);
				while (newRotation < AngleB && newRotation > AngleA);
			}
		} else if (prevDirection == "Left" && newDirection == "Left") {
			AngleA = (prevCurrentRotation + offset + 360) % 360;
			AngleB = (AngleA + selectionZone + 360) % 360;
			if (AngleB > AngleA) //Normal Situation
				newRotation = Random.Range (AngleB, AngleA);
			else {
				do
					newRotation = Random.Range (0, 360);
				while (newRotation < AngleA && newRotation > AngleB);
			}
		}

		RingClone = GameObject.Instantiate (Ring, 
			new Vector3 (0, ((numRings + 1) * ringSpace) + rSpawn_Offset, zOffset), 
			Quaternion.Euler (270, 0, 0)) as GameObject;
		RingClone.transform.RotateAround (Vector3.zero, Vector3.up, newRotation); //Sets Z position of Ring
		RingClone.GetComponent<Ring> ().ROL = newDirection;
		numRings++;
		RingClone.name = numRings + "";

		CheckLevel (); //Used for Speed and Colors

		//Set Speed:
		switch (PlayerPrefs.GetInt ("RingLevel")) {
		case 1:
			RingClone.GetComponent<Ring> ().speed = Random.Range (1.1f, 1.4f);
			break;
		case 2:
			RingClone.GetComponent<Ring> ().speed = Random.Range (1.2f, 1.4f);
			break;
		case 3:
			RingClone.GetComponent<Ring> ().speed = Random.Range (1.3f, 1.5f);
			break;
		case 4:
			RingClone.GetComponent<Ring> ().speed = Random.Range (1.4f, 1.5f);
			break;
		case 5:
			RingClone.GetComponent<Ring> ().speed = Random.Range (1.5f, 1.6f);
			break;
		case 6:
			RingClone.GetComponent<Ring> ().speed = Random.Range (1.6f, 1.8f);
			break;
		}



		//COLOR EDITOR FOR RING
		switch (PlayerPrefs.GetInt ("RingLevel")) {
		case 1:
			hsbc = HSBColor.FromColor (Ring1);
			break;
		case 2:
			hsbc = HSBColor.FromColor (Ring2);
			break;
		case 3:
			hsbc = HSBColor.FromColor (Ring3);
			break;
		case 4:
			hsbc = HSBColor.FromColor (Ring4);
			break;
		case 5:
			hsbc = HSBColor.FromColor (Ring5);
			break;
		case 6:
			hsbc = HSBColor.FromColor (RingGod);
			break;
		}

		SetSkybox ();

		//Set the color
		RingClone.transform.Find("MainRing").GetComponent<Renderer>().material.color = HSBColor.ToColor (hsbc); //Places color on Ring
		RingClone.transform.Find("RingSafe").GetComponent<Renderer>().material.color = HSBColor.ToColor (hsbc); //Places color on Ring
		RingClone.transform.Find("Left_Dead").GetComponent<Renderer>().material.color = HSBColor.ToColor (hsbc); //Places color on Ring
		RingClone.transform.Find("Right_Dead").GetComponent<Renderer>().material.color = HSBColor.ToColor (hsbc); //Places color on Ring

	}


	public void SpawnColumns () {
		ColumnClone = GameObject.Instantiate (columnPiece, 
			new Vector3 (0,(numColumns+1),-0.22f), 
			Quaternion.Euler (0, numColumns * 5, 0) ) as GameObject;
		numColumns++;

		//Color Editor:

		//Switches base color gradient at beginning of each level. Level 1 already set at Start();
		switch (PlayerPrefs.GetInt("Score")) { //numRings
		case 7: hsbc_columns = HSBColor.FromColor (Level2); //Start of Level 2 - 11-3
			break;
		case 17: hsbc_columns = HSBColor.FromColor (Level3); //Start of Level 3
			break;
		case 27: hsbc_columns = HSBColor.FromColor (Level4); //Start of Level 4
			break;
		case 37: hsbc_columns = HSBColor.FromColor (Level5); //Start of Level 5
			break;
		case 47: hsbc_columns = HSBColor.FromColor (GodLevel); //Start of Level God - 51-3
			break;
		}

		hsbc_columns.h = (hsbc_columns.h + ((Time.deltaTime*4f / time)/30)) % 1.0f; //Mutates color slightly
		ColumnClone.GetComponent<Renderer>().material.color = HSBColor.ToColor(hsbc_columns);

	}

	void CheckLevel () { //IMPORTANT: Level in perspective of total rings, not player!
		if (numRings < 11) {
			PlayerPrefs.SetInt ("RingLevel", 1);
		} else if (numRings < 21) {
			PlayerPrefs.SetInt ("RingLevel", 2);
		} else if (numRings < 31) {
			PlayerPrefs.SetInt ("RingLevel", 3);
		} else if (numRings < 41) {
			PlayerPrefs.SetInt ("RingLevel", 4);
		} else if (numRings < 51) {
			PlayerPrefs.SetInt ("RingLevel", 5);
		} else if (numRings < 61) {
			PlayerPrefs.SetInt ("RingLevel", 6); //GOD
		}

	}

	void SetSkybox () {
		switch (PlayerPrefs.GetInt ("Score")) {
		case 1:
			RenderSettings.skybox = Skybox1;
			break;
		case 11:
			RenderSettings.skybox = Skybox2;
			break;
		case 21:
			RenderSettings.skybox = Skybox3;
			break;
		case 31:
			RenderSettings.skybox = Skybox4;
			break;
		case 41:
			RenderSettings.skybox = Skybox5;
			break;
		case 51:
			RenderSettings.skybox = SkyboxGod;
			break;
		}
	}

	void BlendSkybox () { //Simply increases the Blend() variable of the current skybox if needed
		float currBlend = RenderSettings.skybox.GetFloat ("_Blend");
		if (currBlend < 1)
			RenderSettings.skybox.SetFloat ("_Blend", currBlend+0.01f);
	}

	void Update () {
		if (RenderSettings.skybox != Skybox1 && RenderSettings.skybox != SkyboxGod)
			BlendSkybox ();


	}


	Material SetMyColor () {
		Material myColor = BASEMAT;
		switch (PlayerPrefs.GetString ("Color")) {
		case "Base":
			myColor = BASEMAT;
			break;
		case "Red":
			myColor = REDMAT;
			break;
		case "LightBlue":
			myColor = LBMAT;
			break;
		case "Blue":
			myColor = BLUEMAT;
			break;
		case "Purple":
			myColor = PURPLEMAT;
			break;
		case "Green":
			myColor = GREENMAT;
			break;
		case "Sunset":
			myColor = SUNSETMAT;
			break;
		case "SalmonPink":
			myColor = SALMONPINKMAT;
			break;
		case "StrangePurple":
			myColor = STRANGEPURPLEMAT;
			break;

		}

		return myColor;

	}


}

