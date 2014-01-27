using UnityEngine;
using System.Collections;

/*
	This is the "main" class for Link.  It holds the main logic for link.
	
	See LinkMovement for the movement component of Link.
	See LinkAction for the action component of Link.
	See LinkCollision for the collision component of Link.
*/

public enum SpriteDir {UP, DOWN, LEFT, RIGHT, LEFT_STEP, RIGHT_STEP, UP_STEP, DOWN_STEP};

public partial class Link : MonoBehaviour {

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

		woodenSwordPrefab = Resources.Load<GameObject>("WoodenSword");
	}

	void Update(){
		rigidbody2D.velocity = Vector2.zero;
		if(movementEnabled && !isAttacking){
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
}