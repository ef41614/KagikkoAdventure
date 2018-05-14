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
	GameObject turnmanager;
	TurnManager TurnMscript;
	GameObject pchan; 
	PchanController Pscript; 


	//☆################☆################  Start  ################☆################☆

	void Start () {
		unitychan = GameObject.Find ("unitychan"); 
		Uscript = unitychan.GetComponent<UnityChanController>(); 
		pchan = GameObject.Find ("pchan"); 
		Pscript = pchan.GetComponent<PchanController>(); 
		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 
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
		} else if(canRoll == false) {
			DiceB.SetActive (false);	
		}

	}

	//####################################  other  ####################################

	//サイコロをふる処理
	public void DiceRoll() {
		this.DiceB = GameObject.Find ("DiceRollButton");
		if (DiceB != null) {
			int num = Random.Range (1, 6);
			DiceResult = num;
			if(TurnMscript.canMove1P == true){
				Uscript.RemainingSteps = DiceResult;
				this.stepTx.GetComponent<Text> ().text = "あと " + (Uscript.RemainingSteps-1) + "マス";
				Uscript.UDiceTicket--;
			}else if(TurnMscript.canMove2P == true){
				Pscript.RemainingSteps = DiceResult;
				this.stepTx.GetComponent<Text> ().text = "あと " + (Pscript.RemainingSteps-1) + "マス";
				Pscript.PDiceTicket--;
			}
			Debug.Log("サイコロ投げた！");
			Debug.Log("サイコロが止まった！ あと"+DiceResult+"マス動けます");

			canRoll = false;
		} else {
		}

	}

	//#################################################################################

}
// End
