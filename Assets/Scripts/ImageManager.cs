using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {

	public Sprite uchan50;
	public Sprite pchan50;

	private GameObject obj;
	private Image FaceImage;
	GameObject turnmanager;
	TurnManager TurnMscript;

	//☆################☆################  Start  ################☆################☆

	void Start () {
		obj = GameObject.Find("charaFace").gameObject as GameObject;
		FaceImage = obj.GetComponent<Image> ();

		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 
	}


	//####################################  Update  ###################################

	void Update () {


	}

	//####################################  other  ####################################

	public void ChangeFaceImage(){

		if (TurnMscript.canMove2P == true) {
			FaceImage.sprite = pchan50;
		}

		if (TurnMscript.canMove1P == true) {
			FaceImage.sprite = uchan50;
		}
	}

	//#################################################################################

}
// End