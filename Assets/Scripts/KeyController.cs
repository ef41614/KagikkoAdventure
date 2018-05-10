using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

	private GameObject gameManager;
	public GameManager GMScript;

	//☆################☆################  Start  ################☆################☆

	void Start () {
		gameManager = GameObject.Find ("GameManager");
		GMScript = gameManager.GetComponent<GameManager> ();
		GMScript.getPositionInfo();
		transform.position = GMScript.appearPosition;
	}

	//####################################  Update  ###################################

	void Update () {

	}

	//####################################  other  ####################################

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			GMScript.GetKey ();
			GMScript.CreateTreasure ();
			Destroy (this.gameObject);
		}
	}
	//#################################################################################

}
// End