using UnityEngine;
using System.Collections;

public class Spikes : Enemy {

	Vector3 initialPosition;
	const float MAX_DISTANCE = 5;
	const float CHARGE_SPEED = 0.15f;
	const float REEL_SPEED = CHARGE_SPEED * 0.25f;

	//0 = idle, 1 = charging right, 2 = charging left, 3 = reeling
	int state = 0;

	// Use this for initialization
	void Start () {
		health = 100;
		initialPosition = transform.position;
	}
	
	public override void customUpdate()
	{
		//idle
		if(state == 0)
		{
			float absYDiff = Mathf.Abs(Link.that.transform.position.y - transform.position.y);
			float xDiff = Link.that.transform.position.x - transform.position.x;
			if(absYDiff < 1)
			{
				if(xDiff > 0) //charge right
					state = 1;
				else if(xDiff < 0)  //charge left
					state = 2;
			}

			return;
		}
		//charging right
		else if(state == 1)
		{
			transform.Translate(new Vector3(CHARGE_SPEED, 0, 0));
			float distanceFromInitialPos = Mathf.Abs(transform.position.x - initialPosition.x);
			if(distanceFromInitialPos > MAX_DISTANCE)
			{
				state = 3;
			}
			return;
		}
		//charging left
		else if(state == 2)
		{
			transform.Translate(new Vector3(-CHARGE_SPEED, 0, 0));
			float distanceFromInitialPos = Mathf.Abs(transform.position.x - initialPosition.x);
			if(distanceFromInitialPos > MAX_DISTANCE)
			{
				state = 3;
			}
			return;
		}
		//reeling
		else if(state == 3)
		{
			if(initialPosition.x - transform.position.x > 0)
			{
				transform.Translate(new Vector3(REEL_SPEED, 0, 0));
			}
			else if(initialPosition.x - transform.position.x < 0)
			{
				transform.Translate(new Vector3(-REEL_SPEED, 0, 0));
			}

			float distanceFromInitialPos = Mathf.Abs(transform.position.x - initialPosition.x);
			if(distanceFromInitialPos < 0.1f)
			{
				transform.position = initialPosition;
				state = 0;
			}
			return;
		}
	}


	public override void Movement(){

	}
}
