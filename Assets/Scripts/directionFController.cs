using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionFController : MonoBehaviour {

	private GameObject unitychan;
	UnityChanController Uscript; 
	public GameObject dirM;
	private Vector3 UniPos;
	private bool canGoAhead = true;

	//☆################☆################  Start  ################☆################☆

	void Start () {

		this.unitychan = GameObject.Find ("unitychan");
		Uscript = unitychan.GetComponent<UnityChanController>(); 
		//		this.dirM = GameObject.Find ("directionR");
		///		this.dirM = GameObject.Find ("directionL");
				this.dirM = GameObject.Find ("directionF");
		//      this.dirM = GameObject.Find ("directionB");

	}

	//####################################  Update  ###################################

	void Update () {

		bool URun = Uscript.UIsRunning; 

		// Unityちゃんが走っていない時（＝止まっている時）、
		if (URun == false){ 

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
		}
	}

	void OnTriggerExit(Collider other){
		canGoAhead = true;
	}

	//#################################################################################

}
// End