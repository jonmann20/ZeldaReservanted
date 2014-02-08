using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			++Link.numKey;

			GUIText gt = GameObject.Find ("keyNum").GetComponent<GUIText>();
			gt.text = Link.numKey.ToString();
			GameAudio.playRupeePickup(1);

			Destroy (gameObject);
		}
	}
}
