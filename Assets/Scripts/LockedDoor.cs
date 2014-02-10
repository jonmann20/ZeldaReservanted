using UnityEngine;
using System.Collections;

public class LockedDoor : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player" && Link.numKey >= 1){
			--Link.numKey;

			GUIText gt = GameObject.Find ("keyNum").GetComponent<GUIText>();
			gt.text = Link.numKey.ToString();
			GameAudio.playDoorOpened();

			DungeonRooms.that.doorIsOpen = true;

			Destroy (gameObject);
		}
	}
}
