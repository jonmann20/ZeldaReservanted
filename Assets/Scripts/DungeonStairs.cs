using UnityEngine;
using System.Collections;

public class DungeonStairs : MonoBehaviour {
	public bool isRoom8 = false;
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){

			if(isRoom8){
				Dungeon.that.changeRoom(SpriteDir.DOWN_STEP);
			}
			else {
				Dungeon.that.changeRoom(SpriteDir.UP_STEP);
			}
		}
	}
}
