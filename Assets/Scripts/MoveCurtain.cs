using UnityEngine;
using System.Collections;

public class MoveCurtain : MonoBehaviour {

	bool isMoved = false;
	GameObject lnk;
	Link lnkScript;

	void Start () {
		int firstRun = 0;

		foreach(Transform child in transform){
			StartCoroutine(MoveToPosition(
				child,
				child.position + new Vector3((firstRun++ == 0) ? -8.25f : 8.25f, 0),
				1f
			));
		}

		lnk = GameObject.Find("Link");
		lnkScript = lnk.GetComponent<Link>();
	}

	IEnumerator MoveToPosition(Transform tForm, Vector3 newPos, float time){
		float elapsedTime = 0;
		Vector3 startingPos = tForm.position;
		
		while (elapsedTime < time){
			tForm.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
			elapsedTime += Time.deltaTime;

			if(elapsedTime >= time && !isMoved){
				doneMoving();
			}
			
			yield return null;
		}
	}

	void doneMoving(){
		isMoved = true;
		
		GameObject mainCam = GameObject.Find("MainCamera");
		mainCam.audio.Play();
		
		lnk.GetComponent<SpriteRenderer>().enabled = true;
		
		// TODO: allow setting links movement to false
		//lnkScript.setMovementEnabled(true);

		foreach(Transform child in transform){
			Destroy (child.gameObject);
		}
	}
}
