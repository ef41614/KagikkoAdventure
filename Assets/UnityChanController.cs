using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnityChanController : MonoBehaviour {

	// 任意の移動スピード用変数
	private float RunTime = 0.5f;

	// あと何マス動けるか
	private int RemainingSteps = 0;

	private Vector3 Player_pos; //プレイヤーのポジション
	private Vector3 NextPos;

	public Rigidbody rb;
	//アニメーションするためのコンポートを入れる
	private Animator myAnimator;
	//残り歩数を表示するテキスト
	private GameObject stepTx;

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

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		Debug.Log ("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ゲームスタート■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
		int num = Random.Range (1, 13);
		RemainingSteps = num;
		Debug.Log("サイコロが止まった！ あと"+RemainingSteps+"マス動けます");
		Debug.Log("RunTime:"+RunTime);

		Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
		rb = GetComponent<Rigidbody>();
		//Animatorコンポーネント取得
		this.myAnimator = GetComponent<Animator>();
		this.stepTx = GameObject.Find("stepText");
		this.myAnimator.SetBool ("isRunning", false);
	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

		if (rb.IsSleeping ()) {
			this.myAnimator.SetBool ("isRunning", false);
			UIsRunning = false;

			if (RemainingSteps > 0) {
				checkNextMove ();

				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					if (canGoF == true) {
						Player_pos = GetComponent<Transform> ().position;
						NextPos = Player_pos + (new Vector3 (0, 0, 3));
						transform.DOLocalMove (NextPos, RunTime);
						RemainingSteps = reduceSteps (RemainingSteps);
						transform.rotation = Quaternion.AngleAxis (0, new Vector3 (0, 1, 0));
					}
				}

				if (Input.GetKeyDown (KeyCode.DownArrow)) {
					if (canGoB == true) {
						Player_pos = GetComponent<Transform> ().position;
						NextPos = Player_pos + (new Vector3 (0, 0, -3));
						transform.DOLocalMove (NextPos, RunTime);
						RemainingSteps = reduceSteps (RemainingSteps);
						transform.rotation = Quaternion.AngleAxis (180, new Vector3 (0, 1, 0));
					}
				}

				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					if (canGoL == true) {
						Player_pos = GetComponent<Transform> ().position;
						NextPos = Player_pos + (new Vector3 (-3, 0, 0));
						transform.DOLocalMove (NextPos, RunTime);
						RemainingSteps = reduceSteps (RemainingSteps);
						transform.rotation = Quaternion.AngleAxis (90, new Vector3 (0, -1, 0));
					}
				}

				if (Input.GetKeyDown (KeyCode.RightArrow)) {
					if (canGoR == true) {
						Player_pos = GetComponent<Transform> ().position;
						NextPos = Player_pos + (new Vector3 (3, 0, 0));
						transform.DOLocalMove (NextPos, RunTime);
						RemainingSteps = reduceSteps (RemainingSteps);
						transform.rotation = Quaternion.AngleAxis (90, new Vector3 (0, 1, 0));
					}
				}

				Debug.Log("あと"+RemainingSteps+"マス動けます");
				this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";

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

	//#################################################################################

}
// End