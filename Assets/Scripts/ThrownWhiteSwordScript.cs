using UnityEngine;
using System.Collections;

public class ThrownWhiteSwordScript : MonoBehaviour {

	public Sprite whiteSword;
	public Sprite arrow;

	int mode = 0;

	int stillTimer = 10;
	
	void Update(){
		if(stillTimer > 0)
			stillTimer --;
		
		if(Mathf.Abs(transform.position.x) > 9.2f || transform.position.y > 5.5f || transform.position.y < -8.5f){
			Destroy(gameObject);
		}
		if(rigidbody2D.velocity.x == 0 && rigidbody2D.velocity.y == 0 && stillTimer <= 0)
			Destroy(gameObject);
	}
	void OnBecameInvisible(){
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			if(mode == 0)
				Link.health -= 2;
			if(mode == 1)
				Link.health --;
			Link.updateHealth();
			Destroy(gameObject);
		}
		if(col.gameObject.tag == "physicaltile")
			Destroy(gameObject);
	}

	//0 = white sword, 1 = arrow
	void setMode(int m)
	{
		(renderer as SpriteRenderer).sprite = whiteSword;
		(renderer as SpriteRenderer).sprite = arrow;
	}
}
