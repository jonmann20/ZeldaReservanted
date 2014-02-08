using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LynelScript : Enemy {

	public bool isWalking = false;
	bool walkMode = false;

	const int animateTime = 10;
	int animateTimer = animateTime;

	public Sprite spr_n1;
	public Sprite spr_n2;
	public Sprite spr_e1;
	public Sprite spr_e2;
	public Sprite spr_s1;
	public Sprite spr_s2;
	public Sprite spr_w1;
	public Sprite spr_w2;
	
	//n, e, s, or w.
	public char dir = 'n';
	char previousMove;
	
	const float SHOT_SPEED = 10;
	GameObject ThrownWhiteSword;
	
	Vector2 destination;
	const float speed = 0.025f;
	
	public delegate void Callback();
	
	void Start()
	{
		ThrownWhiteSword = Resources.Load("Enemies/ThrownWhiteSword") as GameObject;
		poofTimer = 15 + (int)Random.Range(0, 60);
	}
	
	public override void customUpdate()
	{
		if(poofTimer > 0)
		{
			poofTimer --;
			(renderer as SpriteRenderer).sprite = poof;
		}
		if(poofTimer == 0 && !didPoof)
		{
			(renderer as SpriteRenderer).sprite = spr_n1;
			Movement ();
			didPoof = true;
		}

		if(animateTimer > 0)
			animateTimer --;
		else
		{
			animateTimer = animateTime;
			walkMode = !walkMode;
		}

		if(isWalking)
		{
			if(walkMode)
			{
				if(dir == 'e') (renderer as SpriteRenderer).sprite = spr_e1;
				if(dir == 's') (renderer as SpriteRenderer).sprite = spr_s1;
				if(dir == 'w') (renderer as SpriteRenderer).sprite = spr_w1;
				if(dir == 'n') (renderer as SpriteRenderer).sprite = spr_n1;
			}
			else
			{
				if(dir == 'e') (renderer as SpriteRenderer).sprite = spr_e2;
				if(dir == 's') (renderer as SpriteRenderer).sprite = spr_s2;
				if(dir == 'w') (renderer as SpriteRenderer).sprite = spr_w2;
				if(dir == 'n') (renderer as SpriteRenderer).sprite = spr_n2;
			}
		}

	}
	
	public override void Movement(){
		
		Vector3 dest = new Vector3(0, 0, 0);
		
		//SHOOT
		if(Random.Range(0, 100) > 85)
			StartCoroutine(ShootBullet(Random.Range(0.0f, 2.0f)));
		else //MOVE
		{
			//Get Available Moves
			List<char> availableMoves = new List<char>();
			
			Vector2 eastPos = new Vector2(currentCoordsInRoom.x + 1, currentCoordsInRoom.y);
			Vector2 southPos = new Vector2(currentCoordsInRoom.x, currentCoordsInRoom.y + 1);
			Vector2 westPos = new Vector2(currentCoordsInRoom.x - 1, currentCoordsInRoom.y);
			Vector2 northPos = new Vector2(currentCoordsInRoom.x, currentCoordsInRoom.y - 1);
			
			if(isTileTraversableLand(eastPos))
			{
				availableMoves.Add('e');
				if(previousMove != null && previousMove == 'e')
				{
					availableMoves.Add('e');
					availableMoves.Add('e');
					availableMoves.Add('e');
				}
			}
			if(isTileTraversableLand(southPos))
			{
				availableMoves.Add('s');
				if(previousMove != null && previousMove == 's')
				{
					availableMoves.Add('s');
					availableMoves.Add('s');
					availableMoves.Add('s');
				}
			}
			if(isTileTraversableLand(westPos))
			{
				availableMoves.Add('w');
				if(previousMove != null && previousMove == 'w')
				{
					availableMoves.Add('w');
					availableMoves.Add('w');
					availableMoves.Add('w');
				}
			}
			if(isTileTraversableLand(northPos))
			{
				availableMoves.Add('n');
				if(previousMove != null && previousMove == 'n')
				{
					availableMoves.Add('n');
					availableMoves.Add('n');
					availableMoves.Add('n');
				}
			}
			
			char desiredDir = 'z';
			if(availableMoves.Count > 0)
				desiredDir = getRandomElementInList(availableMoves);
			dir = desiredDir;
			
			if(desiredDir == 'e')
			{
				dest = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
				currentCoordsInRoom.x ++;
			}
			else if(desiredDir == 's')
			{
				dest = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
				currentCoordsInRoom.y ++;
			}
			else if(desiredDir == 'w')
			{
				dest = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
				currentCoordsInRoom.x --;
			}
			else if(desiredDir == 'n')
			{
				dest = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
				currentCoordsInRoom.y --;
			}
			dir = desiredDir;
			previousMove = desiredDir;
			
			StartCoroutine(MoveToPosition(transform, dest, 0.5f));
		}
	}
	
	public IEnumerator MoveToPosition(Transform tForm, Vector3 newPos, float time){
		isWalking = true;
		float elapsedTime = 0;
		Vector3 startingPos = tForm.position;
		
		while (elapsedTime < time){
			tForm.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			
			if(elapsedTime >= time){
				isWalking = false;
				Callback call = MoveAgain;
				call();
			}
			
			yield return null;
		}
	}
	
	public IEnumerator ShootBullet(float time){
		float elapsedTime = 0;

		Quaternion rot = Quaternion.identity;
		Vector3 vel = new Vector3(0, 0, 0);

		if(dir == 'n')
		{
			vel = new Vector3(0, SHOT_SPEED, 0);
			rot = Quaternion.Euler(0, 0, 270);
		}
		else if(dir == 'e')
		{
			vel = new Vector3(SHOT_SPEED, 0, 0);
			rot = Quaternion.Euler(0, 0, 180);
		}
		else if(dir == 's')
		{
			vel = new Vector3(0, -SHOT_SPEED, 0);
			rot = Quaternion.Euler(0, 0, 90);
		}
		else if(dir == 'w')
		{
			vel = new Vector3(-SHOT_SPEED, 0, 0);
			rot = Quaternion.Euler(0, 0, 0);
		}

		GameObject go = Instantiate(ThrownWhiteSword, transform.position, rot) as GameObject;
		go.rigidbody2D.velocity = vel;

		while (elapsedTime < time){
			elapsedTime += Time.deltaTime;
			
			if(elapsedTime >= time){
				Callback call = MoveAgain;
				call();
			}
			
			yield return null;
		}
	}
	
	public void MoveAgain()
	{
		Movement();
	}
}