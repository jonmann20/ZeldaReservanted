using UnityEngine;
using System.Collections;

public class BossScript : Enemy {

	public Sprite spr_norm;
	public Sprite spr_angry;

	public float firePosX = 13;
	public float firePosY = 4;
	float sinCounter = 0;
	const int FLAME_FREQUENCY = 120;
	int flameTimer;
	//0 = intro
	//1 = vulnerable, move up and down

	const int SHOT_FREQUENCY = 120;
	int shotTimer;
	int anger = 0;

	GameObject FireProjectile;
	GameObject RockShot;
	public int state = 0;


	public GameObject linkRef;

	// Use this for initialization
	void Start () {
		linkRef = GameObject.FindWithTag("Player");

		health = 11;
		FireProjectile = Resources.Load("Enemies/FireProjectile") as GameObject;
		RockShot = Resources.Load("Enemies/RockShot") as GameObject;
		transform.position = new Vector3(25, -8f, 0);
		flameTimer =  FLAME_FREQUENCY + Random.Range(0, FLAME_FREQUENCY);
	}

	public override void Movement()
	{

	}

	void OnDestroy()
	{
		print("EPONA DIDED!");
		DungeonRooms.that.addStairsBoss();
	}

	// Update is called once per frame
	public override void customUpdate() {

		if(state == 0)
		{
			sinCounter += 0.5f;
			float xPos = transform.position.x - 0.06f;
			float yPos = -8 + Mathf.Sin(sinCounter);

			transform.position = new Vector3(xPos, yPos, 0);

			if(transform.position.x < 15)
				state = 1;
			return;
		}

		if(state == 1)
		{
			flameTimer --;
			if(flameTimer <= 0)
			{
				flameTimer = FLAME_FREQUENCY + Random.Range(0, FLAME_FREQUENCY);
				Vector3 pos = new Vector3(transform.position.x - firePosX, transform.position.y + firePosY, 0);
				GameObject go = Instantiate(FireProjectile, pos, Quaternion.identity) as GameObject;
				go.transform.parent = GameObject.Find("EnemyHolder").transform;
			}

			//MOVEMENT
			sinCounter += 0.05f;
			float xPos = 15 + Mathf.Sin(sinCounter * 0.5f);
			float yPos = -8 + Mathf.Sin(sinCounter);
			
			transform.position = new Vector3(xPos, yPos, 0);
			
			if(health <= 8)
			{
				state = 2;
				shotTimer = 60;
			}
			return;
		}

		if(state == 2)
		{
			shotTimer --;
			if(shotTimer <= 0)
			{
				Vector3 pos = new Vector3(transform.position.x - 13, transform.position.y + 6, 0);
				Vector3 vel = linkRef.transform.position - pos;
				vel.Normalize();
				vel *= 3;
				
				GameObject go = Instantiate(RockShot, pos, Quaternion.identity) as GameObject;
				go.rigidbody2D.velocity = vel;
				go.SendMessage("setMode", 1);

				go.transform.parent = GameObject.Find("EnemyHolder").transform;
			}
			if(shotTimer <= -10)
				shotTimer = SHOT_FREQUENCY;


			//MOVEMENT
			sinCounter += 0.1f;
			float xPos = 15 + Mathf.Sin(sinCounter * 0.5f);
			float yPos = -8 + Mathf.Sin(sinCounter);
			
			transform.position = new Vector3(xPos, yPos, 0);
			if(health <= 5)
				state = 3;
			return;
		}

		//CHARGE!
		if(state == 3)
		{
			(renderer as SpriteRenderer).sprite = spr_angry;

			//MOVEMENT
			float xPos = transform.position.x;
			if(anger == 0)
			{
				sinCounter += 0.2f;
				if(transform.position.x > -5)
					xPos = transform.position.x - 0.02f;
			}
			if(anger == 1)
			{
				sinCounter += 0.3f;
				if(transform.position.x > -5)
					xPos = transform.position.x - 0.06f;
			}
			if(anger == 2)
			{
				sinCounter += 0.4f;
				if(transform.position.x > -5)
					xPos = transform.position.x - 0.1f;
			}
			if(anger == 3)
			{
				sinCounter += 0.5f;
				if(transform.position.x > -5)
					xPos = transform.position.x - 0.14f;
			}
			if(anger == 4)
			{
				sinCounter += 0.6f;
				if(transform.position.x > -5)
					xPos = transform.position.x - 0.18f;
			}

			float yPos = -8 + Mathf.Sin(sinCounter);
			transform.position = new Vector3(xPos, yPos, 0);

			return;
		}

		//REELING
		if(state == 4)
		{
			float xPos = transform.position.x + 0.5f;
			transform.position = new Vector3(xPos, transform.position.y, 0);
			if(xPos > 15)
				state = 3;
			return;
		}
	}

	public override void justHurt()
	{
		if(state == 3)
		{
			state = 4;
			anger ++;
		}
	}
}
