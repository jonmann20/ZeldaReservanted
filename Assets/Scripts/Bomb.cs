using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public static bool isPickup = true;

	void Start () {
		if(!isPickup){
			// play audio set
			blowUp();
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(isPickup && col.gameObject.tag == "Player"){
			GameObject.Find("HUDbombAction").GetComponent<SpriteRenderer>().enabled = true;

			Link.numBomb += 5;
			GUIText gt = GameObject.Find ("bombNum").GetComponent<GUIText>();
			gt.text = Link.numBomb.ToString();
			Destroy(gameObject);
			GameAudio.playRupeePickup(1);
		}
	}


	void OnCollisionEnter2D(Collision2D col){
		if(!isPickup){
			if(col.gameObject.tag == "Enemy"){
				col.gameObject.GetComponent<Enemy>().kill();
			}
		}
	}

	void blowUp(){
		// change animation
		// make collider NOT a trigger, and scale to blast radius
		// destroy
	}
}
