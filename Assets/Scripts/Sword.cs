using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public Sprite spr_normal_n;
	public Sprite spr_shot1_n;
	bool isSprite1 = true;

	const int animateTime = 5;
	int animateTimer = animateTime;
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Enemy"){
			if(rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0)
				Destroy(gameObject);
		}
	}
	
	void Update(){
		
		if(rigidbody2D.velocity.x != 0 || rigidbody2D.velocity.y != 0)
		{
			--animateTimer;
			if(animateTimer <= 0)
			{
				animateTimer = animateTime;
				if(!isSprite1)
				{
					(renderer as SpriteRenderer).sprite = spr_normal_n;
					isSprite1 = true;
				}
				else
				{
					(renderer as SpriteRenderer).sprite = spr_shot1_n;
					isSprite1 = false;
				}
			}
		}

		if(Mathf.Abs(transform.position.x) > 9.2f || transform.position.y > 5.5f || transform.position.y < -8.5f){
			Destroy (gameObject);
		}
	}
	
	void OnBecameInvisible(){
		Destroy(gameObject);
	}
}
