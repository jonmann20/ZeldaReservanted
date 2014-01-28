﻿using UnityEngine;
using System.Collections;

public class octorokScript : Base_Enemy {

	public Sprite[] spr;
	
	int timeBeforeShot = 0;
	int dirToMove = 0;
	float howFarToMove = 0;

	bool doDiceRoll = true;
	float distMoved = 0;
	int timeSinceRoll = 0;

	void Start(){
		sprRend = gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
		sprRend.sprite = spr[2];
	}

	void Update(){
		//movement();

		if(doDiceRoll){
			rollDice();
		}

		move();
		animate();
	}

	void OnCollisionEnter2D(Collision2D col){
		switch(dirToMove){
			case 0:
				dirToMove = 2;
				break;
			case 1:
				dirToMove = 3;
				break;
			case 2:
				dirToMove = 0;
				break;
			case 3:
				dirToMove = 1;
				break;
		}
	}

	void move(){
		if(transform.position.x >= 7){
			dirToMove = 3;
		}
		else if(transform.position.x <= -7){
			dirToMove = 1;
		}

		if(transform.position.y >= 6){	
			dirToMove = 2;
		}
		else if(transform.position.y <= -6){	
			dirToMove = 0;
		}

		float speed = 1.5f;

		switch(dirToMove){
			case 0:
				transform.Translate(0, speed*Time.deltaTime, 0);
				break;
			case 1:
				transform.Translate(speed*Time.deltaTime, 0, 0);
				break;
			case 2:
				transform.Translate(0, -speed*Time.deltaTime, 0);
				break;
			case 3:
				transform.Translate(-speed*Time.deltaTime, 0, 0);
				break;
		}
		distMoved += speed*Time.deltaTime;

		if(distMoved >= howFarToMove){
			doDiceRoll = true;
		}
	}

	void rollDice(){
		distMoved = 0;

		timeBeforeShot = Random.Range(1, 8);
		dirToMove = Random.Range(0, 4);

		if(dirToMove == 0 || dirToMove == 2){	// vertical
			howFarToMove = Random.Range(1, 16);
		}
		else {
			howFarToMove = Random.Range(1, 11);
		}

		doDiceRoll = false;
	}

	void animate(){
//		if(direction == 'n') sprRend.sprite = spr[2];
//		else if(direction == 's') sprRend.sprite = spr[1];
//		else if(direction == 'e') sprRend.sprite = spr[0];
//		else if(direction == 'w') sprRend.sprite = spr[3];

		sprRend.sprite = spr[dirToMove];
	}
}