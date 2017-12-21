using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeeverScript : Enemy {
	
	const int animationTime = 10;
	int animationTimer = animationTime;
	public Sprite spr1;
	public Sprite spr2;

	const int digTime = 180;
    public int digTimer;

	List<Vector3> traversiblePositions;
	
	//n, e, s, or w.
	public char dir = 'n';
	public char previousMove;
	
	Vector2 destination;
	const float speed = 0.025f;
	
	public delegate void Callback();

    void Start()
	{
        digTimer = digTime + Random.Range(0, digTime);
        traversiblePositions = getTraversiblePositions();
		Movement ();
	}
	
	public override void customUpdate()
	{
		if(animationTimer > 0)
			animationTimer --;
		else
			animationTimer = animationTime;

		if(animationTimer > animationTime * 0.5f)
			(GetComponent<Renderer>() as SpriteRenderer).sprite = spr1;
		else
			(GetComponent<Renderer>() as SpriteRenderer).sprite = spr2;

		if(digTimer > 0)
			digTimer --;
	}
	
	public override void Movement(){

		//HOP TO NEW PLACE
		if(digTimer <= 0)
		{
			digTimer = digTime + Random.Range(0, digTime);
			Vector3 newPos = getRandomElementInList<Vector3>(traversiblePositions);
			newPos.x += 0.5f;
			newPos.y -= 0.5f;
			transform.position = newPos;
			initCoordsInRoom();
		}

		Vector3 dest = new Vector3(0, 0, 0);
		
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

		print("dest: " + dest.ToString());
		
		char desiredDir = 'z';
		if(availableMoves.Count > 0)
			desiredDir = getRandomElementInList<char>(availableMoves);
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
