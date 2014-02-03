using UnityEngine;
using System.Collections;

public class MoveCurtain : MonoBehaviour {

	static public bool isMoved = false;
	GameObject lnk;
	Link lnkScript;

	void Start () {
		int firstRun = 0;

		foreach(Transform child in transform){
			StartCoroutine(Utils.that.MoveToPosition(
				child,
				child.position + new Vector3((firstRun++ == 0) ? -8.25f : 8.25f, 0),
				1f,
				doneMoving
			));
		}

		lnk = GameObject.Find("Link");
		lnkScript = lnk.GetComponent<Link>();
	}

	void doneMoving(){
		if(isMoved) return;	// prevents double call (1 for each curtain)
		
		GameObject mainCam = GameObject.Find("MainCamera");
		mainCam.audio.Play();
		
		lnk.GetComponent<SpriteRenderer>().enabled = true;
		
		// TODO: allow setting links movement to false
		//lnkScript.setMovementEnabled(true);

		foreach(Transform child in transform){
			Destroy (child.gameObject);
		}

		isMoved = true;
	}
}
