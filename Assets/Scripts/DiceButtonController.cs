using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceButtonController : MonoBehaviour {

	private GameObject DiceB;
	private int DiceResult = 0;
	GameObject unitychan; 
	UnityChanController Uscript; 
	public bool canRoll = true;
	private GameObject stepTx;  //残り歩数

	//☆################☆################  Start  ################☆################☆

	void Start () {
		unitychan = GameObject.Find ("unitychan"); 
		Uscript = unitychan.GetComponent<UnityChanController>(); 
		Debug.Log("Diceスクリプト出席確認");
		this.stepTx = GameObject.Find("stepText");
		this.DiceB = GameObject.Find ("DiceRollButton");
	}

	//####################################  Update  ###################################

	void Update () {

		//スペースが押されたらサイコロをふる
		if (Input.GetKeyDown (KeyCode.Space)) {
			DiceRoll ();
		}
			
		// 進めるマスが0 && サイコロふる準備ができたら、サイコロボタンを有効（再表示）にする
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
			this.stepTx.GetComponent<Text> ().text = "あと " + Uscript.RemainingSteps + "マス";
			canRoll = false;
		} else {
		}
			
	}

	//#################################################################################

}
// End