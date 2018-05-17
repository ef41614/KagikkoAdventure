using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {

	public Sprite 画像1;
	public Sprite 画像2;

	private GameObject obj;
	private Image image;

	//☆################☆################  Start  ################☆################☆

	void Start () {
		obj = GameObject.Find("image(imageオブジェクトの名前)").gameObject as GameObject;
		image = obj.GetComponent<Image> ();

	}


	//####################################  Update  ###################################

	void Update () {


	}

	//####################################  other  ####################################

	public void 画像変更(){
		image.sprite = 画像2;
	}

	//#################################################################################

}
// End