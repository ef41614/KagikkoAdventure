using System.Collections;
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

	GameObject turnmanager;
	TurnManager TurnMscript;

	public int UDiceTicket = 1;

	//☆################☆################  Start  ################☆################☆
	void Start () {
		Debug.Log ("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ゲームスタート■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");

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

		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 

		ArrivedNextPoint = true;
		Debug.Log("開始 UDiceTicket :"+UDiceTicket);
	}

	//####################################  Update  ###################################

	void Update () {
		Debug.Log("UDiceTicket :"+UDiceTicket);
		if (TurnMscript.canMove1P == true) {
			if (ArrivedNextPoint == true) {
				// 走行中状態がOFF（＝停止状態）の時
				this.myAnimator.SetBool ("isRunning", false);  
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

				} else if (RemainingSteps <= 0) {
					if (UDiceTicket <= 0) {
						if (rb.IsSleeping ()) {
							DiceC.canRoll = true;
							ArrowC.canMove = false;
							TurnMscript.ChangePlayer ();
							Debug.Log ("Uちゃんからターン切り替えスクリプト呼び出し");
						}
					}
				}

			} else {
				this.myAnimator.SetBool ("isRunning", true);
				UIsRunning = true;
				ArrivedNextPoint = false;

			}
		} else {
			// 走行中状態がOFF（＝停止状態）の時
			this.myAnimator.SetBool ("isRunning", false);  
			UIsRunning = false;
		}
	}

	//####################################  other  ####################################

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

	public int reduceSteps(int stp){
		stp -= 1;
		return stp;
	}

	//------------------------------------------------

	public void MoveForward() {
		MoveNextPosition (0,3,0,canGoF);
	}

	public void MoveBack() {
		MoveNextPosition (0,-3,180,canGoB);
	}

	public void MoveLeft() {
		MoveNextPosition (-3,0,-90,canGoL);
	}

	public void MoveRight() {
		MoveNextPosition (3,0,90,canGoR);
	}

	public void MoveNextPosition(int x, int z, int turn, bool canGoDir){
		if (TurnMscript.canMove1P == true) {
			if (ArrivedNextPoint == true) {
				UIsRunning = false;
				if (RemainingSteps > 0) {
					if (canGoDir == true) {
//						Player_pos = GetComponent<Transform> ().position;
//						FixPosition ();
//						NextPos = Player_pos + (new Vector3 (x, 0, z));

//						transform.DOLocalMove (NextPos, RunTime);
//						transform.rotation = Quaternion.AngleAxis (turn, new Vector3 (0, 1, 0));
						this.stepTx.GetComponent<Text> ().text = "あと " + (RemainingSteps - 1) + "マス";
//						GuideC.ToUnderGround ();	
//						GuideC.adjustNextGuidePos ();
					}
				} else {

				}
			}
		}
	}

	//---------------------------------------

	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "guideM"){
			ArrivedNextPoint = true;
			transform.position = GuideC.NextGuidePos;
			RemainingSteps = reduceSteps (RemainingSteps);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "guideM") {
			ArrivedNextPoint = false;
			this.stepTx.GetComponent<Text> ().text = "あと " + (RemainingSteps-1) + "マス";
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
