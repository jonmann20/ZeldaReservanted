using UnityEngine;
using System.Collections;

public class Tektike : Enemy {

	public Sprite spr_ground;
	public Sprite spr_air;

	int timer = 10;
	float fractionCovered = 1.0f;

	Vector3 destination;
	Vector3 midPoint;

	void Start()
	{
		setHealth(1);
		poofTimer = 15 + (int)Random.Range(0, 60);
		print(poof);
	}

	public override void customUpdate()
	{
		//POOFING
		if(poofTimer > 0)
		{
			poofTimer --;
			(GetComponent<Renderer>() as SpriteRenderer).sprite = poof;
		}
		if(poofTimer == 0 && !didPoof)
		{
			(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_ground;
			//Movement ();
			destination = transform.position;
			midPoint = destination;
			didPoof = true;
		}

		if(didPoof)
		{
		//NORMAL UPDATE
		if(fractionCovered < 1.0f)
		{
			fractionCovered += 0.05f;
		}
		
		//ANIMATE
		if(didPoof)
		{
			if(fractionCovered < 1.0f || (timer > 60 && timer < 100))
				(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_air;
			else
				(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_ground;
		}
		
		if(fractionCovered <= 0.5f)
			transform.position = Vector3.Lerp(transform.position, midPoint, fractionCovered / 0.5f);
		if(fractionCovered > 0.5f)
			transform.position = Vector3.Lerp(transform.position, destination, (fractionCovered - 0.5f) / 0.5f);
		
		if(didPoof)
			timer --;
		if(timer <= 0 && didPoof)
		{
			Movement();
			timer = 60 + (int)Random.Range(0, 240);
		}
		}
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
		midPoint = getMidpoint(transform.position, destination);
		fractionCovered = 0.0f;
	}

	Vector3 pickRandom(){
		return new Vector3((int)Random.Range(-3,3), (int)Random.Range(-3,3));
	}

	Vector3 getMidpoint(Vector3 currentPoint, Vector3 newPoint)
	{
		float rise = 1.0f;
		float midX = 0.0f;
		float midY = 0.0f;

		if(currentPoint.y == newPoint.y)
		{
			midX = (newPoint.x + currentPoint.x) * 0.5f;
			midY = newPoint.y + rise;
		}
		else if(currentPoint.y > newPoint.y)
		{
			midX = (currentPoint.x * (0.75f) + newPoint.x * (0.25f));
			midY = currentPoint.y + rise;
		}
		else if(currentPoint.y < newPoint.y)
		{
			midX = (currentPoint.x * (0.25f) + newPoint.x * (0.75f));
			midY = newPoint.y + rise;
		}

		return new Vector3(midX, midY, 0);
	}
}
