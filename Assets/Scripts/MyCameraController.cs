using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

	private GameObject unitychan;
	private float difference;

	//☆################☆################  Start  ################☆################☆

	void Start () {
		
		this.unitychan = GameObject.Find ("unitychan");
		this.difference = unitychan.transform.position.z - this.transform.position.z;

	}


	//####################################  Update  ###################################

	void Update () {

	}

	//####################################  other  ####################################

	void LateUpdate () {
		//Unityちゃんの位置に合わせてカメラの位置を移動
		this.transform.position = new Vector3(this.unitychan.transform.position.x, this.transform.position.y, this.unitychan.transform.position.z-difference);
	}

	//#################################################################################

}
// End