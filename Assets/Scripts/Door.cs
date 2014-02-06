using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public SpriteDir doorSide;

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			Dungeon.that.changeRoom(doorSide);
		}
	}
}
