using UnityEngine;
using System.Collections;

public class FireProjectileScript : MonoBehaviour {
	
	public Sprite spr1;
	public Sprite spr2;

	const int animationTime = 5;
	int animationTimer = animationTime;

	int lifeTimer = 300;
	
	void Start()
	{
		rigidbody2D.velocity = new Vector3(-3, -3, 0);
	}

	void Update(){
		lifeTimer --;
		if(lifeTimer <= 0)
			Destroy(gameObject);

		animationTimer --;
		if(animationTimer <= 0)
		{
			animationTimer = animationTime;
			if(animationTimer < animationTime * 0.5f)
				(renderer as SpriteRenderer).sprite = spr1;
			else
				(renderer as SpriteRenderer).sprite = spr2;
		}

		Vector3 newVel = rigidbody2D.velocity;

		if(transform.position.x > 5.0f)
		{
			newVel.x = -newVel.x;
		}

		if(transform.position.y < -4.5f)
		{
			newVel.y = -newVel.y;
		}
		if(transform.position.x < -5.0f)
		{
			newVel.x = -newVel.x;
		}
		if(transform.position.y > 0.5f)
		{
			newVel.y = -newVel.y;
		}

		rigidbody2D.velocity = newVel;
	}
	void OnBecameInvisible(){
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			Link.health --;
			Link.invincibility = 60;
			GameAudio.playPlayerHurt();
			Link.updateHealth();
			Destroy(gameObject);
		}
	}
}