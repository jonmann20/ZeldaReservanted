using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZolaScript : Enemy {

	const int timeToMove = 120;
	int timer = timeToMove;
	List<Vector3> availablePositions;

	GameObject RockShot;

	public delegate void Callback();

	// Use this for initialization
	void Start () {
		health = 3;
		RockShot = Resources.Load("Enemies/RockShot") as GameObject;
		availablePositions = getWaterPositions();
		Movement();
	}

	public override void Movement()
	{
		Vector3 newPos = getRandomElementInList<Vector3>(availablePositions);
		newPos.x += 0.5f;
		newPos.y -= 0.5f;
		transform.position = newPos;
		print(newPos.ToString());
		StartCoroutine(ShootBulletAndMove(3));
	}

	//SHOOTS BULLET AT time * 0.5, THEN LEAVE AT time
	public IEnumerator ShootBulletAndMove(float time){
		float elapsedTime = 0;
		bool shotAlready = false;

		while (elapsedTime < time){
			elapsedTime += Time.deltaTime;

			if(elapsedTime > time * 0.5f && !shotAlready)
			{
				Vector3 vel = gpm.linkRef.transform.position - transform.position;
				vel.Normalize();
				vel *= 5;
				GameObject go = Instantiate(RockShot, transform.position, Quaternion.identity) as GameObject;
				go.rigidbody2D.velocity = vel;
				go.SendMessage("setMode", 1);
				gpm.enemies.Add(go);
				shotAlready = true;
			}
			if(elapsedTime >= time){
				Callback call = Movement;
				call();
			}
			
			yield return null;
		}
	}
}
