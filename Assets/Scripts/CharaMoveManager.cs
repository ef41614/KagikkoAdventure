using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharaMoveManager : MonoBehaviour {

	// 任意の移動スピード用変数
	private float RunTime = 0.7f;

	// あと何マス動けるか
	public int RemainingStepsInfo = 0;

	public Vector3 Player_pos; 
	public Vector3 NextPos;

	public Rigidbody rbInfo;
//	private Animator myAnimator;
	private GameObject stepTx;  //残り歩数

//	public bool UIsRunning = false;
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

	private GameObject unitychan;
	UnityChanController Uscript; 
	GameObject pchan; 
	PchanController Pscript; 
	GameObject turnmanager;
	TurnManager TurnMscript;

	public int UDiceTicket = 1;
	public int PDiceTicket = 1;

	bool canMoveInfo;
	bool RunningInfo;
	int TicketInfo = 0;
	GameObject activeChara;


	//☆################☆################  Start  ################☆################☆
	void Start () {
		Debug.Log ("CharaMoveManagerスクリプト出席確認");

		Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
		rbInfo = GetComponent<Rigidbody>();
//		this.myAnimator = GetComponent<Animator>();
		this.stepTx = GameObject.Find("stepText");
//		this.myAnimator.SetBool ("isRunning", false);

		DiceB = GameObject.Find ("DiceBox");
		DiceC = DiceB.GetComponent<DiceButtonController>(); 
		ArrowB = GameObject.Find ("ArrowsBox");
		ArrowC = ArrowB.GetComponent<arrowButtonsController>();
		GuideM = GameObject.Find ("guideMaster");
		GuideC = GuideM.GetComponent<guideController> ();

		this.unitychan = GameObject.Find ("unitychan");
		Uscript = unitychan.GetComponent<UnityChanController>();
		pchan = GameObject.Find ("pchan"); 
		Pscript = pchan.GetComponent<PchanController>(); 
		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 

		ArrivedNextPoint = true;

	}

	//####################################  Update  ###################################

	void Update () {

		if (TurnMscript.canMove1P == true) {
			canMoveInfo = TurnMscript.canMove1P;
			RunningInfo = Uscript.UIsRunning;
			TicketInfo = Uscript.UDiceTicket;
			rbInfo = unitychan.GetComponent<Rigidbody> ();
			RemainingStepsInfo = Uscript.RemainingSteps;
			Player_pos = unitychan.GetComponent<Transform>().position; 
			activeChara = unitychan;
		}

		if (TurnMscript.canMove2P == true) {
			canMoveInfo = TurnMscript.canMove2P;
			RunningInfo = Pscript.PIsRunning;
			TicketInfo = Pscript.PDiceTicket;
			rbInfo = pchan.GetComponent<Rigidbody> ();
			RemainingStepsInfo = Pscript.RemainingSteps;
			Player_pos = pchan.GetComponent<Transform>().position; 
			activeChara = pchan;
		}


		if (canMoveInfo == true) {
			Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前5");
			if (ArrivedNextPoint == true) {
				// 走行中状態がOFF（＝停止状態）の時
//				this.myAnimator.SetBool ("isRunning", false);  
				RunningInfo = false;
				if (RemainingStepsInfo > 0) {
					checkNextMove ();
					ArrowC.canMove = true;
					Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前4");
				} else if (RemainingStepsInfo <= 0) {
					Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前3");
					if (TicketInfo <= 0) {
						Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前2");
//						if (rbInfo.IsSleeping ()) {
							DiceC.canRoll = true;
							ArrowC.canMove = false;
							Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前");
							TurnMscript.ChangePlayer ();
							Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し後");
//						}
					}
				}

			} else {
//				this.myAnimator.SetBool ("isRunning", true);
				RunningInfo = true;
				ArrivedNextPoint = false;

			}
		} else {
			// 走行中状態がOFF（＝停止状態）の時
//			this.myAnimator.SetBool ("isRunning", false);  
			RunningInfo = false;
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

//	public int reduceSteps(int stp){
//		stp -= 1;
//		return stp;
//	}

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
		if (canMoveInfo == true) {
			if (ArrivedNextPoint == true) {
				RunningInfo = false;
				if (RemainingStepsInfo > 0) {
					if (canGoDir == true) {
//						Player_pos = GetComponent<Transform> ().position;
//						Debug.Log("Player_pos 修正前:"+Player_pos);
						FixPosition ();
//						Debug.Log("Player_pos 修正後:"+Player_pos);
						NextPos = Player_pos + (new Vector3 (x, 0, z));

						activeChara.transform.DOLocalMove (NextPos, RunTime);
						activeChara.transform.rotation = Quaternion.AngleAxis (turn, new Vector3 (0, 1, 0));
						this.stepTx.GetComponent<Text> ().text = "あと " + (RemainingStepsInfo - 1) + "マス";
						GuideC.ToUnderGround ();	
						GuideC.adjustNextGuidePos ();
					}
				} else {

				}
			}
		}
	}

	//---------------------------------------

//	public void OnTriggerEnter(Collider other){
//		if (other.gameObject.tag == "guideM"){
//			ArrivedNextPoint = true;
//			transform.position = GuideC.NextGuidePos;
//			RemainingStepsInfo = reduceSteps (RemainingStepsInfo);
//		}
//	}

//	void OnTriggerExit(Collider other){
//		if (other.gameObject.tag == "guideM") {
//			ArrivedNextPoint = false;
//			this.stepTx.GetComponent<Text> ().text = "あと " + (RemainingStepsInfo-1) + "マス";
//		}
//	}

	void FixPosition(){
		this.stepTx.GetComponent<Text> ().text = "あと " + RemainingStepsInfo + "マス";
		Debug.Log ("修正中");
		Player_pos.x = Mathf.RoundToInt ( ((Player_pos.x)/3)*3);
		Debug.Log ("Player_pos.x % 3 は"+ Player_pos.x % 3);
		if ((Player_pos.x % 3 == 2)||(Player_pos.x % 3 == -1)) {
			Player_pos.x ++;
			Debug.Log ("ｘ++修正完了");
		} else if ((Player_pos.x % 3 == 1)||(Player_pos.x % 3 == -2)) {
			Player_pos.x --;
			Debug.Log ("ｘ--修正完了");
		}

		Player_pos.z = Mathf.RoundToInt ( ((Player_pos.z)/3)*3);
			if ((Player_pos.z % 3 == 2)||(Player_pos.z % 3 == -1)) {
			Player_pos.z ++;
			Debug.Log ("ｚ++修正完了");
		} else if ((Player_pos.z % 3 == 1)||(Player_pos.z % 3 == -2)) {
			Player_pos.z --;
			Debug.Log ("ｚ--修正完了");
		}
	}

	//#################################################################################

}
// End
