using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour {

	// 任意の移動スピード用変数
	private float RunTime = 0.7f;

	// あと何マス動けるか
	public int RemainingSteps = 0;

	public Vector3 Player_pos; 
	public Vector3 NextPos;

	public Rigidbody rb;
	private Animator myAnimator;
	private GameObject stepTx;  //残り歩数

	public bool PIsRunning = false;
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
	GameObject charamovemanager;
	CharaMoveManager CharaMoveMscript;

	public int UDiceTicket = 1;
	public int PDiceTicket = 1;
	float timeleft =0;

	//☆################☆################  Start  ################☆################☆

	public void Start () {
		Debug.Log ("キャラConスクリプト出席確認");

	}


	//####################################  Update  ###################################

	void Update () {

	}

	//####################################  other  ####################################

	//------- 旧  Uちゃん Update () ----------------
//	/**
	public void charaUpdate(){
		if (TurnMscript.canMove2P == true) {
			if (ArrivedNextPoint == true) {
				// 走行中状態がOFF（＝停止状態）の時
				this.myAnimator.SetBool ("isRunning", false);  
				PIsRunning = false;

				if (RemainingSteps > 0) {
					CharaMoveMscript.checkNextMove ();
					ArrowC.canMove = true;

				} else if (RemainingSteps <= 0) {
					if (PDiceTicket <= 0) {
						if (rb.IsSleeping ()) {
							DiceC.canRoll = true;
							ArrowC.canMove = false;
						}
					}
				}

			} else {
				this.myAnimator.SetBool ("isRunning", true);
				PIsRunning = true;
				ArrivedNextPoint = false;

			}
		} else {
			// 走行中状態がOFF（＝停止状態）の時
			this.myAnimator.SetBool ("isRunning", false);  
			PIsRunning = false;
		}
	}
//	**/
	//----------------------------------------------

	//#################################################################################

}
// End