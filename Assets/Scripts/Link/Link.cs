using UnityEngine;
using System.Collections;

/*
	This is the "main" class for Link.  It holds the main logic for link.
	
	See LinkMovement for the movement component of Link.
	See LinkAction for the action component of Link.
	See LinkCollision for the collision component of Link.
*/

public enum SpriteDir {UP, UP_STEP, DOWN, DOWN_STEP, LEFT, LEFT_STEP, RIGHT, RIGHT_STEP};	// NOTE: X_STEP must directly proceed X

public partial class Link : MonoBehaviour {

	public Sprite[] spr;
	public float speed = 5f;

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;

	public bool movementEnabled = true;
	public bool isAttacking = false;


	SpriteRenderer sprRend;

	SpriteDir dir = SpriteDir.UP_STEP;
	Vector3 previousPos;

	// SCREEN SCROLL
	Vector2 desiredDisplacement, deltaDisplacement;
	float desiredDisplacementTime, vert, hor;


	void Start(){
		sprRend = renderer as SpriteRenderer;
		sprRend.sprite = spr[8];	// NOTE: this actually gets overwritten by movement logic

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