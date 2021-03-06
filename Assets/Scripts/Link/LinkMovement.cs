using UnityEngine;
using System.Collections;

/*
	This is the movement component of Link.  It takes input from the player and translates link accordingly.
*/

public partial class Link : MonoBehaviour {

	const float DIST_BTW_STEPS = 0.4f;
	Vector2 distSinceLastStep = Vector2.zero;
	bool isRightFootForward = true;

	void checkMovement(){
		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");

		// VERTICAL MOVEMENT
		if(Input.GetButton("Up") || Input.GetButton("Down")){
			float diffFromGridLine = getNearestVerticalGridLine(transform.position.x, dir);
			
			if(Mathf.Abs(diffFromGridLine) < 0.2f){
				transform.Translate(new Vector2(-diffFromGridLine, 0));
				
				if(vert == 1f){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed * Time.deltaTime));
					dir = handleStep(SpriteDir.UP);
				}
				
				if(vert == -1f){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -speed * Time.deltaTime));
					dir = handleStep(SpriteDir.DOWN);
				}
			}
			else{
				if(diffFromGridLine > 0){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed * Time.deltaTime, 0));
					dir = handleStep(SpriteDir.LEFT);
				}
				else if(diffFromGridLine < 0){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * Time.deltaTime, 0));
					dir = handleStep(SpriteDir.RIGHT);
				}
			}
		}

		//HORIZONTAL MOVEMENT
		if(vert == 0 && (Input.GetButton("Left") || Input.GetButton("Right"))){
			float diffFromGridLine = getNearestHorizontalGridLine(transform.position.y, dir);
			
			if(Mathf.Abs(diffFromGridLine) < 0.2f){
				transform.Translate(new Vector2(0, diffFromGridLine));

				if(hor == 1f){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * Time.deltaTime, 0));
					dir = handleStep(SpriteDir.RIGHT);
				}
				
				if(hor == -1f){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed * Time.deltaTime, 0));
					dir = handleStep(SpriteDir.LEFT);
				}
			}
			else{
				if(diffFromGridLine > 0){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed * Time.deltaTime));
					dir = handleStep(SpriteDir.UP);
				}
				else if(diffFromGridLine < 0){
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -speed * Time.deltaTime));
					dir = handleStep(SpriteDir.DOWN);
				}
			}
		}

		if(!isAttacking){
			switch(dir){
			case SpriteDir.UP:
				sprRend.sprite = spr[0];
				break;
			case SpriteDir.UP_STEP:
				sprRend.sprite = spr[8];
				break;
			case SpriteDir.DOWN:
				sprRend.sprite = spr[2];
				break;
			case SpriteDir.DOWN_STEP:
				sprRend.sprite = spr[10];
				break;
			case SpriteDir.RIGHT:
				sprRend.sprite = spr[1];
				break;
			case SpriteDir.RIGHT_STEP:
				sprRend.sprite = spr[9];
				break;
			case SpriteDir.LEFT:
				sprRend.sprite = spr[3];
				break;
			case SpriteDir.LEFT_STEP:
				sprRend.sprite = spr[11];
				break;
			}
		}
	}

	SpriteDir handleStep(SpriteDir theDir){
		Vector3 dtPos = previousPos - transform.position;
		distSinceLastStep += new Vector2(Mathf.Abs(dtPos.x), Mathf.Abs(dtPos.y));

		if(distSinceLastStep.x >= DIST_BTW_STEPS || distSinceLastStep.y >= DIST_BTW_STEPS){
			isRightFootForward = !isRightFootForward;
			distSinceLastStep = Vector2.zero;
		}

		if(isRightFootForward){
			return theDir+1;
		}
		else {
			return theDir;
		}
	}
	
	float getNearestHorizontalGridLine(float ypos, SpriteDir currentDir){
		float closestUp = topLeftY + 20 + 0.5f;
		float closestDown = topLeftY - 50;
		
		while(closestUp >= ypos){
			closestUp --;
		}
		closestUp ++;
		
		while(closestDown <= ypos){ 
			closestDown ++;
		}
		closestDown --;
		
		float closestUpDiff = Mathf.Abs(closestUp - ypos);
		float closestDownDiff = Mathf.Abs(closestDown - ypos);

		if(closestUpDiff < closestDownDiff) return closestUpDiff;
		else if(closestDownDiff <= closestUpDiff) return -closestDownDiff;

		return 0;
	}
	


	float getNearestVerticalGridLine(float xpos, SpriteDir currentDir){
		float closestRight = topLeftX + 0.5f + 50;
		float closestLeft = topLeftX - 2;
		
		while(closestRight >= xpos){
			closestRight --;
		}
		closestRight ++;
		
		while(closestLeft <= xpos) {
			closestLeft ++;
		}
		closestLeft --;
		
		float closestLeftDiff = Mathf.Abs(closestLeft - xpos);
		float closestRightDiff = Mathf.Abs(closestRight - xpos);

		if(closestLeftDiff < closestRightDiff) return closestLeftDiff;
		else if(closestRightDiff <= closestLeftDiff) return -closestRightDiff;

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