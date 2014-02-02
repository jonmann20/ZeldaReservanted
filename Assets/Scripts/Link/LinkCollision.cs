using UnityEngine;
using System.Collections;

/*
	This is the collision component of Link.  It checks for collision between link, the tiles, and the enemies.
*/

public partial class Link : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col){

//		print (col.gameObject.tag);
//		if(col.gameObject.tag == "Enemy"){
//			// TODO: move link backwards
//
//			--Link.health;
//			Link.updateHealth();
//		}

	}
}