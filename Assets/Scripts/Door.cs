using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public SpriteDir doorSide;
	public bool isEntrance = false;		// NOTE: roomRBL is hardcoded with this info

	void OnTriggerEnter2D(Collider2D col){
		if(isEntrance){
			Application.LoadLevel("main");
		}
		else if(col.gameObject.tag == "Player"){
			Dungeon.that.changeRoom(doorSide);
		}
	}
}
