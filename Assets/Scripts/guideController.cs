using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class guideController : MonoBehaviour {

	private GameObject unitychan;
	private UnityChanController Uscript;
	GameObject pchan; 
	PchanController Pscript; 
	GameObject turnmanager;
	TurnManager TurnMscript;
	public Vector3 NextGuidePos;
	[SerializeField]
	RectTransform rectTran;
	Vector3 NGP;

	//☆################☆################  Start  ################☆################☆
	void Start () {
		this.unitychan = GameObject.Find ("unitychan");
		Uscript = unitychan.GetComponent<UnityChanController>();
		pchan = GameObject.Find ("pchan"); 
		Pscript = pchan.GetComponent<PchanController>(); 
		turnmanager = GameObject.Find ("turnmanager");
		TurnMscript = turnmanager.GetComponent<TurnManager>(); 
	}
		
	//####################################  Update  ###################################
	void Update () {

		if (TurnMscript.canMove1P == true) {
			NGP = Uscript.NextPos;
		} else if (TurnMscript.canMove2P == true) {
			NGP = Pscript.NextPos;
		}

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