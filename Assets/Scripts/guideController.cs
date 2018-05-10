using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class guideController : MonoBehaviour {

	private GameObject unitychan;
	private UnityChanController Uscript;
	public Vector3 NextGuidePos;
	[SerializeField]
	RectTransform rectTran;

	//☆################☆################  Start  ################☆################☆
	void Start () {
		this.unitychan = GameObject.Find ("unitychan");
		Uscript = unitychan.GetComponent<UnityChanController>();

	}
		
	//####################################  Update  ###################################
	void Update () {

		Vector3 NGP = Uscript.NextPos;

		NGP.x = Mathf.RoundToInt ( ((NGP.x)/3)*3);
		NGP.z = Mathf.RoundToInt ( ((NGP.z)/3)*3);

		NextGuidePos = NGP;
		transform.position = NextGuidePos;
	}

	//####################################  other  ####################################

	public void ToUnderGround(){
		Vector3 underGround = new Vector3 (this.transform.position.x, this.transform.position.y - 50, this.transform.position.y);
		transform.DOLocalMove (underGround, 0.1f);
	}

	//#################################################################################

}
// End