using UnityEngine;
using System.Collections;

/*
	This is the "main" class for Link.  It holds the main logic for link.
	
	See LinkMovement for the movement component of Link.
	See LinkAction for the action component of Link.
	See LinkCollision for the collision component of Link.
*/

public enum SpriteDir {UP, UP_STEP, RIGHT, RIGHT_STEP, DOWN, DOWN_STEP, LEFT, LEFT_STEP};	// NOTE: don't modify order

public partial class Link : MonoBehaviour {

	public Sprite[] spr;
	public float speed = 5f;

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;

	public bool movementEnabled = false;
	public bool isAttacking = false;
	
	SpriteRenderer sprRend;
	Vector3 previousPos;

	// SCREEN SCROLL
	Vector2 desiredDisplacement, deltaDisplacement;
	float desiredDisplacementTime, vert, hor;

	public float initHealth = 3, health = 3;

	SpriteDir dir = SpriteDir.UP_STEP;
	public static int numRupee = 0;

	void Start(){
		previousPos = transform.position;
		//initHealth = health = PlayerPrefs.GetInt("numHearts");

		sprRend = renderer as SpriteRenderer;
		sprRend.sprite = spr[8];	// NOTE: this actually gets overwritten by movement logic

		woodenSwordPrefab = Resources.Load<GameObject>("WoodenSword");
	}

	void Update(){
		rigidbody2D.velocity = Vector2.zero;
		if(!isAttacking){
			checkAction();
		}


		// SCREEN SCROLL
		if(desiredDisplacementTime > 0){
			transform.Translate(deltaDisplacement);
			--desiredDisplacementTime;
		}


		// on sword shot destroyed
		if(woodenSwordProjectile == null){
			canShootAgain = true;
		}

	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = Vector2.zero;
		if(movementEnabled && !isAttacking){
			checkMovement();
		}
		previousPos = transform.position;
	}
}