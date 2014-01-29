using UnityEngine;
using System.Collections;

public class Octorok : Enemy {

	int timeBeforeShot = 0;
	SpriteDir dirToMove = SpriteDir.UP;
	float howFarToMove = 0;
	
	bool doDiceRoll = true;
	float distMoved = 0;
	int timeSinceRoll = 0;

	float speed = 450f;
	bool showStep = false;

	Vector3 prevPosition, dirVec = Vector3.zero;

	void Update(){
		if(doDiceRoll){
			rollDice();
		}
		
		Movement();

		// animation
		sprRend.sprite = spr[(int)dirToMove];
	}

	bool freezeMovement = false;

	bool waitForExit = false;

	void OnCollisionEnter2D(Collision2D col){
		if(waitForExit) return;
		//print ("enter");

		switch(dirToMove){
			case SpriteDir.UP:
			case SpriteDir.UP_STEP:
				//dirVec = new Vector2(0, -speed);
				dirToMove = SpriteDir.DOWN;
				break;
			case SpriteDir.RIGHT:
			case SpriteDir.RIGHT_STEP:
				//dirVec = new Vector2(-speed, 0);
				dirToMove = SpriteDir.LEFT;
				break;
			case SpriteDir.DOWN:
			case SpriteDir.DOWN_STEP:
				//dirVec = new Vector2(0, speed);
				dirToMove = SpriteDir.UP;
				break;
			case SpriteDir.LEFT:
			case SpriteDir.LEFT_STEP:
				//dirVec = new Vector2(speed, 0);
				dirToMove = SpriteDir.RIGHT;
				break;
		}

		waitForExit = true;
	}

	void OnCollisionStay2D(Collision2D col){
		//print ("stay");
	}

	void OnCollisionExit2D(Collision2D col){
		//print ("exit");
		waitForExit = false;
	}

	public override void Movement(){
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce(dirVec * Time.deltaTime);

		float dtDist = Vector3.Distance(prevPosition, transform.position);
		distMoved += dtDist;
		prevPosition = transform.position;
		
		if(distMoved >= howFarToMove){
			doDiceRoll = true;
		}

		//checkForInvisibleWall();
		//handleStep();

		switch(dirToMove){
			case SpriteDir.UP:
			case SpriteDir.UP_STEP:
				dirVec = new Vector2(0, speed);
				dirToMove = showStep ? SpriteDir.UP_STEP : SpriteDir.UP;

				break;
			case SpriteDir.RIGHT:
			case SpriteDir.RIGHT_STEP:
				dirVec = new Vector2(speed, 0);
				dirToMove = showStep ? SpriteDir.RIGHT_STEP : SpriteDir.RIGHT;

				break;
			case SpriteDir.DOWN:
			case SpriteDir.DOWN_STEP:
				dirVec = new Vector2(0, -speed);
				dirToMove = showStep ? SpriteDir.DOWN_STEP : SpriteDir.DOWN;

				break;
			case SpriteDir.LEFT:
			case SpriteDir.LEFT_STEP:
				dirVec = new Vector2(-speed, 0);
				dirToMove = showStep ? SpriteDir.LEFT_STEP : SpriteDir.LEFT;

				break;
		}
	}

	void checkForInvisibleWall(){
		if(transform.position.x >= 7){			// hit right "wall"
			dirToMove = SpriteDir.LEFT;
		}
		else if(transform.position.x <= -7){	// hit left "wall"
			dirToMove = SpriteDir.RIGHT;
		}
		
		if(transform.position.y >= 6){			// hit top "wall"
			dirToMove = SpriteDir.DOWN;
		}
		else if(transform.position.y <= -6){	// hit bottom "wall"
			dirToMove = SpriteDir.UP;
		}
	}

	void handleStep(){
		int distFix = (int)(distMoved*10);
		if(distFix % 4 == 0){
			//showStep = !showStep;
		}
	}

	void rollDice(){
		distMoved = 0;
		
		timeBeforeShot = Random.Range(1, 8);
		int newDir = Random.Range(0, 4);

		switch(newDir){
			case 0:
				dirToMove = SpriteDir.UP;
				break;
			case 1:
				dirToMove = SpriteDir.RIGHT;
				break;
			case 2:
				dirToMove = SpriteDir.DOWN;
				break;
			case 3:
				dirToMove = SpriteDir.LEFT;
				break;
		}

		
		if(dirToMove == SpriteDir.UP || dirToMove == SpriteDir.DOWN){	// vertical
			howFarToMove = Random.Range(1, 16);
		}
		else {
			howFarToMove = Random.Range(1, 11);
		}


		// debug
		//howFarToMove = 10;
		//dirToMove = SpriteDir.UP;

		doDiceRoll = false;
	}
}
