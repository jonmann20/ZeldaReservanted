using UnityEngine;
using System.Collections;

public enum SpriteDir {UP, DOWN, LEFT, RIGHT, LEFT_STEP, RIGHT_STEP, UP_STEP, DOWN_STEP};

public class Link : MonoBehaviour {

	public Sprite[] spr;
	public float speed = 5f;

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;

	public bool movementEnabled = true;
	public bool isAttacking = false;


	SpriteRenderer sprRend;

	SpriteDir dir = SpriteDir.UP;
	Vector3 previousPos;

	// SCREEN SCROLL
	Vector2 desiredDisplacement, deltaDisplacement;
	float desiredDisplacementTime, vert, hor;


	void Start(){
		sprRend = renderer as SpriteRenderer;
		sprRend.sprite = spr[2];
	}

	void Update(){
		if(movementEnabled){
			checkMovement();
		}

		if(!isAttacking){
			checkAction();
		}


		// SCREEN SCROLL
		if(desiredDisplacementTime > 0){
			transform.Translate(deltaDisplacement);
			--desiredDisplacementTime;
		}
	}

	void FixedUpdate(){
		previousPos = transform.position;
	}

//	void OnCollisionEnter2D(Collision2D col){
//		//transform.Translate(-1, 0, 0);
//		//transform.position = previousPos;
//
//		Debug.Log("Collision2D");
//
//		if(col.gameObject.tag == "wall"){
//			speed = 0;
//		}
//	}


	#region Movement
	void checkMovement(){
		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");

		// VERTICAL MOVEMENT
		if(Input.GetButton("Up") || Input.GetButton("Down")){
			float diffFromGridLine = getNearestVerticalGridLine(transform.position.x, dir);

			if(Mathf.Abs(diffFromGridLine) < 0.2f)
			{
				transform.Translate(new Vector2(-diffFromGridLine, 0));

				if(vert == 1f){
					transform.Translate(new Vector2(0, speed * Time.deltaTime));
					dir = SpriteDir.UP;
				}
			
				if(vert == -1f){
					transform.Translate(new Vector2(0, -speed * Time.deltaTime));
					dir = SpriteDir.DOWN;
				}
			}
			else
			{
				if(diffFromGridLine > 0){
					transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
					dir = SpriteDir.LEFT;
				}
				else if(diffFromGridLine < 0){
					transform.Translate(new Vector2(speed * Time.deltaTime, 0));
					dir = SpriteDir.RIGHT;
				}
			}
		}

		// HORIZONTAL MOVEMENT
		if(vert == 0 && (Input.GetButton("Left") || Input.GetButton("Right"))){
			float diffFromGridLine = getNearestHorizontalGridLine(transform.position.y, dir);

			if(Mathf.Abs(diffFromGridLine) < 0.2f)
			{
				transform.Translate(new Vector2(0, diffFromGridLine));

				if(hor == 1f){
					transform.Translate(new Vector2(speed * Time.deltaTime, 0));
					dir = SpriteDir.RIGHT_STEP;
				}
				
				if(hor == -1f){
					transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
					dir = SpriteDir.LEFT;
				}
			}
			else
			{
				if(diffFromGridLine > 0){
					transform.Translate(new Vector2(0, speed * Time.deltaTime));
					dir = SpriteDir.UP;
				}
				else if(diffFromGridLine < 0){
					transform.Translate(new Vector2(0, -speed * Time.deltaTime));
					dir = SpriteDir.DOWN;
				}
			}
		}

		if(!isAttacking){
			switch(dir){
				case SpriteDir.UP:
					sprRend.sprite = spr[2];
					break;
				case SpriteDir.DOWN:
					sprRend.sprite = spr[0];
					break;
				case SpriteDir.RIGHT:
					sprRend.sprite = spr[11];
					break;
				case SpriteDir.RIGHT_STEP:
					sprRend.sprite = spr[3];
					break;
				case SpriteDir.LEFT:
					sprRend.sprite = spr[1];
					break;
			}
		}
	}

	float getNearestHorizontalGridLine(float ypos, SpriteDir direction)
	{
		float closestUp = topLeftY + 20 + 0.5f;
		float closestDown = topLeftY - 50;

		while(closestUp >= ypos){
			--closestUp;
		}
		++closestUp;

		while(closestDown <= ypos){ 
			++closestDown;
		}
		--closestDown;

		float closestUpDiff = Mathf.Abs(closestUp - ypos);
		float closestDownDiff = Mathf.Abs(closestDown - ypos);

		if(direction == SpriteDir.UP)
		{
			if(closestUpDiff < closestDownDiff * 1.2f) return closestUpDiff;
			else if(closestDownDiff * 1.2f <= closestUpDiff) return -closestDownDiff;
		}

		if(direction == SpriteDir.DOWN)
		{
			if(closestUpDiff * 1.2f < closestDownDiff) return closestUpDiff;
			else if(closestDownDiff <= closestUpDiff * 1.2f) return -closestDownDiff;
		}
		return 0;
	}

	float getNearestVerticalGridLine(float xpos, SpriteDir direction)
	{
		float closestRight = topLeftX + 0.5f + 50;
		float closestLeft = topLeftX - 2;
		
		while(closestRight >= xpos){
			--closestRight;
		}
		++closestRight;
		
		while(closestLeft <= xpos) {
			++closestLeft;
		}
		--closestLeft;
		
		float closestLeftDiff = Mathf.Abs(closestLeft - xpos);
		float closestRightDiff = Mathf.Abs(closestRight - xpos);

		if(direction == SpriteDir.RIGHT)
		{
			if(closestLeftDiff * 1.2f < closestRightDiff) return closestLeftDiff;
			else if(closestRightDiff <= closestLeftDiff * 1.2f) return -closestRightDiff;
		}

		if(direction == SpriteDir.LEFT)
		{
			if(closestLeftDiff < closestRightDiff * 1.2f) return closestLeftDiff;
			else if(closestRightDiff  * 1.2f <= closestLeftDiff) return -closestRightDiff;
		}
		return 0;
	}

	public void setMovementEnabled(bool b){
		movementEnabled = b;
	}

	public void setDesiredDisplacementTime(Vector3 v){
		desiredDisplacement = new Vector2(v.x, v.y);
		desiredDisplacementTime = v.z;
		deltaDisplacement = desiredDisplacement / (desiredDisplacementTime);
	}
	#endregion Movement

	#region Actions
	void checkAction(){
		if(Input.GetButtonDown("Attack")){
			switch(dir){
				case SpriteDir.UP:
				case SpriteDir.UP_STEP:
					sprRend.sprite = spr[6];
					transform.Translate(0, 0.345f, 0);
					break;
				case SpriteDir.DOWN:
				case SpriteDir.DOWN_STEP:
					sprRend.sprite = spr[4];
					transform.Translate(0, -0.345f, 0);
					break;
				case SpriteDir.RIGHT:
				case SpriteDir.RIGHT_STEP:
					sprRend.sprite = spr[7];
					transform.Translate(0.345f, 0, 0);
					break;
				case SpriteDir.LEFT:
				case SpriteDir.LEFT_STEP:
					sprRend.sprite = spr[5];
					transform.Translate(-0.345f, 0, 0);
					break;
			}

			isAttacking = true;
			StartCoroutine("finishAttack", dir);
		}
	}

	IEnumerator finishAttack(SpriteDir d){
		yield return new WaitForSeconds(0.19f);

		switch(d){
			case SpriteDir.UP:
			case SpriteDir.UP_STEP:
				sprRend.sprite = spr[10];
				transform.Translate(0, -0.345f, 0);
				break;
			case SpriteDir.DOWN:
			case SpriteDir.DOWN_STEP:
				sprRend.sprite = spr[8];
				transform.Translate(0, 0.345f, 0);
				break;
			case SpriteDir.RIGHT:
			case SpriteDir.RIGHT_STEP:
				transform.Translate(-0.345f, 0, 0);
				sprRend.sprite = spr[11];
				dir = SpriteDir.RIGHT;
				break;
			case SpriteDir.LEFT:
			case SpriteDir.LEFT_STEP:
				transform.Translate(0.345f, 0, 0);
				sprRend.sprite = spr[1];
				dir = SpriteDir.LEFT;
				break;
		}

		isAttacking = false;
	}

	#endregion Actions
}