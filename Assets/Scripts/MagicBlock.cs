using UnityEngine;
using System.Collections;

public class MagicBlock : MonoBehaviour {
	bool isMoving = false, isMoved = false;

	int resistance = 0;

	Vector3 newPos;

	void OnCollisionEnter2D(Collision2D col){
		resistance = 0;
	}

	void OnCollisionStay2D(Collision2D col){
		bool isRightSide = checkSide(col);

		if(!isMoved && isRightSide && ++resistance > 15 && col.gameObject.tag == "Player"){
			isMoved = true;
			isMoving = true;

			newPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
			//TODO: GameAudio.play(magic);

			DungeonRooms.that.addStairs();
		}
	}

	void OnCollisionExit2D(Collision2D col){
		resistance = 0;
	}

	bool checkSide(Collision2D col){
		return col.transform.position.x >= (transform.position.x + 0.9f);
	}

	void Update(){
		if(isMoving){
			transform.Translate(-1*Time.deltaTime, 0, 0);

			if(transform.position.x <= newPos.x){
				isMoving = false;
			}
		}
	}


}
