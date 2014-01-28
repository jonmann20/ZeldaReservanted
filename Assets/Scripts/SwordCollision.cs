using UnityEngine;
using System.Collections;

public class SwordCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Enemy"){
			Enemy enemy = col.gameObject.GetComponent<Enemy>();
			enemy.kill();
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
