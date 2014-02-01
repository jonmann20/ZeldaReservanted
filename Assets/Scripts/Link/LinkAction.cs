using UnityEngine;
using System.Collections;

/*
	This is the action component of Link.  It defines link's attack and special attack functions.
*/

public partial class Link : MonoBehaviour {

	GameObject woodenSwordPrefab, woodenSword, woodenSwordProjectile;
	float itemOffset = 0.93f;

	bool canShootAgain = true;

	void checkAction(){
		if(Input.GetButtonDown("Attack")){
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
			
			isAttacking = true;
			StartCoroutine(finishAttack(dir));

			Sword swordScript = woodenSword.GetComponent<Sword>();

			if(health == initHealth){
				if(canShootAgain){
					canShootAgain = false;

					shootSword();
					Destroy(woodenSword);
				}
				else {
					GameAudio.playSwordSwing();
				}
			}
			else {
				GameAudio.playSwordSwing();
			}
		}
	}

	void shootSword(){
		GameAudio.playSwordShoot();


		float speed = 5;

		woodenSwordProjectile = getItem();
		Quaternion r = woodenSwordProjectile.transform.localRotation;

		if(Mathf.Approximately(r.eulerAngles.z, 0)){
			woodenSwordProjectile.rigidbody2D.velocity = new Vector3(0, speed, 0);
		}
		else if(Mathf.Approximately(r.eulerAngles.z, 270)){
			woodenSwordProjectile.rigidbody2D.velocity = new Vector3(speed, 0, 0);
		}
		else if(Mathf.Approximately(r.eulerAngles.z, 180)){
			woodenSwordProjectile.rigidbody2D.velocity = new Vector3(0, -speed, 0);
		}
		else if(Mathf.Approximately(r.eulerAngles.z, 90)){
			woodenSwordProjectile.rigidbody2D.velocity = new Vector3(-speed, 0, 0);
		}
	}

//	IEnumerator blinkShot(){
//		yield return new WaitForSeconds(0.1f);
//	}

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