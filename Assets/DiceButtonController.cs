﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceButtonController : MonoBehaviour {

	//サイコロボタン押下の判定
	private bool isDiceButtonDown = false;
	private GameObject DiceB;
	private int DiceResult = 0;
	GameObject unitychan; // Unityちゃんそのものが入る変数  
	UnityChanController Uscript; // UnityChanControllerが入る変数
	public bool canRoll = true;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		unitychan = GameObject.Find ("unitychan"); 
		Uscript = unitychan.GetComponent<UnityChanController>(); 
		Debug.Log("Diceスクリプト出席確認");

	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

		//スペースが押されたらサイコロをふる
		if (Input.GetKeyDown (KeyCode.Space)) {
			DiceRoll ();
		}
			
		// 勧めるマスが0でサイコロふる準備ができたら、サイコロボタン有効（再表示）にする
		if (canRoll == true) {
			DiceB.SetActive (true);
		} else {
			DiceB.SetActive (false);	
		}

	}

	//####################################  other  ####################################

	//サイコロをふる処理
	public void DiceRoll() {
		this.DiceB = GameObject.Find ("DiceRollButton");
		if (DiceB != null) {
			int num = Random.Range (1, 13);
			DiceResult = num;
			Uscript.RemainingSteps = DiceResult;
			Debug.Log("サイコロ投げた！");
			Debug.Log("サイコロが止まった！ あと"+DiceResult+"マス動けます");
			canRoll = false;
			Uscript.RemainingSteps = DiceResult;
		} else {
		}
			
	}

	//#################################################################################

}
// End