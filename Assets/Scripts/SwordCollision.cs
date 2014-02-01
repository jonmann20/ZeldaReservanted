using UnityEngine;
using System.Collections;

public class SwordCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Enemy"){
			Enemy enemy = col.gameObject.GetComponent<Enemy>();
			enemy.kill();
		}
	}

	void Update(){
		if(Mathf.Abs(transform.position.x) > 9.2f || transform.position.y > 5.5f || transform.position.y < -8.5f){
			Destroy (gameObject);
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
