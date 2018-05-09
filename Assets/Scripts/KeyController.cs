using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

	public enum APPEAR_POS
	{
		NORTH_L,
		NORTH_C,
		NORTH_R,
		CENTER_L,
		CENTER_C,
		CENTER_R,
		SOUTH_L,
		SOUTH_C,
		SOUTH_R,
	};

	private APPEAR_POS appearPositon = APPEAR_POS.CENTER_C;
	private Vector3 key_pos;
	private int rnd = 1;

	private GameObject gameManager;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		rnd = Random.Range(1, 9);

		switch (rnd) {
		case 1:
			key_pos = new Vector3 (-24, 0, 27);
			Debug.Log("カギはNORTH_L");
			break;

		case 2:
			key_pos = new Vector3 (-3, 0, 27);
			Debug.Log("カギはNORTH_C");
			break;

		case 3:
			key_pos = new Vector3 (21, 0, 27);
			Debug.Log("カギはNORTH_R");
			break;

		case 4:
			key_pos = new Vector3 (-24, 0, 6);
			Debug.Log("カギはCENTER_L");
			break;

		case 5:
			key_pos = new Vector3 (0, 0, 12);
			Debug.Log("カギはCENTER_C");
			break;

		case 6:
			key_pos = new Vector3 (21, 0, 9);
			Debug.Log("カギはCENTER_R");
			break;

		case 7:
			key_pos = new Vector3 (-24, 0, -24);
			Debug.Log("カギはSOUTH_L");
			break;

		case 8:
			key_pos = new Vector3 (-6, 0, -9);
			Debug.Log("カギはSOUTH_C");
			break;

		case 9:
			key_pos = new Vector3 (21, 0, -18);
			Debug.Log("カギはSOUTH_R");
			break;
		}
		//		key_pos = new Vector3 (3, 0, 6);
		//		transform.Translate(new Vector3 (3, 0, -3));
				transform.position = key_pos;

	}


	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {


	}

	//####################################  other  ####################################

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			gameManager.GetComponent<GameManager> ().GetKey ();
			Destroy (this.gameObject);
		}
	}
	//#################################################################################

}
// End