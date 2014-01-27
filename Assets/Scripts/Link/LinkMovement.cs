using UnityEngine;
using System.Collections;

<<<<<<< HEAD:Assets/Scripts/Link.cs
public class Link : MonoBehaviour {
	
	public Sprite[] spr;
	float speed = 1200;
	
	public float topLeftX = -8f;
	public float topLeftY = 3.5f;
	
	float initSpeed;
	char direction = 'n';
	Vector3 previousPos;
	
	SpriteRenderer sprRend;
	
	//SCREEN SCROLL
	Vector2 desiredDisplacement;
	Vector2 deltaDisplacement;
	float desiredDisplacementTime;
	
	public bool movementEnabled = true;
	
	void Start () {
		initSpeed = speed;
		sprRend = renderer as SpriteRenderer;
		sprRend.sprite = spr[2];
	}
	
	void Update () {
		rigidbody2D.velocity = Vector2.zero;
		if(movementEnabled)
			movement();
		
		//SCREEN SCROLL
		if(desiredDisplacementTime > 0)
		{

			transform.Translate(deltaDisplacement);
			desiredDisplacementTime --;
		}
	}
	
	void FixedUpdate()
	{
		previousPos = transform.position;
	}
	
	float vert, hor;
	void movement(){
		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");
		
		//VERTICAL MOVEMENT
		if(Input.GetButton("Up") || Input.GetButton("Down")){
			float diffFromGridLine = getNearestVerticalGridLine(transform.position.x, direction);
			
			if(Mathf.Abs(diffFromGridLine) < 0.2f)
			{
				transform.Translate(new Vector2(-diffFromGridLine, 0));
				if(vert == 1f){
					rigidbody2D.AddForce(new Vector2(0, speed * Time.deltaTime));
					direction = 'n';
				}
				
				if(vert == -1f){
					rigidbody2D.AddForce(new Vector2(0, -speed * Time.deltaTime));
					direction = 's';
				}
			}
			else
			{
				if(diffFromGridLine > 0)
				{
					rigidbody2D.AddForce(new Vector2(-speed * Time.deltaTime, 0));
					direction = 'w';
				}
				else if(diffFromGridLine < 0)
				{
					rigidbody2D.AddForce(new Vector2(speed * Time.deltaTime, 0));
					direction = 'e';
=======
/*
	This is the movement component of Link.  It takes input from the player and translates link accordingly.
*/

public partial class Link : MonoBehaviour {

	void checkMovement(){
		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");
		
		// VERTICAL MOVEMENT
		if(Input.GetButton("Up") || Input.GetButton("Down")){
			float diffFromGridLine = getNearestVerticalGridLine(transform.position.x, dir);
			
			if(Mathf.Abs(diffFromGridLine) < 0.2f){
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
			else{
				if(diffFromGridLine > 0){
					transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
					dir = SpriteDir.LEFT;
				}
				else if(diffFromGridLine < 0){
					transform.Translate(new Vector2(speed * Time.deltaTime, 0));
					dir = SpriteDir.RIGHT;
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
				}
			}
		}
		
<<<<<<< HEAD:Assets/Scripts/Link.cs
		//HORIZONTAL MOVEMENT
		if(vert == 0 && (Input.GetButton("Left") || Input.GetButton("Right"))){
			float diffFromGridLine = getNearestHorizontalGridLine(transform.position.y, direction);
=======
		// HORIZONTAL MOVEMENT
		if(vert == 0 && (Input.GetButton("Left") || Input.GetButton("Right"))){
			float diffFromGridLine = getNearestHorizontalGridLine(transform.position.y, dir);
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
			
			if(Mathf.Abs(diffFromGridLine) < 0.2f)
			{
				transform.Translate(new Vector2(0, diffFromGridLine));
				
				if(hor == 1f){
<<<<<<< HEAD:Assets/Scripts/Link.cs
					rigidbody2D.AddForce(new Vector2(speed * Time.deltaTime, 0));
					direction = 'e';
				}
				
				if(hor == -1f){
					rigidbody2D.AddForce(new Vector2(-speed * Time.deltaTime, 0));
					direction = 'w';
=======
					transform.Translate(new Vector2(speed * Time.deltaTime, 0));
					dir = SpriteDir.RIGHT_STEP;
				}
				
				if(hor == -1f){
					transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
					dir = SpriteDir.LEFT;
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
				}
			}
			else
			{
<<<<<<< HEAD:Assets/Scripts/Link.cs
				if(diffFromGridLine > 0)
				{
					rigidbody2D.AddForce(new Vector2(0, speed * Time.deltaTime));
					direction = 'n';
				}
				else if(diffFromGridLine < 0)
				{
					rigidbody2D.AddForce(new Vector2(0, -speed * Time.deltaTime));
					direction = 's';
=======
				if(diffFromGridLine > 0){
					transform.Translate(new Vector2(0, speed * Time.deltaTime));
					dir = SpriteDir.UP;
				}
				else if(diffFromGridLine < 0){
					transform.Translate(new Vector2(0, -speed * Time.deltaTime));
					dir = SpriteDir.DOWN;
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
				}
			}
		}
		
<<<<<<< HEAD:Assets/Scripts/Link.cs
		if(direction == 'n') sprRend.sprite = spr[2];
		else if(direction == 's') sprRend.sprite = spr[0];
		else if(direction == 'e') sprRend.sprite = spr[3];
		else if(direction == 'w') sprRend.sprite = spr[1];
	}
	
	float getNearestHorizontalGridLine(float ypos, char direction)
	{
		float closestUp = topLeftY + 20 + 0.5f;
		float closestDown = topLeftY - 50;
		
		while(closestUp >= ypos)
			closestUp --;
		closestUp ++;
		
		while(closestDown <= ypos) 
			closestDown ++;
		closestDown --;
=======
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
	
	float getNearestHorizontalGridLine(float ypos, SpriteDir direction){
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
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
		
		float closestUpDiff = Mathf.Abs(closestUp - ypos);
		float closestDownDiff = Mathf.Abs(closestDown - ypos);
		
<<<<<<< HEAD:Assets/Scripts/Link.cs
		if(direction == 'n')
		{
=======
		if(direction == SpriteDir.UP){
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
			if(closestUpDiff < closestDownDiff * 1.2f) return closestUpDiff;
			else if(closestDownDiff * 1.2f <= closestUpDiff) return -closestDownDiff;
		}
		
		if(direction == SpriteDir.DOWN){
			if(closestUpDiff * 1.2f < closestDownDiff) return closestUpDiff;
			else if(closestDownDiff <= closestUpDiff * 1.2f) return -closestDownDiff;
		}

		return 0;
	}
	
<<<<<<< HEAD:Assets/Scripts/Link.cs
	float getNearestVerticalGridLine(float xpos, char direction)
	{
=======
	float getNearestVerticalGridLine(float xpos, SpriteDir direction){
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
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
		
<<<<<<< HEAD:Assets/Scripts/Link.cs
		if(direction == 'e')
=======
		if(direction == SpriteDir.RIGHT)
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
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
	
<<<<<<< HEAD:Assets/Scripts/Link.cs
	void OnCollisionEnter2D(Collision2D col){
		//transform.Translate(-1, 0, 0);
		//transform.position = previousPos;
	}
	
	public void setMovementEnabled(bool b)
	{
		movementEnabled = b;
	}
	
	public void setDesiredDisplacementTime(Vector3 v)
	{
=======
	public void setMovementEnabled(bool b){
		movementEnabled = b;
	}
	
	public void setDesiredDisplacementTime(Vector3 v){
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9:Assets/Scripts/Link/LinkMovement.cs
		desiredDisplacement = new Vector2(v.x, v.y);
		desiredDisplacementTime = v.z;
		deltaDisplacement = desiredDisplacement / (desiredDisplacementTime);
	}
}