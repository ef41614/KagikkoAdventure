using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

	private GameObject gameManager;
	public GameManager GMScript;
	public GameObject parentObject;
	Renderer rend;
	Color color;
	float alpha;

	//☆################☆################  Start  ################☆################☆

	void Start () {
		gameManager = GameObject.Find ("GameManager");
		GMScript = gameManager.GetComponent<GameManager> ();
		GMScript.getPositionInfo();
		transform.position = GMScript.appearPosition;
		rend = GetComponentInChildren<Renderer> ();
		alpha = 0;
	}

	//####################################  Update  ###################################

	void Update () {
		gameObject.transform.rotation = Quaternion.Euler(45, 0, 0);
	}

	//####################################  other  ####################################

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			parentObject = other.gameObject;
			GMScript.GetKey ();
			GMScript.CreateTreasure ();
			this.gameObject.transform.parent = parentObject.transform;
//			transform.position = new Vector3( 0.0f, 1.0f, 0.0f);
			float Size = 0.05f;
			this.transform.localScale = new Vector3(Size, Size, Size);
			transform.localPosition = new Vector3( 0.0f, 1.8f, 0.0f);
			rend.material.color = new Color(0, 0, 0, 150);
//			transform.Translate (0, 0, 0);
//			Destroy (this.gameObject);
		}
	}
	//#################################################################################

}
// End