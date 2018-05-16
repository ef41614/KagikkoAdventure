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
	GameObject charamovemanager;
	CharaMoveManager CharaMoveMscript;
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
		charamovemanager = GameObject.Find ("charamovemanager");
		CharaMoveMscript = charamovemanager.GetComponent<CharaMoveManager> ();
	}
		
	//####################################  Update  ###################################
	void Update () {

	}

	//####################################  other  ####################################

	public void ToUnderGround(){
		Vector3 underGround = new Vector3 (this.transform.position.x, this.transform.position.y - 50, this.transform.position.y);
		transform.DOLocalMove (underGround, 0.1f);
		Debug.Log("地面の下に行ったよ");
	}

	public void adjustNextGuidePos(){
		Debug.Log("adjustスクリプト出席確認");
//		if (TurnMscript.canMove1P == true) {
//			NGP = Uscript.NextPos;
//		} else if (TurnMscript.canMove2P == true) {
//			NGP = Pscript.NextPos;
//		}
		NGP = CharaMoveMscript.NextPos;
		Debug.Log("NGP上:"+NGP);
		NGP.x = Mathf.RoundToInt ( ((NGP.x)/3)*3);
		NGP.z = Mathf.RoundToInt ( ((NGP.z)/3)*3);
//		Debug.Log("NGP下:"+NGP);
		NextGuidePos = NGP;
//		transform.position = NGP;
		transform.DOLocalMove (NextGuidePos, 0.1f);
//		transform.position = NextGuidePos;
//		transform.Translate (NextGuidePos);
	}

	public void initializePosition(){
		
	}

	//#################################################################################

}
// End