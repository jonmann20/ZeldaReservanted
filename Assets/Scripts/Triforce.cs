using UnityEngine;
using System.Collections;

public class Triforce : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){

		GameObject.Find ("MainCamera").GetComponent<AudioSource>().enabled = false;
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
