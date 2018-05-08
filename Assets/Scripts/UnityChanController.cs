﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnityChanController : MonoBehaviour {

	// 任意の移動スピード用変数
	private float RunTime = 0.7f;

	// あと何マス動けるか
	public int RemainingSteps = 0;

	public Vector3 Player_pos; 
	public Vector3 NextPos;

	public Rigidbody rb;
	private Animator myAnimator;
	private GameObject stepTx;  //残り歩数

	public bool UIsRunning = false;
	[SerializeField]
	RectTransform rectTran;

	private bool canGoR = true;
	private bool canGoL = true;
	private bool canGoF = true;
	private bool canGoB = true;

	private GameObject dirR;
	private GameObject dirL;
	private GameObject dirF;
	private GameObject dirB;

	private GameObject DiceB; 
	public DiceButtonController DiceC;
	private GameObject ArrowB;
	public arrowButtonsController ArrowC;
	private GameObject GuideM;
	public guideController GuideC;

	public bool ArrivedNextPoint = false;

	//☆################☆################  Start  ################☆################☆
	void Start () {
		Debug.Log ("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ゲームスタート■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");

		Debug.Log("サイコロが止まった！ あと"+RemainingSteps+"マス動けます");
		Debug.Log("RunTime:"+RunTime);

		Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
		rb = GetComponent<Rigidbody>();
		this.myAnimator = GetComponent<Animator>();
		this.stepTx = GameObject.Find("stepText");
		this.myAnimator.SetBool ("isRunning", false);

		DiceB = GameObject.Find ("DiceBox");
		DiceC = DiceB.GetComponent<DiceButtonController>(); 
		ArrowB = GameObject.Find ("ArrowsBox");
		ArrowC = ArrowB.GetComponent<arrowButtonsController>();
		GuideM = GameObject.Find ("guideMaster");
		GuideC = GuideM.GetComponent<guideController> ();
//		Player_pos = new Vector3 (21, 0, -18);
//		transform.Translate(new Vector3 (3, 0, -3));
//		transform.position = Player_pos;
//		ArrivedNextPoint = true;
	}

	//####################################  Update  ###################################

	void Update () {

		if(ArrivedNextPoint == true){
//		if (rb.IsSleeping ()) {
			this.myAnimator.SetBool ("isRunning", false);  // 走行中OFF（＝停止状態）
			UIsRunning = false;

			if (RemainingSteps > 0) {
				checkNextMove ();
				ArrowC.canMove = true;

				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					MoveForward ();
				}

				if (Input.GetKeyDown (KeyCode.DownArrow)) {
					MoveBack ();
				}

				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					MoveLeft ();
				}

				if (Input.GetKeyDown (KeyCode.RightArrow)) {
					MoveRight ();
				}

//				Debug.Log ("あと" + RemainingSteps + "マス動けます");
//★				this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";

			} else if(RemainingSteps <=0){
				DiceC.canRoll = true;
				ArrowC.canMove = false;
			}

		} else {
			this.myAnimator.SetBool ("isRunning", true);
			UIsRunning = true;
//			ArrivedNextPoint = false;
		}

	}

	//####################################  other  ####################################

	public int reduceSteps(int stp){
		stp -= 1;
		return stp;
	}

	public void checkNextMove(){
		this.dirR = GameObject.Find ("directionR");
		this.dirL = GameObject.Find ("directionL");
		this.dirF = GameObject.Find ("directionF");
		this.dirB = GameObject.Find ("directionB");

		if (dirR != null) {
			canGoR = true;
		} else {
			canGoR = false;
		}

		if (dirL != null) {
			canGoL = true;
		} else {
			canGoL = false;
		}

		if (dirF != null) {
			canGoF = true;
		} else {
			canGoF = false;
		}

		if (dirB != null) {
			canGoB = true;
		} else {
			canGoB = false;
		}
	}

	public void MoveForward() {
		if(ArrivedNextPoint == true){
			UIsRunning = false;
		if (RemainingSteps > 0) {
			if (canGoF == true) {
				Player_pos = GetComponent<Transform> ().position;
					FixPosition ();
					//Player_pos.x = Mathf.RoundToInt ( ((Player_pos.x)/3)*3); 
					//Player_pos.z = Mathf.RoundToInt ( ((Player_pos.z)/3)*3);
				NextPos = Player_pos + (new Vector3 (0, 0, 3));
				transform.DOLocalMove (NextPos, RunTime);
				RemainingSteps = reduceSteps (RemainingSteps);
				transform.rotation = Quaternion.AngleAxis (0, new Vector3 (0, 1, 0));
				this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";
			}
		} else {
			ArrowC.canMove = false;
		}
	}
	}

	public void MoveBack() {
		if(ArrivedNextPoint == true){
			UIsRunning = false;
		if (RemainingSteps > 0) {
			if (canGoB == true) {
				Player_pos = GetComponent<Transform> ().position;
					FixPosition ();
//					Player_pos.x = Mathf.RoundToInt ( ((Player_pos.x)/3)*3); 
//					Player_pos.z = Mathf.RoundToInt ( ((Player_pos.z)/3)*3);
				NextPos = Player_pos + (new Vector3 (0, 0, -3));
				transform.DOLocalMove (NextPos, RunTime);
				RemainingSteps = reduceSteps (RemainingSteps);
				transform.rotation = Quaternion.AngleAxis (180, new Vector3 (0, 1, 0));
				this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";
			}
		} else {
			ArrowC.canMove = false;
		}
	}
	}

	public void MoveLeft() {
		if(ArrivedNextPoint == true){
			UIsRunning = false;
		if (RemainingSteps > 0) {
			if (canGoL == true) {
				Player_pos = GetComponent<Transform> ().position;
					FixPosition ();
//					Player_pos.x = Mathf.RoundToInt ( ((Player_pos.x)/3)*3); 
//					Player_pos.z = Mathf.RoundToInt ( ((Player_pos.z)/3)*3);
				NextPos = Player_pos + (new Vector3 (-3, 0, 0));
				transform.DOLocalMove (NextPos, RunTime);
				RemainingSteps = reduceSteps (RemainingSteps);
				transform.rotation = Quaternion.AngleAxis (90, new Vector3 (0, -1, 0));
				this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";
			}
		} else {
			ArrowC.canMove = false;
		}
	}
	}

	public void MoveRight() {
		if(ArrivedNextPoint == true){
			UIsRunning = false;
		if (RemainingSteps > 0) {
			if (canGoR == true) {
				Player_pos = GetComponent<Transform> ().position;
					//Mathf.RoundToInt ( ((NP.x)/3)*3);
					FixPosition ();
//					Player_pos.x = Mathf.RoundToInt ( ((Player_pos.x)/3)*3); 
//					Player_pos.z = Mathf.RoundToInt ( ((Player_pos.z)/3)*3);
//					NextPos = Mathf.RoundToInt (((Player_pos + (new Vector3 (3, 0, 0)))/3)*3);
				NextPos = Player_pos + (new Vector3 (3, 0, 0));
				transform.DOLocalMove (NextPos, RunTime);
				RemainingSteps = reduceSteps (RemainingSteps);
				transform.rotation = Quaternion.AngleAxis (90, new Vector3 (0, 1, 0));
				this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";
			}
		} else {
			ArrowC.canMove = false;
		}
	}
	}

	public void OnTriggerEnter(Collider other){
			if (other.gameObject.tag == "guideM"){
			ArrivedNextPoint = true;
				Debug.Log ("次のマスに到達したよ");
//			transform.Translate(NextPos);
			Vector3 NP;
//			NP = NextPos;
//			NP = Player_pos;
//			NP.x = Mathf.RoundToInt ( ((NP.x)/3)*3);
//			NP.z = Mathf.RoundToInt ( ((NP.z)/3)*3);
//			NextPos = NP;
//			transform.Translate(NextPos);
			NP = GuideC.NextGuidePos;
			Debug.Log ("次のマスの座標は"+NP);
//			transform.Translate(NP);
			transform.position = NP;
//			transform.localPosition = NP;
			}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "guideM") {
			ArrivedNextPoint = false;
			Debug.Log ("次のマスに行こう");
			this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";
		}
	}

	void FixPosition(){
		this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";

		Player_pos.x = Mathf.RoundToInt ( ((Player_pos.x)/3)*3);
		if (Player_pos.x % 3 == 2) {
			Player_pos.x ++;
			Debug.Log ("ｘ++修正完了");
		} else if (Player_pos.x % 3 == 1) {
			Player_pos.x --;
			Debug.Log ("ｘ--修正完了");
		}
			
		Player_pos.z = Mathf.RoundToInt ( ((Player_pos.z)/3)*3);
		if (Player_pos.z % 3 == 2) {
			Player_pos.z ++;
			Debug.Log ("ｚ++修正完了");
		} else if (Player_pos.z % 3 == 1) {
			Player_pos.z --;
			Debug.Log ("ｚ--修正完了");
		}
	}
		
	//#################################################################################

}
// End