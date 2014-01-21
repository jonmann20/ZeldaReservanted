using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	public Sprite[] spr;
	public float speed;

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
		speed = 5.0f;
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
			//Debug.Log(diffFromGridLine);
			if(Mathf.Abs(diffFromGridLine) < 0.2f)
			{
				transform.Translate(new Vector2(-diffFromGridLine, 0));

				if(vert == 1f){
					transform.Translate(new Vector2(0, speed * Time.deltaTime));
					direction = 'n';
				}
			
				if(vert == -1f){
					transform.Translate(new Vector2(0, -speed * Time.deltaTime));
					direction = 's';
				}
			}
			else
			{
				if(diffFromGridLine > 0)
				{
					transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
					direction = 'w';
				}
				else if(diffFromGridLine < 0)
				{
					transform.Translate(new Vector2(speed * Time.deltaTime, 0));
					direction = 'e';
				}
			}
		}

		//HORIZONTAL MOVEMENT
		if(vert == 0 && (Input.GetButton("Left") || Input.GetButton("Right"))){
			float diffFromGridLine = getNearestHorizontalGridLine(transform.position.y, direction);
			//Debug.Log(diffFromGridLine);
			if(Mathf.Abs(diffFromGridLine) < 0.2f)
			{
				transform.Translate(new Vector2(0, diffFromGridLine));
				//Debug.Log("diff:" + diffFromGridLine);
				if(hor == 1f){
					transform.Translate(new Vector2(speed * Time.deltaTime, 0));
					direction = 'e';
				}
				
				if(hor == -1f){
					transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
					direction = 'w';
				}
			}
			else
			{
				if(diffFromGridLine > 0)
				{
					transform.Translate(new Vector2(0, speed * Time.deltaTime));
					direction = 'n';
				}
				else if(diffFromGridLine < 0)
				{
					transform.Translate(new Vector2(0, -speed * Time.deltaTime));
					direction = 's';
				}
			}
		}

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
			closestRight --;
		closestRight ++;
		
		while(closestLeft <= xpos) 
			closestLeft ++;
		closestLeft --;
		
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

	void OnCollisionEnter2D(Collision2D col){
		//transform.Translate(-1, 0, 0);
		//transform.position = previousPos;
		Debug.Log("Collision2D");
		if(col.gameObject.tag == "wall"){
			speed = 0;
		}
		else {
			speed = initSpeed;
		}
	}

	public void setMovementEnabled(bool b)
	{
		movementEnabled = b;
	}

	public void setDesiredDisplacementTime(Vector3 v)
	{
		desiredDisplacement = new Vector2(v.x, v.y);
		Debug.Log(v.x);
		desiredDisplacementTime = v.z;
		deltaDisplacement = desiredDisplacement / (desiredDisplacementTime);
	}
}