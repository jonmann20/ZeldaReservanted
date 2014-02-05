using UnityEngine;
using System.Collections;

public class Octorok : Enemy {
	
	public Sprite spr_n1;
	
	int timer = 10;
	float fractionCovered = 1.0f;
	Vector3 destination;
	Vector3 midPoint;

	int xInRoom = 0;
	int yInRoom = 0;
	
	void Start()
	{
		Movement();
	}
	
	public override void Movement(){

		Vector3 newPlace = transform.position + pickRandom();
		
		//enforce new place is within room. (WARNING: SLOW!)
		//while(true)
		//{
			//if(newPlace.x > -6.5f && newPlace.x < 6.5f
			   //&& newPlace.y > -6 && newPlace.y < 2)
				//break;
			newPlace = transform.position + pickRandom();
		//}
		destination = newPlace;
		midPoint = getMidpoint(transform.position, destination);
		fractionCovered = 0.0f;
	}
	
	Vector3 pickRandom(){
		int dx = 0;
		int dy = 0;

		//Move Horizontal
		if(Random.Range(0, 1) > 0.5f)
		{
			dx = (int)Mathf.Round(Random.Range(-14, 14));
		}
		else //Move Vertical
		{
			dy = (int)Mathf.Round(Random.Range(-9, 9));
		}

		return new Vector3(dx, dy, 0);
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
	
	void Update(){

	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			--Link.health;
			Link.updateHealth();
		}
	}
}
