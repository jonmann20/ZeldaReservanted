﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Octorok : Enemy {
	
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
	GameObject RockShot;

	Vector2 destination;
	const float speed = 0.025f;

	public delegate void Callback();

	void Start()
	{
		RockShot = Resources.Load("Enemies/RockShot") as GameObject;
		poofTimer = 15 + (int)Random.Range(0, 60);
	}

	public override void customUpdate()
	{
		if(poofTimer > 0)
		{
			poofTimer --;
			(GetComponent<Renderer>() as SpriteRenderer).sprite = poof;
		}
		if(poofTimer == 0 && !didPoof)
		{
			(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_n1;
			Movement ();
			didPoof = true;
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
					availableMoves.Add('e');
			}
			if(isTileTraversableLand(southPos))
			{
				availableMoves.Add('s');
				if(previousMove != null && previousMove == 's')
					availableMoves.Add('s');
			}
			if(isTileTraversableLand(westPos))
			{
				availableMoves.Add('w');
				if(previousMove != null && previousMove == 'w')
					availableMoves.Add('w');
			}
			if(isTileTraversableLand(northPos))
			{
				availableMoves.Add('n');
				if(previousMove != null && previousMove == 'n')
					availableMoves.Add('n');
			}

			char desiredDir = 'z';
			if(availableMoves.Count > 0)
				desiredDir = getRandomElementInList(availableMoves);
			dir = desiredDir;

			if(desiredDir == 'e')
			{
				dest = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
				(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_e1;
				currentCoordsInRoom.x ++;
			}
			else if(desiredDir == 's')
			{
				dest = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
				(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_s1;
				currentCoordsInRoom.y ++;
			}
			else if(desiredDir == 'w')
			{
				dest = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
				(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_w1;
				currentCoordsInRoom.x --;
			}
			else if(desiredDir == 'n')
			{
				dest = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
				(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_n1;
				currentCoordsInRoom.y --;
			}
			dir = desiredDir;
			previousMove = desiredDir;

			StartCoroutine(MoveToPosition(transform, dest, 0.5f));
		}
	}

	public IEnumerator MoveToPosition(Transform tForm, Vector3 newPos, float time){
		float elapsedTime = 0;
		Vector3 startingPos = tForm.position;
		
		while (elapsedTime < time){
			tForm.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			
			if(elapsedTime >= time){
				Callback call = MoveAgain;
				call();
			}
			
			yield return null;
		}
	}

	public IEnumerator ShootBullet(float time){
		float elapsedTime = 0;

		GameObject go = Instantiate(RockShot, transform.position, Quaternion.identity) as GameObject;
		if(dir == 'n')
			go.GetComponent<Rigidbody2D>().velocity = new Vector3(0, SHOT_SPEED, 0);
		else if(dir == 'e')
			go.GetComponent<Rigidbody2D>().velocity = new Vector3(SHOT_SPEED, 0, 0);
		else if(dir == 's')
			go.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -SHOT_SPEED, 0);
		else if(dir == 'w')
			go.GetComponent<Rigidbody2D>().velocity = new Vector3(-SHOT_SPEED, 0, 0);

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
