using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonZolaScript : Enemy {
	
	const int timeToMove = 120;
	int timer = timeToMove;
	List<Vector3> availablePositions;
	
	GameObject RockShot;
	
	public delegate void Callback();

	void Start(){
		health = 3;
		RockShot = Resources.Load("Enemies/RockShot") as GameObject;
		//print(DungeonRooms.that.waterTiles.Count);

		availablePositions = new List<Vector3>();

		foreach(GameObject g in DungeonRooms.that.waterTiles){
			if(g != null){
				availablePositions.Add (g.transform.position);
			}
		}

		Movement();
	}

	void Update(){
		/*timer --;
		if(timer <= 0)
		{
			Movement();
			timer = timeToMove;
		}*/
	}
	
	public override void Movement()
	{
		Vector3 newPos = getRandomElementInList<Vector3>(availablePositions);
		transform.position = newPos;

		//print(newPos.ToString());

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
				Vector3 vel = Link.that.transform.position - transform.position;
				vel.Normalize();
				vel *= 5;
				GameObject go = Instantiate(RockShot, transform.position, Quaternion.identity) as GameObject;
				go.rigidbody2D.velocity = vel;
				go.SendMessage("setMode", 1);


				go.transform.parent = GameObject.Find("EnemyHolder").transform;


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