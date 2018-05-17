using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionBController : MonoBehaviour {

	private GameObject unitychan;
	UnityChanController Uscript; 
	GameObject pchan; 
	PchanController Pscript; 
	GameObject turnmanager;
	TurnManager TurnMscript;
	public GameObject dirM;
	private Vector3 UniPos;
	private bool canGoAhead = true;

	//☆################☆################  Start  ################☆################☆

	void Start () {
		this.unitychan = GameObject.Find ("unitychan");
		Uscript = unitychan.GetComponent<UnityChanController>(); 
		pchan = GameObject.Find ("pchan"); 
		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 
		Pscript = pchan.GetComponent<PchanController>(); 
		//		this.dirM = GameObject.Find ("directionR");
		//		this.dirM = GameObject.Find ("directionL");
		//		this.dirM = GameObject.Find ("directionF");
				this.dirM = GameObject.Find ("directionB");
	}

	//####################################  Update  ###################################

	void Update () {

		bool URun = Uscript.UIsRunning; 
		bool PRun = Pscript.PIsRunning;

			// Unityちゃんが走っていない時（＝止まっている時）、
			if (URun == false) { 
				// 進路クリアならばガイドマスを表示する
				if (canGoAhead == true) {
					dirM.SetActive (true);
				} else {
					dirM.SetActive (false);	
				}
			} else if (URun == true) {
				dirM.SetActive (false);	
			}
			

			// Pちゃんが走っていない時（＝止まっている時）、
			if (PRun == false) { 
				// 進路クリアならばガイドマスを表示する
				if (canGoAhead == true) {
					dirM.SetActive (true);
				} else {
					dirM.SetActive (false);	
				}
			} else if (PRun == true) {
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