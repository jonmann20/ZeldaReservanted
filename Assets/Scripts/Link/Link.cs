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

	static GameObject heartPrefab, heartEmptyPrefab;

	public static float initHealth = 3, health = 3;

	SpriteDir dir = SpriteDir.UP_STEP;
	public static int numRupee = 0;

	void Awake(){
		heartPrefab = Resources.Load<GameObject>("Heart");
		heartEmptyPrefab = Resources.Load<GameObject>("HeartEmpty");
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
			Application.LoadLevel("death");
		}


		float f = 0;

		for(int i=0; i < health; ++i){
			GameObject h = Instantiate(heartPrefab, new Vector3(3.5f + f, 4.4f, 0), Quaternion.identity) as GameObject;
			//h.transform.parent = GameObject.Find("heartHolder").transform;
			f += 0.5f;
		}



		for (float hi = health; hi < initHealth; ++hi){
			GameObject h = Instantiate(heartEmptyPrefab, new Vector3(3.5f + f, 4.4f, 0), Quaternion.identity) as GameObject;
			f += 0.5f;
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