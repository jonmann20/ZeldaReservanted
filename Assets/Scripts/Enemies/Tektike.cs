using UnityEngine;
using System.Collections;

public class Tektike : Enemy {

	public Sprite spr_ground;
	public Sprite spr_air;

	int timer = 10;
	float fractionCovered = 1.0f;
	Vector3 destination;

	void Awake()
	{
		Movement();
	}

	public override void Movement(){
		Vector3 newPlace = transform.position + pickRandom();

		//enforce new place is within room. (WARNING: SLOW!)
		while(true)
		{
			if(newPlace.x > -6.5f && newPlace.x < 6.5f
			   && newPlace.y > -6 && newPlace.y < 2)
				break;
			newPlace = transform.position + pickRandom();
		}
		destination = newPlace;
		fractionCovered = 0.0f;
	}

	Vector3 pickRandom(){
		return new Vector3((int)Random.Range(-3,3), (int)Random.Range(-3,3));
	}

	void Update(){
		if(fractionCovered < 1.0f)
		{
			fractionCovered += 0.05f;
		}

		//ANIMATE
		if(fractionCovered < 1.0f || (timer > 60 && timer < 100))
			(renderer as SpriteRenderer).sprite = spr_air;
		else
			(renderer as SpriteRenderer).sprite = spr_ground;

		transform.position = Vector3.Lerp(transform.position, destination, fractionCovered);

		timer --;
		if(timer <= 0)
		{
			Movement();
			timer = 15 + (int)Random.Range(0, 240);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			--Link.health;
			Link.updateHealth();
		}
	}
}
