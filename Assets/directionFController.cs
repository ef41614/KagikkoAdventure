using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionFController : MonoBehaviour {

	// Unityちゃんのオブジェクト
	private GameObject unitychan;
	// UnityChanControllerが入る変数
	UnityChanController Uscript; 
	public GameObject dirM;
	private Vector3 UniPos;
	private bool canGoAhead = true;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {

		// Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find ("unitychan");
		// unitychanの中にあるUnityChanControllerを取得して変数に格納する
		Uscript = unitychan.GetComponent<UnityChanController>(); 
		//		this.dirM = GameObject.Find ("directionR");
		///		this.dirM = GameObject.Find ("directionL");
		this.dirM = GameObject.Find ("directionF");
		//      this.dirM = GameObject.Find ("directionB");

	}

	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {

		Debug.Log ("進路はクリア? canGoAhead :"+canGoAhead);
		bool URun = Uscript.UIsRunning; 

		// Unityちゃんが走っていない時（＝止まっている時）、
		if (URun == false){ 
			//Unityちゃんの位置に合わせてガイドマスの位置を移動
			UniPos = unitychan.transform.position;
			this.transform.position = new Vector3(UniPos.x, this.transform.position.y, UniPos.z);

			// 進路クリアならばガイドマスを表示する
			if (canGoAhead == true) {
				dirM.SetActive (true);
			} else {
				dirM.SetActive (false);	
			}

		} else {
			dirM.SetActive (false);	
		}

	}

	//####################################  other  ####################################

	void OnTriggerEnter(Collider other){
		if ((other.gameObject.tag == "Tree")||(other.gameObject.tag =="obstacle")){
			canGoAhead = false;
			Debug.Log ("障害物にぶつかる");
		}
	}

	void OnTriggerExit(Collider other){
		canGoAhead = true;
		Debug.Log ("障害物は無いよ");
	}

	//#################################################################################

}
// End