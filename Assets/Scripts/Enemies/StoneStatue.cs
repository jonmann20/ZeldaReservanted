using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneStatue : Enemy {
	
	const int timeToMove = 120;
	int timer = timeToMove;
	
	GameObject RockShot;
	
	public delegate void Callback();
	
	// Use this for initialization
	void Start () {
		health = 100;
		RockShot = Resources.Load("Enemies/RockShot") as GameObject;
		Movement();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public override void Movement()
	{
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
				shotAlready = true;

				go.transform.parent = GameObject.Find("EnemyHolder").transform;
			}
			if(elapsedTime >= time){
				Callback call = Movement;
				call();
			}
			
			yield return null;
		}
	}
}
