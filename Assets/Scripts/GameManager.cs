using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public AudioClip getKeySE;
	public AudioClip getTreasureSE;
	private AudioSource audioSource;

	public GameObject TreasurePrefab;
	public GameObject KeyPrefab;
	public Vector3 appearPosition;
	private int rndNum = 0;
	private int lastTimeNum = 0;

	GameObject Mewindow;
	public Text targetText; 

	//☆################☆################  Start  ################☆################☆

	void Start () {
		audioSource = this.gameObject.GetComponent<AudioSource> ();
		CreateKey ();

		Mewindow = GameObject.Find ("MeWindow");
		Mewindow.gameObject.SetActive (false);

	}


	//####################################  Update  ###################################

	void Update () {

	}

	//####################################  other  ####################################

	public void ActiveMewindow(){
		Mewindow.gameObject.SetActive(true);
	}

	public void InactiveMewindow(){
		Mewindow.gameObject.SetActive(false);
	}

	public void DisplayMessage(string sentence){
		this.targetText = Mewindow.GetComponentInChildren<Text>();
		this.targetText.text = sentence; 
	}

	public void CreateKey(){
		GameObject key = (GameObject)Instantiate (KeyPrefab);	
	}
		
	public void CreateTreasure(){
		GameObject treasure = (GameObject)Instantiate (TreasurePrefab);
	}

	public void GetKey(){
		audioSource.PlayOneShot (getKeySE);
	}

	public void GetTreasure(){
		audioSource.PlayOneShot (getTreasureSE);
	}


	public void getPositionInfo(){
		do {
			rndNum = Random.Range (1, 9);

			if (lastTimeNum != rndNum) {
				Debug.Log ("rndNum      :"+rndNum);
				Debug.Log ("lastTimeNum : " + lastTimeNum);

				switch (rndNum) {
				case 1:
					appearPosition = new Vector3 (-24, 0, 27);
					Debug.Log ("目指す場所はNORTH_L");
					break;

				case 2:
					appearPosition = new Vector3 (-3, 0, 27);
					Debug.Log ("目指す場所はNORTH_C");
					break;

				case 3:
					appearPosition = new Vector3 (21, 0, 27);
					Debug.Log ("目指す場所はNORTH_R");
					break;

				case 4:
					appearPosition = new Vector3 (-24, 0, 6);
					Debug.Log ("目指す場所はCENTER_L");
					break;

				case 5:
					appearPosition = new Vector3 (0, 0, 12);
					Debug.Log ("目指す場所はCENTER_C");
					break;

				case 6:
					appearPosition = new Vector3 (21, 0, 9);
					Debug.Log ("目指す場所はCENTER_R");
					break;

				case 7:
					appearPosition = new Vector3 (-24, 0, -24);
					Debug.Log ("目指す場所はSOUTH_L");
					break;

				case 8:
					appearPosition = new Vector3 (-6, 0, -9);
					Debug.Log ("目指す場所はSOUTH_C");
					break;

				case 9:
					appearPosition = new Vector3 (21, 0, -18);
					Debug.Log ("目指す場所はSOUTH_R");
					break;
				}
			}
		} while(lastTimeNum == rndNum);
		lastTimeNum = rndNum;
		Debug.Log ("lastTimeNum が " + lastTimeNum +"に上書きされます。");
	}

	//#################################################################################

}
// End