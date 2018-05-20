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
	public Animator myAnimator;
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
	private GameObject Arrowbox;
	GameObject ArrowBtns;
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
	GameObject blackpanel;
	FadeScript FadeSC;

	public int UDiceTicket = 1;
	public int PDiceTicket = 1;

	bool canMoveInfo;
	bool RunningInfo;
	int TicketInfo = 0;
	GameObject activeChara;
	GameObject activeCharaScript;

	GameObject wall_Left;
	GameObject wall_Right;
	GameObject wall_Bottom;
	GameObject wall_Top;

	Vector3 LeftPos;
	Vector3 RightPos;
	Vector3 BottomPos;
	Vector3 TopPos;


	//☆################☆################  Start  ################☆################☆
	void Start () {
		Debug.Log ("CharaMoveManagerスクリプト出席確認");

		Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
		rbInfo = GetComponent<Rigidbody>();
		myAnimator = GetComponent<Animator>();
		this.stepTx = GameObject.Find("stepText");
//		this.myAnimator.SetBool ("isRunning", false);

		DiceB = GameObject.Find ("DiceBox");
		DiceC = DiceB.GetComponent<DiceButtonController>(); 
		Arrowbox = GameObject.Find ("ArrowsBox");
		ArrowBtns = GameObject.Find ("arrowButtons");
		ArrowC = Arrowbox.GetComponent<arrowButtonsController>();
		GuideM = GameObject.Find ("guideMaster");
		GuideC = GuideM.GetComponent<guideController> ();

		this.unitychan = GameObject.Find ("unitychan");
		Uscript = unitychan.GetComponent<UnityChanController>();
		pchan = GameObject.Find ("pchan"); 
		Pscript = pchan.GetComponent<PchanController>(); 
		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 
		blackpanel = GameObject.Find ("blackpanel");
		FadeSC = blackpanel.GetComponent<FadeScript> ();

		ArrivedNextPoint = true;

		wall_Left = GameObject.Find ("Cube_W");
		wall_Right = GameObject.Find ("Cube_E");
		wall_Bottom = GameObject.Find ("Cube_S");
		wall_Top = GameObject.Find ("Cube_N");

		LeftPos = wall_Left.transform.position;
		LeftPos.x += 3; 
		RightPos = wall_Right.transform.position;
		RightPos.x -= 3;
		BottomPos = wall_Bottom.transform.position;
		BottomPos.z += 3;
		TopPos = wall_Top.transform.position;
		TopPos.z -= 3.5f;
	}

	//####################################  Update  ###################################

	void Update () {
			unitychan.transform.position = (new Vector3 (
			Mathf.Clamp (unitychan.transform.position.x, LeftPos.x, RightPos.x),
			Mathf.Clamp (unitychan.transform.position.y, -1, 3),
			Mathf.Clamp (unitychan.transform.position.z, BottomPos.z, TopPos.z)
		));
			
			pchan.transform.position = (new Vector3 (
			Mathf.Clamp (pchan.transform.position.x, LeftPos.x, RightPos.x),
			Mathf.Clamp (pchan.transform.position.y, -1, 3),
			Mathf.Clamp (pchan.transform.position.z, BottomPos.z, TopPos.z) 
		));

		if (TurnMscript.canMove1P == true) {
			canMoveInfo = TurnMscript.canMove1P;
			RunningInfo = Uscript.UIsRunning;
			TicketInfo = Uscript.UDiceTicket;
			rbInfo = unitychan.GetComponent<Rigidbody> ();
			RemainingStepsInfo = Uscript.RemainingSteps;
			Player_pos = unitychan.GetComponent<Transform>().position; 
			activeChara = unitychan;
			Uscript.Player_pos = Player_pos;
			myAnimator = unitychan.GetComponent<Animator> ();
			rbInfo = Uscript.rb;
//			activeCharaScript = Uscript;
//			Uscript.NextPos = NextPos;
		}

		if (TurnMscript.canMove2P == true) {
			canMoveInfo = TurnMscript.canMove2P;
			RunningInfo = Pscript.PIsRunning;
			TicketInfo = Pscript.PDiceTicket;
			rbInfo = pchan.GetComponent<Rigidbody> ();
			RemainingStepsInfo = Pscript.RemainingSteps;
			Player_pos = pchan.GetComponent<Transform>().position; 
			activeChara = pchan;
			Pscript.Player_pos = Player_pos;
			myAnimator = Pscript.GetComponent<Animator> ();
			rbInfo = Pscript.rb;
//			activeCharaScript = Pscript;
//			Pscript.NextPos = NextPos;
		}



		if (canMoveInfo == true) {

			//---動けなくなった時の救済措置---
			if(ArrowC.canMove == true){
				GameObject[] directions = GameObject.FindGameObjectsWithTag("guideChild");
				if (directions != null) {
				}else{
					if (Player_pos.x >= 0) {
						Vector3 toLeft = new Vector3 (this.transform.position.x-3, this.transform.position.y, this.transform.position.z);
						transform.DOLocalMove (toLeft, 0.1f);
						Debug.Log("救済：左に行ったよ");
					} else if (Player_pos.x < 0) {
						Vector3 toLeft = new Vector3 (this.transform.position.x-3, this.transform.position.y, this.transform.position.z);
						transform.DOLocalMove (toLeft, 0.1f);
						Debug.Log("救済：右に行ったよ");
					}
					if (Player_pos.z >= 0) {
						Vector3 toLeft = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z-3);
						transform.DOLocalMove (toLeft, 0.1f);
						Debug.Log("救済：後ろに行ったよ");
					} else if (Player_pos.z < 0) {
						Vector3 toLeft = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z+3);
						transform.DOLocalMove (toLeft, 0.1f);
						Debug.Log("救済：前に行ったよ");
					}
				}
			}
			//-----------------------------------

//			Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前5");
			if (ArrivedNextPoint == true) {
				// 走行中状態がOFF（＝停止状態）の時
//				this.myAnimator.SetBool ("isRunning", false);  
				RunningInfo = false;
				if (RemainingStepsInfo > 0) {
					checkNextMove ();
					ArrowC.canMove = true;
//					Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前4");
				} else if (RemainingStepsInfo <= 0) {
//					Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前3");
					if (TicketInfo <= 0) {
//						Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前2");
//						if (rbInfo.IsSleeping ()) {
							DiceC.canRoll = true;
							ArrowC.canMove = false;
//							Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し前");
//						Invoke ("TurnMscript.ChangePlayer", 1.0f);
//					TurnMscript.ChangePlayer ();
						StartCoroutine("WaitAndTurnChange");
//							Debug.Log ("CharaMoveManagerからターン切り替えスクリプト呼び出し後");
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

	IEnumerator WaitAndTurnChange(){
		yield return new WaitForSeconds(0.8f);
		FadeSC.goFadeOut = true;
		FadeSC.goFadeIn = false;
//		GuideC.initializePosition ();
		yield return new WaitForSeconds(0.8f);
//		GuideC.initializePosition ();
		TurnMscript.ChangePlayer ();
//		GuideC.initializePosition ();
		FadeSC.goFadeOut = false;
		FadeSC.goFadeIn = true;
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

	//------- 旧  Uちゃん Update () ----------------
	/**
	public void charaUpdate(){
			if(canMoveInfo ==true){
			if (ArrivedNextPoint == true) {
				// 走行中状態がOFF（＝停止状態）の時
				myAnimator.SetBool ("isRunning", false);  
				activeCharaScript.myAnimator.SetBool ("isRunning", false);  

				RunningInfo = false;

				if (RemainingStepsInfo > 0) {
					checkNextMove ();
					ArrowC.canMove = true;

				} else if (RemainingStepsInfo <= 0) {
					if (PDiceTicket <= 0) {
						if (rbInfo.IsSleeping ()) {
							DiceC.canRoll = true;
							ArrowC.canMove = false;
						}
					}
				}

			} else {
				myAnimator.SetBool ("isRunning", true);
				RunningInfo = true;
				ArrivedNextPoint = false;

			}
		}
		else {
			// 走行中状態がOFF（＝停止状態）の時
			myAnimator.SetBool ("isRunning", false);  
			RunningInfo = false;
		}
	}
	**/
	//----------------------------------------------

	//#################################################################################

}
// End
