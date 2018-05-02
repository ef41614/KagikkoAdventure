
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

	// 任意の移動スピード用変数
	public float speed = 300.0f;

	// あと何マス動けるか
	private int RemainingSteps = 0;

	private Vector3 Player_pos; //プレイヤーのポジション
//	private float pushX =0; 
//	private float pushZ =0; 
	public Rigidbody rb;
	//アニメーションするためのコンポートを入れる
	private Animator myAnimator;
	//残り歩数を表示するテキスト
	private GameObject stepTx;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		Debug.Log ("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ゲームスタート■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
		int num = Random.Range (1, 13);
		RemainingSteps = num;
		Debug.Log("サイコロが止まった！ あと"+RemainingSteps+"マス動けます");
		Debug.Log("speed:"+speed);

		Player_pos = GetComponent<Transform>().position; //最初の時点でのプレイヤーのポジションを取得
		rb = GetComponent<Rigidbody>();
		//Animatorコンポーネント取得
		this.myAnimator = GetComponent<Animator>();
		this.stepTx = GameObject.Find("stepText");
	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

//		pushX = Input.GetAxis("Horizontal"); //x方向の入力値を取得
//		pushZ = Input.GetAxis("Vertical");   //z方向の入力値を取得



		// 左へ移動
		//		transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
		//		transform.Translate(-0.15f, 0, 0);
		//		if (Input.GetKey ("up")) {
		//			transform.position += transform.forward * speed * Time.deltaTime;
		//		}


		if (rb.IsSleeping ()) {
			this.myAnimator.SetBool ("isRunning", false);

			if (RemainingSteps > 0) {

				/** GetKeyDown - transform.position
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				transform.position += transform.forward * speed * Time.deltaTime;
				RemainingSteps = reduceSteps(RemainingSteps);
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				transform.position -= transform.forward * speed * Time.deltaTime;
				RemainingSteps = reduceSteps(RemainingSteps);
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				transform.position += transform.right * speed * Time.deltaTime;
				RemainingSteps = reduceSteps(RemainingSteps);
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				transform.position -= transform.right * speed * Time.deltaTime;
				RemainingSteps = reduceSteps(RemainingSteps);
			}
			**/


				/** GetKeyDown_transform.Translate
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				transform.Translate (0, 0, 1);
				RemainingSteps = reduceSteps(RemainingSteps);
			}
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				transform.Translate (0, 0, -1);
				RemainingSteps = reduceSteps(RemainingSteps);
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				transform.Translate (-1, 0, 0);
				RemainingSteps = reduceSteps(RemainingSteps);
			}
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				transform.Translate (1, 0, 0);
				RemainingSteps = reduceSteps(RemainingSteps);
			}
**/

//			/**

				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					rb.AddForce (0, 0, 1 * speed);
					RemainingSteps = reduceSteps (RemainingSteps);
					transform.rotation = Quaternion.AngleAxis (0, new Vector3 (0, 1, 0));
				}

				if (Input.GetKeyDown (KeyCode.DownArrow)) {
					rb.AddForce (0, 0, -1 * speed);
					RemainingSteps = reduceSteps (RemainingSteps);
					transform.rotation = Quaternion.AngleAxis (180, new Vector3 (0, 1, 0));
				}

				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					rb.AddForce (-1 * speed, 0, 0);
					RemainingSteps = reduceSteps (RemainingSteps);
					transform.rotation = Quaternion.AngleAxis (90, new Vector3 (0, -1, 0));
				}

				if (Input.GetKeyDown (KeyCode.RightArrow)) {
					rb.AddForce (1 * speed, 0, 0);
					RemainingSteps = reduceSteps (RemainingSteps);
					transform.rotation = Quaternion.AngleAxis (90, new Vector3 (0, 1, 0));
				}
//			**/

				/**
			float x =  Input.GetAxis("Horizontal") * speed;
			float z = Input.GetAxis("Vertical") * speed;
			rb.AddForce(x , 0 , z );
			**/

				Debug.Log("あと"+RemainingSteps+"マス動けます");
				this.stepTx.GetComponent<Text> ().text = "あと " + RemainingSteps + "マス";


			}
		} else {
			this.myAnimator.SetBool ("isRunning", true);
		}

	}

	//####################################  other  ####################################

	public int reduceSteps(int stp){
		stp -= 1;
//		Debug.Log("あと"+stp+"マス動けます");
		return stp;

		// Animatorコンポーネントを取得し、"isRunning"をtrueにする


//		Vector3 diff = transform.position - Player_pos;          //プレイヤーがどの方向に進んでいるかがわかるように、初期位置と現在地の座標差分を取得
//		if (diff.magnitude > 0.01f){                             //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
//			transform.rotation = Quaternion.LookRotation(diff);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
//		}
//		Player_pos = transform.position; //プレイヤーの位置を更新
	}

	//#################################################################################

}
// End