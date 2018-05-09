using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class guideController : MonoBehaviour {

	private GameObject unitychan;
	private float difference;
	private UnityChanController Uscript;
	private GameObject dirR;
	private GameObject dirL;
	private GameObject dirF;
	private GameObject dirB;
	private bool canGoAhead = true;
	private Vector3 UniPos;
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

//		bool URun = Uscript.UIsRunning; 

		Vector3 NGP = Uscript.NextPos;

		NGP.x = Mathf.RoundToInt ( ((NGP.x)/3)*3);
		NGP.z = Mathf.RoundToInt ( ((NGP.z)/3)*3);

		NextGuidePos = NGP;

//		Vector3 underGround = new Vector3 (this.transform.position.x, this.transform.position.y - 5, this.transform.position.y);
//		transform.DOLocalMove (NextGuidePos, 0.2f);
//		transform.DOLocalMove (underGround, 0.01f);
		transform.position = NextGuidePos;
	}

	//####################################  other  ####################################

	public void ToUnderGround(){
		Vector3 underGround = new Vector3 (this.transform.position.x, this.transform.position.y - 5, this.transform.position.y);
		transform.DOLocalMove (underGround, 0.3f);
	}

	//#################################################################################

}
// End