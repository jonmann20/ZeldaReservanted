using UnityEngine;
using System.Collections;

/*
	This is the movement component of Link.  It takes input from the player and translates link accordingly.
*/

public partial class Link : MonoBehaviour {

	void checkMovement(){
		rigidbody2D.velocity = Vector2.zero;
		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");
		
		// VERTICAL MOVEMENT
		if(Input.GetButton("Up") || Input.GetButton("Down")){
			float diffFromGridLine = getNearestVerticalGridLine(transform.position.x, dir);
			
			if(Mathf.Abs(diffFromGridLine) < 0.2f){
				transform.Translate(new Vector2(-diffFromGridLine, 0));
				
				if(vert == 1f){
					rigidbody2D.AddForce(new Vector2(0, speed * Time.deltaTime));
					dir = SpriteDir.UP;
				}
				
				if(vert == -1f){
					rigidbody2D.AddForce(new Vector2(0, -speed * Time.deltaTime));
					dir = SpriteDir.DOWN;
				}
			}
			else{
				if(diffFromGridLine > 0){
					rigidbody2D.AddForce(new Vector2(-speed * Time.deltaTime, 0));
					dir = SpriteDir.LEFT;
				}
				else if(diffFromGridLine < 0){
					rigidbody2D.AddForce(new Vector2(speed * Time.deltaTime, 0));
					dir = SpriteDir.RIGHT;
				}
			}
		}

		//HORIZONTAL MOVEMENT
		if(vert == 0 && (Input.GetButton("Left") || Input.GetButton("Right"))){
			float diffFromGridLine = getNearestHorizontalGridLine(transform.position.y, dir);
			
			if(Mathf.Abs(diffFromGridLine) < 0.2f)
			{
				transform.Translate(new Vector2(0, diffFromGridLine));
				
				if(hor == 1f){
					rigidbody2D.AddForce(new Vector2(speed * Time.deltaTime, 0));
					dir = SpriteDir.RIGHT;
				}
				
				if(hor == -1f){
					rigidbody2D.AddForce(new Vector2(-speed * Time.deltaTime, 0));
					dir = SpriteDir.LEFT;
				}
			}
			else
			{
				if(diffFromGridLine > 0)
				{
					rigidbody2D.AddForce(new Vector2(0, speed * Time.deltaTime));
					dir = SpriteDir.UP;
				}
				else if(diffFromGridLine < 0)
				{
					rigidbody2D.AddForce(new Vector2(0, -speed * Time.deltaTime));
					dir = SpriteDir.DOWN;
				}
			}
		}

		if(!isAttacking){
			switch(dir){
			case SpriteDir.UP:
				sprRend.sprite = spr[0];
				break;
			case SpriteDir.DOWN:
				sprRend.sprite = spr[2];
				break;
			case SpriteDir.RIGHT:
				sprRend.sprite = spr[1];
				break;
			case SpriteDir.RIGHT_STEP:
				sprRend.sprite = spr[1];
				break;
			case SpriteDir.LEFT:
				sprRend.sprite = spr[3];
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
		
		float closestUpDiff = Mathf.Abs(closestUp - ypos);
		float closestDownDiff = Mathf.Abs(closestDown - ypos);
		

		if(direction == SpriteDir.UP){
			if(closestUpDiff < closestDownDiff * 1.2f) return closestUpDiff;
			else if(closestDownDiff * 1.2f <= closestUpDiff) return -closestDownDiff;
		}
		
		if(direction == SpriteDir.DOWN){
			if(closestUpDiff * 1.2f < closestDownDiff) return closestUpDiff;
			else if(closestDownDiff <= closestUpDiff * 1.2f) return -closestDownDiff;
		}

		return 0;
	}
	


	float getNearestVerticalGridLine(float xpos, SpriteDir direction){
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

	public void setMovementEnabled(bool b)
	{
		movementEnabled = b;
	}
	
	public void setDesiredDisplacementTime(Vector3 v){
		desiredDisplacement = new Vector2(v.x, v.y);
		desiredDisplacementTime = v.z;
		deltaDisplacement = desiredDisplacement / (desiredDisplacementTime);
	}
}