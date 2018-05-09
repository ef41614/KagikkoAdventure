using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour {

	private GameObject gameManager;
	public GameManager GMScript;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		GMScript = gameManager.GetComponent<GameManager> ();
		GMScript.getPositionInfo();
		transform.position = GMScript.appearPosition;

	}


	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {


	}

	//####################################  other  ####################################

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			//gameManager.GetComponent<GameManager> ().GetKey ();
			GMScript.GetTreasure ();
			GMScript.CreateKey ();
			Destroy (this.gameObject);
		}
	}
	//#################################################################################

}
// End