using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnityChanController : MonoBehaviour {

	// 任意の移動スピード用変数
	private float RunTime = 0.5f;

	// あと何マス動けるか
	public int RemainingSteps = 0;

	private Vector3 Player_pos; 
	private Vector3 NextPos;

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
	}

	//####################################  Update  ###################################

	void Update () {

		if (rb.IsSleeping ()) {
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

				Debug.Log ("あと" + RemainingSteps + "マス動けます");
				this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";

			} else if(RemainingSteps <=0){
				DiceC.canRoll = true;
				ArrowC.canMove = false;
			}

		} else {
			this.myAnimator.SetBool ("isRunning", true);
			UIsRunning = true;
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
		if (RemainingSteps > 0) {
			if (canGoF == true) {
				Player_pos = GetComponent<Transform> ().position;
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

	public void MoveBack() {
		if (RemainingSteps > 0) {
			if (canGoB == true) {
				Player_pos = GetComponent<Transform> ().position;
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

	public void MoveLeft() {
		if (RemainingSteps > 0) {
			if (canGoL == true) {
				Player_pos = GetComponent<Transform> ().position;
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

	public void MoveRight() {
		if (RemainingSteps > 0) {
			if (canGoR == true) {
				Player_pos = GetComponent<Transform> ().position;
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
		
	//#################################################################################

}
// End