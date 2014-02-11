using UnityEngine;
using System.Collections;

public class HeartItemDrop : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "Sword"){
			if(Link.health < Link.initHealth){
				++Link.health;
				Link.updateHealth();
			}

			GameAudio.playHeartPickup();

			Destroy(gameObject);
		}
	}
}
