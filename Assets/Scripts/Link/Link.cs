using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	static GameObject heartPrefab, heartEmptyPrefab;

	public static float initHealth = 3, health = 3;

	SpriteDir dir = SpriteDir.UP_STEP;
	public static int numRupee = 0;
	public static int numBomb = 0;

	int itemPoseTimer = 0;

	public static Link that;

	static GameObject hrtHolder;

	void Awake(){
		hrtHolder = GameObject.Find("HeartHolder");
		that = this;
		heartPrefab = Resources.Load<GameObject>("Heart");
		heartEmptyPrefab = Resources.Load<GameObject>("HeartEmpty");
		bombPrefab = Resources.Load<GameObject>("Bomb");

		updateHealth();
	}

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



	public static void updateHealth(){
		if(health <= 0){
			// Game Over
			int numD = PlayerPrefs.GetInt("numDeath");
			PlayerPrefs.SetInt("numDeath", ++numD);
			Application.LoadLevel("death");
		}

		// clear previous hearts
		foreach(Transform child in Link.hrtHolder.transform){
			Destroy(child.gameObject);
		}

		float f = 0;

		for(int i=0; i < health; ++i){
			GameObject h = Instantiate(heartPrefab, new Vector3(3.5f + f, 4.4f, 0), Quaternion.identity) as GameObject;
			h.transform.parent = Link.hrtHolder.transform;
			f += 0.5f;
		}

		for (float hi = health; hi < initHealth; ++hi){
			GameObject h = Instantiate(heartEmptyPrefab, new Vector3(3.5f + f, 4.4f, 0), Quaternion.identity) as GameObject;
			h.transform.parent = Link.hrtHolder.transform;
			f += 0.5f;
		}
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = Vector2.zero;
		if(movementEnabled && !isAttacking && itemPoseTimer <= 0){
			checkMovement();
		}
		if(itemPoseTimer > 0)
		{
			itemPoseTimer --;
			sprRend.sprite = spr[12];
		}
		previousPos = transform.position;
	}

	void executeItemPose()
	{
		itemPoseTimer = 120;
	}
}