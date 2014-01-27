using UnityEngine;
using System.Collections;

/*
	This is the action component of Link.  It defines link's attack and special attack functions.
*/

public partial class Link : MonoBehaviour {

	GameObject woodenSwordPrefab, woodenSword;

	void checkAction(){
		if(Input.GetButtonDown("Attack")){
			switch(dir){
				case SpriteDir.UP:
				case SpriteDir.UP_STEP:
					sprRend.sprite = spr[4];
					break;
				case SpriteDir.DOWN:
				case SpriteDir.DOWN_STEP:
					sprRend.sprite = spr[6];
					break;
				case SpriteDir.RIGHT:
				case SpriteDir.RIGHT_STEP:
					sprRend.sprite = spr[5];

					woodenSword = Instantiate(woodenSwordPrefab, transform.position, Quaternion.identity) as GameObject;
//					SpriteRenderer sp =  woodenSword.renderer as SpriteRenderer;
//					ItemSprite item = woodenSword.GetComponent<ItemSprite>();
//					sp.sprite = item.spr[1];

					break;
				case SpriteDir.LEFT:
				case SpriteDir.LEFT_STEP:
					sprRend.sprite = spr[7];
					break;
			}
			
			isAttacking = true;
			StartCoroutine("finishAttack", dir);
		}
	}
	
	IEnumerator finishAttack(SpriteDir d){
		yield return new WaitForSeconds(0.19f);
		
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
		
		isAttacking = false;
	}
}