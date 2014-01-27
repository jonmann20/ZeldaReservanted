using UnityEngine;
using System.Collections;

/*
	This is the action component of Link.  It defines link's attack and special attack functions.
*/

public partial class Link : MonoBehaviour {

	void checkAction(){
		if(Input.GetButtonDown("Attack")){
			switch(dir){
			case SpriteDir.UP:
			case SpriteDir.UP_STEP:
				sprRend.sprite = spr[6];
				transform.Translate(0, 0.345f, 0);
				break;
			case SpriteDir.DOWN:
			case SpriteDir.DOWN_STEP:
				sprRend.sprite = spr[4];
				transform.Translate(0, -0.345f, 0);
				break;
			case SpriteDir.RIGHT:
			case SpriteDir.RIGHT_STEP:
				sprRend.sprite = spr[7];
				transform.Translate(0.345f, 0, 0);
				break;
			case SpriteDir.LEFT:
			case SpriteDir.LEFT_STEP:
				sprRend.sprite = spr[5];
				transform.Translate(-0.345f, 0, 0);
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
			transform.Translate(0, -0.345f, 0);
			break;
		case SpriteDir.DOWN:
		case SpriteDir.DOWN_STEP:
			sprRend.sprite = spr[8];
			transform.Translate(0, 0.345f, 0);
			break;
		case SpriteDir.RIGHT:
		case SpriteDir.RIGHT_STEP:
			transform.Translate(-0.345f, 0, 0);
			sprRend.sprite = spr[11];
			dir = SpriteDir.RIGHT;
			break;
		case SpriteDir.LEFT:
		case SpriteDir.LEFT_STEP:
			transform.Translate(0.345f, 0, 0);
			sprRend.sprite = spr[1];
			dir = SpriteDir.LEFT;
			break;
		}
		
		isAttacking = false;
	}
}