using UnityEngine;
using System.Collections;

/*
	This is the action component of Link.  It defines link's attack and special attack functions.
*/

public partial class Link : MonoBehaviour {

	GameObject woodenSwordPrefab, woodenSword, woodenSwordProjectile, bombPrefab;
	float itemOffset = 0.93f;

	bool canShootAgain = true;
	const float SHOT_SPEED = 10.5f;

	void checkAction(){
		if(Input.GetButtonDown("Attack")){
			if(!Inventory.hasWoodenSword) return;

			switch(dir){
				case SpriteDir.UP:
				case SpriteDir.UP_STEP:
						sprRend.sprite = spr[4];
						woodenSword = getItem();
						break;
				case SpriteDir.DOWN:
				case SpriteDir.DOWN_STEP:
						sprRend.sprite = spr[6];
						woodenSword = getItem();
						break;
				case SpriteDir.RIGHT:
				case SpriteDir.RIGHT_STEP:
						sprRend.sprite = spr[5];
						woodenSword = getItem();
						break;
				case SpriteDir.LEFT:
				case SpriteDir.LEFT_STEP:
						sprRend.sprite = spr[7];
						woodenSword = getItem();
						break;
			}

			woodenSword.transform.parent = GameObject.Find("ItemHolder").transform;

			isAttacking = true;
			if(Link.health == Link.initHealth && woodenSwordProjectile == null){
				Invoke("shootSword", 0.18f);
			}

			StartCoroutine(finishAttack(dir));

			GameAudio.playSwordSwing();
		}

		if(Input.GetButtonDown("SpecialAttack")){
			if(Inventory.hasBomb){
				if(numBomb > 0){
					GameObject theBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity) as GameObject;
					theBomb.transform.parent = GameObject.Find("ItemHolder").transform;
					Bomb bScript = theBomb.GetComponent<Bomb>();
					bScript.isPickup = false;

					bScript.setBomb();
				}
			}
		}
	}

	void shootSword(){
		GameAudio.playSwordShoot();
		float distFromLink = 0.5f;
		woodenSwordProjectile = getItem();
		woodenSwordProjectile.transform.parent = GameObject.Find("ItemHolder").transform;
		Quaternion r = woodenSwordProjectile.transform.localRotation;

		if(Mathf.Approximately(r.eulerAngles.z, 0)){
			woodenSwordProjectile.rigidbody2D.velocity = new Vector3(0, SHOT_SPEED, 0);
		}
		else if(Mathf.Approximately(r.eulerAngles.z, 270)){
			woodenSwordProjectile.rigidbody2D.velocity = new Vector3(SHOT_SPEED, 0, 0);
		}
		else if(Mathf.Approximately(r.eulerAngles.z, 180)){
			woodenSwordProjectile.rigidbody2D.velocity = new Vector3(0, -SHOT_SPEED, 0);
		}
		else if(Mathf.Approximately(r.eulerAngles.z, 90)){
			woodenSwordProjectile.rigidbody2D.velocity = new Vector3(-SHOT_SPEED, 0, 0);
		}

		woodenSwordProjectile.transform.Translate(0, distFromLink, 0);
	}

	GameObject getItem(){
		float offsetX = 0, offsetY = 0;
		Quaternion rot = Quaternion.identity;

		switch(dir){
			case SpriteDir.UP:
			case SpriteDir.UP_STEP:
					offsetY = itemOffset;
					break;
			case SpriteDir.RIGHT:
			case SpriteDir.RIGHT_STEP:
					offsetX = itemOffset;
					rot = Quaternion.Euler(0, 0, 270);
					break;
			case SpriteDir.DOWN:
			case SpriteDir.DOWN_STEP:
					offsetY = -itemOffset;
					rot = Quaternion.Euler(0, 0, 180);
					break;
			case SpriteDir.LEFT:
			case SpriteDir.LEFT_STEP:
					offsetX = -itemOffset;
					rot = Quaternion.Euler(0, 0, 90);
					break;
		}

		Vector3 newPos = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);

		return Instantiate(woodenSwordPrefab, newPos, rot) as GameObject;
	}
	
	IEnumerator finishAttack(SpriteDir d){
		yield return new WaitForSeconds(0.23f);

		// restore link's animation state
		switch(d){
			case SpriteDir.UP:
			case SpriteDir.UP_STEP:
					sprRend.sprite = spr[10];
					break;
			case SpriteDir.DOWN:
			case SpriteDir.DOWN_STEP:
					sprRend.sprite = spr[8];
					break;
			case SpriteDir.RIGHT:
			case SpriteDir.RIGHT_STEP:
					sprRend.sprite = spr[11];
					break;
			case SpriteDir.LEFT:
			case SpriteDir.LEFT_STEP:
					sprRend.sprite = spr[1];
					break;
		}

		isAttacking = false;
		Destroy(woodenSword);

	}
}