using UnityEngine;
using System.Collections;

/*
	This is the collision component of Link.  It checks for collision between link, the tiles, and the enemies.
*/

public partial class Link : MonoBehaviour {

	const int KNOCK_BACK_SPEED = 10;

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Enemy" && invincibility <= 0){
			--Link.health;
			Link.updateHealth();
			invincibility = 30;
			GameAudio.playPlayerHurt();

			//doKnockback(col);
		}
	}

	void doKnockback(Collision2D col)
	{
		print("KNOCKED BACK!");
		Vector3 enemyToLink = transform.position - col.gameObject.transform.position;

		float horComponent = 0.0f;
		float vertComponent = 0.0f;

		horComponent = enemyToLink.x;
		vertComponent = enemyToLink.y;

		//horizontal knockback
		if(Mathf.Abs(horComponent) > Mathf.Abs(vertComponent))
		{
			if(horComponent > 0) //to right
				rigidbody2D.velocity = new Vector3(KNOCK_BACK_SPEED, 0, 0);
			else //to left
				rigidbody2D.velocity = new Vector3(-KNOCK_BACK_SPEED, 0, 0);
		}
		else //vertical knockback
		{
			if(vertComponent > 0) //to north
				rigidbody2D.velocity = new Vector3(0, KNOCK_BACK_SPEED, 0);
			else //to south
				rigidbody2D.velocity = new Vector3(0, -KNOCK_BACK_SPEED, 0);
		}
	}
}