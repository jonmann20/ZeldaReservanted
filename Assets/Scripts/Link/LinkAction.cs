using UnityEngine;
using System.Collections;

/*
	This is the action component of Link.  It defines link's attack and special attack functions.
*/

public partial class Link : MonoBehaviour {

	GameObject woodenSwordPrefab, woodenSword;
	float itemOffset = 0.93f;

	void checkAction(){
		if(Input.GetButtonDown("Attack")){
			switch(dir){
				case SpriteDir.UP:
				case SpriteDir.UP_STEP:
					sprRend.sprite = spr[4];
					setItem();
					break;
				case SpriteDir.DOWN:
				case SpriteDir.DOWN_STEP:
					sprRend.sprite = spr[6];
					setItem();
					break;
				case SpriteDir.RIGHT:
				case SpriteDir.RIGHT_STEP:
					sprRend.sprite = spr[5];
					setItem();
					break;
				case SpriteDir.LEFT:
				case SpriteDir.LEFT_STEP:
					sprRend.sprite = spr[7];
					setItem();
					break;
			}
			
			isAttacking = true;
			StartCoroutine("finishAttack", dir);
		}
	}

	void setItem(){
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
				rot = Quaternion.Euler(0, 0, -90);
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
		woodenSword = Instantiate(woodenSwordPrefab, newPos, rot) as GameObject;
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
				dir = SpriteDir.RIGHT;
				break;
			case SpriteDir.LEFT:
			case SpriteDir.LEFT_STEP:
				sprRend.sprite = spr[1];
				dir = SpriteDir.LEFT;
				break;
		}

		Destroy(woodenSword);
		isAttacking = false;
	}
}