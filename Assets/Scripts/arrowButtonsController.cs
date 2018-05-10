using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowButtonsController : MonoBehaviour {
	
	private GameObject ArrowB;
	GameObject unitychan; 
	UnityChanController Uscript;
	public bool canMove = false;

	//☆################☆################  Start  ################☆################☆

	void Start () {
		unitychan = GameObject.Find ("unitychan"); 
		Uscript = unitychan.GetComponent<UnityChanController>(); 
		Debug.Log("Arrowスクリプト出席確認");
		ArrowB = GameObject.Find ("arrowButtons");

	}

	//####################################  Update  ###################################

	void Update () {

		// 勧めるマスが0より大きい時、移動矢印ボタンを有効（再表示）にする
		if(canMove == true){
			ArrowB.SetActive (true);
		} else {
			ArrowB.SetActive (false);	
		}

	}

	//####################################  other  ####################################
	//#################################################################################

}
// End