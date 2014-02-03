using UnityEngine;
using System.Collections;

public class HeartItemDrop : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			if(Link.health < Link.initHealth){
				++Link.health;
			}

			Destroy(gameObject);
			GameAudio.playHeartPickup();
		}
	}
}
