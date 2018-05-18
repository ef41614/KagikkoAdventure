using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {

	public float alfa;
	float speed = 0.05f;
	float red, green, blue;
	public bool goFadeIn = false;
	public bool goFadeOut = false;

	//☆################☆################  Start  ################☆################☆
	void Start () {
		red = GetComponent<Image>().color.r;
		green = GetComponent<Image>().color.g;
		blue = GetComponent<Image>().color.b;
	}
	
	//####################################  Update  ###################################
	void Update () {
		if ((goFadeIn == true)&&(alfa>=0)) {
			GetComponent<Image> ().color = new Color (red, green, blue, alfa);
			alfa -= speed;
		}

		if ((goFadeOut == true)&&(alfa<=255)) {
			GetComponent<Image> ().color = new Color (red, green, blue, alfa);
			alfa += speed;
		}
			
	}

	//####################################  other  ####################################
	//#################################################################################

}
// End
