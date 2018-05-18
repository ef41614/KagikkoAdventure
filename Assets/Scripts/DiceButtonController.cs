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
	GameObject GuideM;
	guideController GuideC;
	GameObject fadeScript;
	FadeScript FadeSC;
	GameObject ArrowB;
	private float timeleft;

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
		GuideM = GameObject.Find ("guideMaster");
		GuideC = GuideM.GetComponent<guideController> ();
		fadeScript = GameObject.Find ("blackpanel");
		FadeSC = fadeScript.GetComponent<FadeScript> ();
		ArrowB = GameObject.Find ("arrowButtons");
	}

	//####################################  Update  ###################################

	void Update () {

		//スペースが押されたらサイコロをふる
		if (Input.GetKeyDown (KeyCode.Space)) {
			DiceRoll ();
		}

		// 進めるマスが0 && サイコロふる準備ができたら、サイコロボタンを有効（再表示）にする
		if (canRoll == true) {
//			Debug.Log("DiceB.transform.rotation.y！"+DiceB.transform.rotation.y);
			if((DiceB.transform.rotation.y>0.01)||(DiceB.transform.rotation.y<-0.01)){
			DiceB.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
			}else if ((DiceB.transform.rotation.y <=0.01)||(DiceB.transform.rotation.y>=-0.01)) {
				DiceB.transform.Rotate(new Vector3(0, 0, 0));
			}

			DiceB.SetActive (true);

		} else if(canRoll == false){
			DiceB.SetActive (false);	
		}

	}

	//####################################  other  ####################################

	IEnumerator WaitAndDiceSet(){
		yield return new WaitForSeconds (1.8f);
		DiceB.SetActive (true);
	}

	void DiceSet(){
		DiceB.SetActive (true);
	}

	//サイコロをふる処理
	public void DiceRoll() {
		this.DiceB = GameObject.Find ("DiceRollButton");
		if (DiceB != null) {
			int num = Random.Range (2, 7);
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
			DiceB.transform.Translate (0,0,10);
			DiceB.transform.Rotate(new Vector3(0, 270, 0));
			GuideC.ToUnderGround ();	
			GuideC.initializePosition ();

			canRoll = false;
		} else {
		}

	}

	//#################################################################################

}
// End
