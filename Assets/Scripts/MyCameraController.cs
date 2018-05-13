using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

	private GameObject unitychan;
	private float difference;
	GameObject pchan; 
	GameObject turnmanager;
	TurnManager TurnMscript;

	//☆################☆################  Start  ################☆################☆

	void Start () {

		this.unitychan = GameObject.Find ("unitychan");
		this.difference = unitychan.transform.position.z - this.transform.position.z;
		pchan = GameObject.Find ("pchan"); 
		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 

	}


	//####################################  Update  ###################################

	void Update () {

	}

	//####################################  other  ####################################

	void LateUpdate () {

		if(TurnMscript.canMove1P == true){
			//Unityちゃんの位置に合わせてカメラの位置を移動
			this.transform.position = new Vector3(this.unitychan.transform.position.x, this.transform.position.y, this.unitychan.transform.position.z-difference);
		}else if(TurnMscript.canMove2P == true){
			//Pちゃんの位置に合わせてカメラの位置を移動
			this.transform.position = new Vector3(pchan.transform.position.x, this.transform.position.y, pchan.transform.position.z-difference);
		}
	}

	//#################################################################################

}
// End
