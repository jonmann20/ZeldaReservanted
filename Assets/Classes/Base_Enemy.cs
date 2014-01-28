using UnityEngine;
using System.Collections;

public class Base_Enemy : MonoBehaviour {

	public float speed = 450;
	public float topLeftX = -8f;
	public float topLeftY = 3.5f;
	public SpriteRenderer sprRend;
	protected char direction = 'n';

	void Start () {
		sprRend = renderer as SpriteRenderer;
	}

	void killEnemy()
	{
		Destroy(this.gameObject);
	}

	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		//killEnemy ();
	}

	protected void movement(){
		float vert = Input.GetAxis("Vertical");
		float hor = Input.GetAxis("Horizontal");
		
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
				}
			}
		}
		
		//HORIZONTAL MOVEMENT
		if(vert == 0 && (Input.GetButton("Left") || Input.GetButton("Right"))){
			float diffFromGridLine = getNearestHorizontalGridLine(transform.position.y, direction);
			
			if(Mathf.Abs(diffFromGridLine) < 0.2f)
			{
				transform.Translate(new Vector2(0, diffFromGridLine));
				
				if(hor == 1f){
					rigidbody2D.AddForce(new Vector2(speed * Time.deltaTime, 0));
					direction = 'e';
				}
				
				if(hor == -1f){
					rigidbody2D.AddForce(new Vector2(-speed * Time.deltaTime, 0));
					direction = 'w';
				}
			}
			else
			{
				if(diffFromGridLine > 0)
				{
					rigidbody2D.AddForce(new Vector2(0, speed * Time.deltaTime));
					direction = 'n';
				}
				else if(diffFromGridLine < 0)
				{
					rigidbody2D.AddForce(new Vector2(0, -speed * Time.deltaTime));
					direction = 's';
				}
			}
		}
	}
	
	float getNearestHorizontalGridLine(float ypos, char direction)
	{
		float closestUp = topLeftY + 20 + 0.5f;
		float closestDown = topLeftY - 50;
		
		while(closestUp >= ypos)
			closestUp -= 2;
		closestUp += 2;
		
		while(closestDown <= ypos) 
			closestDown += 2;
		closestDown -= 2;
		
		float closestUpDiff = Mathf.Abs(closestUp - ypos);
		float closestDownDiff = Mathf.Abs(closestDown - ypos);
		
		if(direction == 'n')
		{
			if(closestUpDiff < closestDownDiff * 1.2f) return closestUpDiff;
			else if(closestDownDiff * 1.2f <= closestUpDiff) return -closestDownDiff;
		}
		if(direction == 's')
		{
			if(closestUpDiff * 1.2f < closestDownDiff) return closestUpDiff;
			else if(closestDownDiff <= closestUpDiff * 1.2f) return -closestDownDiff;
		}
		return 0;
	}
	
	float getNearestVerticalGridLine(float xpos, char direction)
	{
		float closestRight = topLeftX + 0.5f + 50;
		float closestLeft = topLeftX - 2;
		
		while(closestRight >= xpos)
			closestRight -= 2;
		closestRight += 2;
		
		while(closestLeft <= xpos) 
			closestLeft += 2;
		closestLeft -= 2;
		
		float closestLeftDiff = Mathf.Abs(closestLeft - xpos);
		float closestRightDiff = Mathf.Abs(closestRight - xpos);
		
		if(direction == 'e')
		{
			if(closestLeftDiff * 1.2f < closestRightDiff) return closestLeftDiff;
			else if(closestRightDiff <= closestLeftDiff * 1.2f) return -closestRightDiff;
		}
		if(direction == 'w')
		{
			if(closestLeftDiff < closestRightDiff * 1.2f) return closestLeftDiff;
			else if(closestRightDiff  * 1.2f <= closestLeftDiff) return -closestRightDiff;
		}
		return 0;
	}
}
