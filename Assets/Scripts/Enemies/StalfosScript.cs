using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StalfosScript : Enemy {
	
	const int animationTime = 15;
	int animationTimer = animationTime;
	public Sprite spr1;
	public Sprite spr2;
	
	//n, e, s, or w.
	public char dir = 'n';
	public char previousMove;
	
	Vector2 destination;
	const float speed = 0.01f;
	
	public delegate void Callback();
	
	void Start()
	{
		Movement ();
	}
	
	public override void customUpdate()
	{	
		(GetComponent<Renderer>() as SpriteRenderer).sprite = spr1;
		if(animationTimer > 0)
			animationTimer --;
		else
			animationTimer = animationTime;

		if(animationTimer > animationTime * 0.5f)
			(GetComponent<Renderer>() as SpriteRenderer).sprite = spr1;
		else
			(GetComponent<Renderer>() as SpriteRenderer).sprite = spr2;
	}
	
	public override void Movement(){
		
		Vector3 dest = new Vector3(0, 0, 0);
		
		//Get Available Moves
		List<char> availableMoves = new List<char>();
		
		Vector2 eastPos = new Vector2(currentCoordsInRoom.x + 1, currentCoordsInRoom.y);
		Vector2 southPos = new Vector2(currentCoordsInRoom.x, currentCoordsInRoom.y + 1);
		Vector2 westPos = new Vector2(currentCoordsInRoom.x - 1, currentCoordsInRoom.y);
		Vector2 northPos = new Vector2(currentCoordsInRoom.x, currentCoordsInRoom.y - 1);
		
		if(transform.position.x < 5.0f)
		{
			availableMoves.Add('e');
			if(previousMove != null && previousMove == 'e')
			{
				availableMoves.Add('e');
				availableMoves.Add('e');
			}
		}
		if(transform.position.y > -4.5f)
		{
			availableMoves.Add('s');
			if(previousMove != null && previousMove == 's')
			{
				availableMoves.Add('s');
				availableMoves.Add('s');
			}
		}
		if(transform.position.x > -5.0f)
		{
			availableMoves.Add('w');
			if(previousMove != null && previousMove == 'w')
			{
				availableMoves.Add('w');
				availableMoves.Add('w');
			}
		}
		if(transform.position.y < 0.5f)
		{
			availableMoves.Add('n');
			if(previousMove != null && previousMove == 'n')
			{
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

		StartCoroutine(MoveToPosition(transform, dest, 0.50f));
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
	
	public void MoveAgain()
	{
		Movement();
	}
}
