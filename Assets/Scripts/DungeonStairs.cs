using UnityEngine;
using System.Collections;

public class DungeonStairs : MonoBehaviour {
	public bool isRoom8 = false;
	public bool isRoomBoss = false;
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){

			if(isRoomBoss){
				Dungeon.that.changeRoom(SpriteDir.RIGHT_STEP);
			}
			else if(isRoom8){
				Dungeon.that.changeRoom(SpriteDir.DOWN_STEP);
			}
			else {
				Dungeon.that.changeRoom(SpriteDir.UP_STEP);
			}
		}
	}
}
