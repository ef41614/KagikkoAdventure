using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CharacterController), typeof(Collider))]
public class UnityChanController : MonoBehaviour {

	CharacterController m_charCtrl;

	// あと何マス動けるか
	public int RemainingSteps = 0;

	public Vector3 Player_pos; 

	public Rigidbody rb;
	private Animator myAnimator;
	private GameObject stepTx;  //残り歩数

	public bool UIsRunning = false;
	[SerializeField]
	RectTransform rectTran;

	private GameObject DiceB; 
	public DiceButtonController DiceC;
	private GameObject ArrowB;
	public arrowButtonsController ArrowC;

	public bool ArrivedNextPoint = false;

	GameObject turnmanager;
	TurnManager TurnMscript;
	GameObject charamovemanager;
	CharaMoveManager CharaMoveMscript;

	public int UDiceTicket = 1;
	float timeleft =0;


	//☆################☆################  Start  ################☆################☆
	void Start () {
		m_charCtrl = GetComponent<CharacterController>();

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

		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 
		charamovemanager = GameObject.Find ("charamovemanager");
		CharaMoveMscript = charamovemanager.GetComponent<CharaMoveManager> ();

		ArrivedNextPoint = true;
		Debug.Log("開始 UDiceTicket :"+UDiceTicket);
	}

	//####################################  Update  ###################################

	void Update () {
		timeleft -= Time.deltaTime;
		if (timeleft <= 0.0) {
			timeleft = 1.0f;

			Debug.Log("UDiceTicket :"+UDiceTicket);
			Debug.Log("Player_pos :"+Player_pos);
		}


		if (TurnMscript.canMove1P == true) {
			if (ArrivedNextPoint == true) {
				// 走行中状態がOFF（＝停止状態）の時
				this.myAnimator.SetBool ("isRunning", false);  
				UIsRunning = false;

				if (RemainingSteps > 0) {
					CharaMoveMscript.checkNextMove ();
					ArrowC.canMove = true;

				} else if (RemainingSteps <= 0) {
					if (UDiceTicket <= 0) {
						if (rb.IsSleeping ()) {
							DiceC.canRoll = true;
							ArrowC.canMove = false;
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

	public int reduceSteps(int stp){
		stp -= 1;
		return stp;
	}
		
	public void OnTriggerEnter(Collider other){
		if (TurnMscript.canMove1P == true) {
			if (other.gameObject.tag == "guideM"){
				ArrivedNextPoint = true;
				RemainingSteps = reduceSteps (RemainingSteps);
				Debug.Log ("UちゃんguideMに接触：ステップ＿"+RemainingSteps);
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (TurnMscript.canMove1P == true) {
			if (other.gameObject.tag == "guideM") {
				ArrivedNextPoint = false;
				this.stepTx.GetComponent<Text> ().text = "あと " + (RemainingSteps - 1) + "マス";
				Debug.Log ("UちゃんguideMから離脱_RemainingSteps");
			}
		}
	}

	public void OnCollisionEnter(Collision other){
		if (TurnMscript.canMove1P == true) {
			if (other.gameObject.tag == "Player") {
				UnityChanController uc = other.gameObject.GetComponent<UnityChanController> ();
				PchanController pc = other.gameObject.GetComponent<PchanController> ();
				if (pc) {
					pc.Move (transform.forward, Random.Range (1, 4) * 3.0f);
					Debug.Log ("Uちゃんの体当たりだ！");
				}
//				if (bc) {
//					bc.Move (transform.forward, Random.Range (1, 4) * 3.0f);
//				}
			}
		} else if (TurnMscript.canMove1P == false) {
			if (other.gameObject.tag == "obstacle") {
				rb.constraints = RigidbodyConstraints.FreezePosition;
			}
		}
	}

	// ------------ 衝突時の処理--------------------------
	private void OnControllerColliderHit(ControllerColliderHit hit){
		BallController bc = hit.gameObject.GetComponent<BallController> ();
		PchanController pc = hit.gameObject.GetComponent<PchanController> ();
		if (pc) {
			pc.Move (transform.forward, Random.Range (1, 4) * 3.0f);
			Debug.Log("Uちゃんの体当たりだ！");
		}
//		if (bc) {
//			bc.Move (transform.forward, Random.Range (1, 4) * 3.0f);
//		}
	}

	public void Move(Vector3 direction, float distance){
		Vector3 moveVector = direction.normalized * distance;
		transform.DOMove(transform.position + moveVector, 1.5f);
		Debug.Log("1P吹っ飛んだ！");
	}
	//---------------------------------------------------

	//#################################################################################

}
// End
