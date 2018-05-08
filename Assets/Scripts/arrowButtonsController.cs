using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowButtonsController : MonoBehaviour {
	
	private GameObject ArrowB;
	GameObject unitychan; // Unityちゃんそのものが入る変数  
	UnityChanController Uscript; // UnityChanControllerが入る変数
//	public int rstp = 0;
	public bool canMove = false;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		unitychan = GameObject.Find ("unitychan"); 
		Uscript = unitychan.GetComponent<UnityChanController>(); 
		Debug.Log("Arrowスクリプト出席確認");
		ArrowB = GameObject.Find ("arrowButtons");

	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

//		rstp = Uscript.RemainingSteps;

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