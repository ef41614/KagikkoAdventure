using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public AudioClip getKeySE;

	private AudioSource audioSource;

	//☆################☆################  Start  ################☆################☆
	// Use this for initialization
	void Start () {
		audioSource = this.gameObject.GetComponent<AudioSource> ();

	}


	//####################################  Update  ###################################
	// Update is called once per frame
	void Update () {


	}

	//####################################  other  ####################################

	public void GetKey(){
		audioSource.PlayOneShot (getKeySE);
	}

	//#################################################################################

}
// End