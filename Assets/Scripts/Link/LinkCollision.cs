using UnityEngine;
using System.Collections;

/*
	This is the collision component of Link.  It checks for collision between link, the tiles, and the enemies.
*/

public partial class Link : MonoBehaviour {

		void OnCollisionEnter2D(Collision2D col){
			//transform.Translate(-1, 0, 0);
			//transform.position = previousPos;
	
//			Debug.Log("Collision2D");
//	
//			if(col.gameObject.tag == "wall"){
//				speed = 0;
//			}
		}
}