using UnityEngine;
using System.Collections;

public class RockScript : Enemy {

	bool fallLeft = true;
	Vector3 startingPosition;
	const int waitTime = 30;
	int waitTimer = waitTime;
	const int respawnTime = 150;
	const int dirTime = 25;
	const int upTime = 5;
	const float horSpeed = 0.05f;
	const float vertSpeed = 0.1f;
	int lifeTimer = respawnTime;
	int dirTimer = dirTime;
	int upTimer = upTime;

	const int animateTime = 10;
	int animateTimer = animateTime;
	bool isSprite1 = true;

	public Sprite spr_1;
	public Sprite spr_2;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}
	
	void Update(){
		-- waitTimer;

		if(waitTimer <= 0)
			Movement();

		-- animateTimer;
		if(animateTimer <= 0)
		{
			animateTimer = animateTime;
			if(isSprite1)
			{
				(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_1;
				isSprite1 = false;
			}
			else
			{
				(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_2;
				isSprite1 = true;
			}
		}
	}

	public override void Movement()
	{
		-- upTimer;

		-- dirTimer;
		if(dirTimer <= 0)
		{
			print("switch");
			dirTimer = dirTime;
			upTimer = upTime;
			if(Random.Range(0, 100) > 50)
				fallLeft = !fallLeft;
		}
		
		if(fallLeft)
		{
			if(upTimer <= 0)
				transform.Translate(-horSpeed, -vertSpeed, 0);
			else
				transform.Translate(-horSpeed, vertSpeed, 0);
		}
		else if(!fallLeft)
		{
			if(upTimer <= 0)
				transform.Translate(horSpeed, -vertSpeed, 0);
			else
				transform.Translate(horSpeed, vertSpeed, 0);
		}
		
		-- lifeTimer;
		if(lifeTimer <= 0)
		{
			lifeTimer = respawnTime + Random.Range(0, 50);
			transform.position = startingPosition;
			waitTimer = waitTime + Random.Range(0, 20);

			print(Random.Range(0, 1));
			if(Random.Range(0, 100) > 50)
			{
				fallLeft = true;
			}
			else
				fallLeft = false;
		}
	}


}
