﻿using UnityEngine;
using System.Collections;

public class RockShotScript : MonoBehaviour {

	public Sprite spr_rock;
	public Sprite spr_energy;

	//0 = rock, 1 = energy
	int mode = 0;
	int stillTimer = 10;
	
	void Update(){
		if(stillTimer > 0)
			stillTimer --;

		if(Mathf.Abs(transform.position.x) > 9.2f || transform.position.y > 5.5f || transform.position.y < -8.5f){
			Destroy(gameObject);
		}
		if(GetComponent<Rigidbody2D>().velocity.x == 0 && GetComponent<Rigidbody2D>().velocity.y == 0 && stillTimer <= 0)
			Destroy(gameObject);
	}
	void OnBecameInvisible(){
		Destroy(gameObject);
	}

	public void setMode(int m)
	{
		mode = m;
		if(m == 0)
			(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_rock;
		else if(m == 1)
			(GetComponent<Renderer>() as SpriteRenderer).sprite = spr_energy;
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player" && Link.invincibility <= 0){
			--Link.health;
			Link.updateHealth();
			Link.invincibility = 90;
			GameAudio.playPlayerHurt();
			Destroy(gameObject);
		}
		if(col.gameObject.tag == "physicaltile" && mode == 0)
			Destroy(gameObject);
	}
}
