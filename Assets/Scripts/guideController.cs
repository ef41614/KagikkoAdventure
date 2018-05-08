using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class guideController : MonoBehaviour {

	//Unityちゃんのオブジェクト
	private GameObject unitychan;
	//Unityちゃんとカメラの距離
	private float difference;
	UnityChanController Uscript; // UnityChanControllerが入る変数
	private GameObject dirR;
	private GameObject dirL;
	private GameObject dirF;
	private GameObject dirB;
	private bool canGoAhead = true;
	private Vector3 UniPos;
	public Vector3 NextGuidePos;
	[SerializeField]
	RectTransform rectTran;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {

		//Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find ("unitychan");
		Uscript = unitychan.GetComponent<UnityChanController>(); //unitychanの中にあるUnityChanControllerを取得して変数に格納する
		//Unityちゃんとの位置（z座標）の差を求める
//		this.difference = unitychan.transform.position.z - this.transform.position.z;
//		this.dirR = GameObject.Find ("directionR");
//		this.dirL = GameObject.Find ("directionL");
//		this.dirF = GameObject.Find ("directionF");
//		this.dirB = GameObject.Find ("directionB");


//		transform.position = new Vector3 (21, 0, -18);
//		transform.Translate(new Vector3 (3, 0, -3));


	}
		
	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {
		//		canGoAhead = true;
//		Debug.Log ("木にぶつかる? canGoAhead :"+canGoAhead);

		bool URun = Uscript.UIsRunning; 
		//		UniPos = unitychan.transform.position;
		//		this.unitychan = GameObject.Find ("unitychan");


		//★		UniPos = unitychan.transform.position;

		//★		this.transform.position = new Vector3(UniPos.x, this.transform.position.y, UniPos.z);

		// Unityちゃんが走っていない時（＝止まっている時）は、ガイドマスを表示する
		//★ if (URun == false && canGoAhead == true) {
//		if (URun == true) { 
//			UniPos = unitychan.transform.position;
//			this.transform.position = new Vector3 (UniPos.x, this.transform.position.y, UniPos.z);

		Vector3 NGP = Uscript.NextPos;
//		Debug.Log ("NGP.x 変更前 :"+NGP.x);
//		NGP.x = Mathf.CeilToInt  (NGP.x);
		//Mathf.Ceil(value / multiple) * multiple;
//		NGP.x = Mathf.Ceil ( ((NGP.x)/3)*3);
		//Mathf.Round(value / multiple) * multiple; //五捨六入的
		NGP.x = Mathf.RoundToInt ( ((NGP.x)/3)*3);

//		Debug.Log ("NGP.x 変更後 :"+NGP.x);
		NGP.z = Mathf.RoundToInt ( ((NGP.z)/3)*3);
//		Debug.Log ("NGP.z 変更後 :"+NGP.z);

		NextGuidePos = NGP;


//		NextGuidePos += (new Vector3 (0, 0, 1.5f));
			transform.DOLocalMove (NextGuidePos, 0.2f);


//		}

	}

	//####################################  other  ####################################

	//	void OnCollisionStay(Collider other){
	//	void OnCollisionEnter(Collider other){
	//	void OnTriggerEnter(Collision other){
//	void OnTriggerEnter(Collider other){
		//	void OnTriggerStay(Collider other){
//		if (other.gameObject.tag == "Tree"){
			//		if (other.gameObject.name == "TreePrefab"){
//			canGoAhead = false;
//			Debug.Log ("木にぶつかるよ");
//		}
//	}

	//	void OnCollisionExit(Collider other){
//	void OnTriggerExit(Collider other){
		//	void OnTriggerExit(Collision other){
		//	void OnTriggerEnter(Collision other){
		//		if (other.gameObject.tag == "Tree"){
//		canGoAhead = true;
//		Debug.Log ("木は無いよ");
		//		}
//	}

	//#################################################################################

}
// End