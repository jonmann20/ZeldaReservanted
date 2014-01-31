using UnityEngine;
using System.Collections;

/*
	This is the action component of Link.  It defines link's attack and special attack functions.
*/

public partial class Link : MonoBehaviour {

	GameObject woodenSwordPrefab, woodenSword, woodenSwordProjectile;
	float itemOffset = 0.93f;

	void checkAction(){
		if(Input.GetButtonDown("Attack")){
			switch(){
				case Sprite.UP:
				case Sprite.UP_STEP:
					sprRend.sprite = spr[4];
					woodenSword = getItem();
					break;
				case Sprite.DOWN:
				case Sprite.DOWN_STEP:
					sprRend.sprite = spr[6];
					woodenSword = getItem();
					break;
				case Sprite.RIGHT:
				case Sprite.RIGHT_STEP:
					sprRend.sprite = spr[5];
					woodenSword = getItem();
					break;
				case Sprite.LEFT:
				case Sprite.LEFT_STEP:
					sprRend.sprite = spr[7];
					woodenSword = getItem();
					break;
			}
			
			isAttacking = true;
			StartCoroutine("finishAttack", );

			if(health == initHealth){
				shootSword();	
			}
		}
	}

	void shootSword(){
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

		switch(){
			case Sprite.UP:
			case Sprite.UP_STEP:
				offsetY = itemOffset;
				break;
			case Sprite.RIGHT:
			case Sprite.RIGHT_STEP:
				offsetX = itemOffset;
				rot = Quaternion.Euler(0, 0, 270);
				break;
			case Sprite.DOWN:
			case Sprite.DOWN_STEP:
				offsetY = -itemOffset;
				rot = Quaternion.Euler(0, 0, 180);
				break;
			case Sprite.LEFT:
			case Sprite.LEFT_STEP:
				offsetX = -itemOffset;
				rot = Quaternion.Euler(0, 0, 90);
				break;
		}

		Vector3 newPos = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
		return Instantiate(woodenSwordPrefab, newPos, rot) as GameObject;
	}
	
	IEnumerator finishAttack(Sprite d){
		yield return new WaitForSeconds(0.23f);
		
		switch(d){
			case Sprite.UP:
			case Sprite.UP_STEP:
				sprRend.sprite = spr[10];
				break;
			case Sprite.DOWN:
			case Sprite.DOWN_STEP:
				sprRend.sprite = spr[8];
				break;
			case Sprite.RIGHT:
			case Sprite.RIGHT_STEP:
				sprRend.sprite = spr[11];
				 = Sprite.RIGHT;
				break;
			case Sprite.LEFT:
			case Sprite.LEFT_STEP:
				sprRend.sprite = spr[1];
				 = Sprite.LEFT;
				break;
		}

		isAttacking = false;
		Destroy(woodenSword);
	}
}