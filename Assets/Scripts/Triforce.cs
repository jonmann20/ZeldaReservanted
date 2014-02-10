using UnityEngine;
using System.Collections;

public class Triforce : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col){

		GameObject.Find ("MainCamera").GetComponent<AudioSource>().enabled = false;
		(GameObject.Find ("Link") as GameObject).SendMessage("executeItemPose");
		transform.position = new Vector3(-0.4f, 0.0f, 0);
		isCollected = true;
		GameAudio.playTriforce();

		// TODO: item pose here
		Link.that.setMovementEnabled(false);
		Invoke("goBackToMain", 8f);
	}

	void goBackToMain(){
		Link.that.setMovementEnabled(true);
		Application.LoadLevel("main");
	}
}
