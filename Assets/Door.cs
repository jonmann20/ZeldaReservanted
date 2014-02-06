using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public static SpriteDir doorSide;

	void Awake(){

		doorSide = SpriteDir.LEFT;
	}

	void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.tag == "Player"){
			Dungeon.that.changeRoom(doorSide);
		}
	}
}
