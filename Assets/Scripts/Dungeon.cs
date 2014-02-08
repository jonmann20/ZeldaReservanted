using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	public static Dungeon that;
	
	int roomPos = 8;//6
	public static int NUM_ROOMS = 9;
	GameObject curRoom, nextRoom;

	SpriteDir theDir;
	public bool isAnimating = false;
	int numDone = 0;

	GameObject linkGM;
	
	/*
		|x|x|			0, 1
		|x|z|x|  		2, 3, 4
		|x|y|x|			5, 6, 7
		|x|				8

		y: start
		z: boss
	*/


	void Awake(){
		that = this;

		linkGM = GameObject.Find("Link");
		linkGM.transform.position = new Vector3(0, -5, 0);
	}

	void Start(){
		curRoom = DungeonRooms.that.getRoom(roomPos, new Vector3(0, -2));
	}


	public void changeRoom(SpriteDir dir){
		float newRoomX = 0;
		float newRoomY = 0;

		switch(dir){
			case SpriteDir.UP:
				if(roomPos == 2 || roomPos == 3){
					roomPos -= 2;
				}
				else {
					roomPos -= 3;
				}

				newRoomY = 9f;


				break;
			case SpriteDir.DOWN:
				if(roomPos == 0 || roomPos == 1){
					roomPos += 2;
				}
				else {
					roomPos += 3;
				}

				newRoomY = -13;

				break;
			case SpriteDir.RIGHT:
				++roomPos;

				newRoomY = -2;
				newRoomX = 16;

				break;
			case SpriteDir.LEFT:
				--roomPos;

				newRoomY = -2;
				newRoomX = -16;

				break;
		}

		if(isAnimating){
			return;
		}

		isAnimating = true;


		// disable link
		linkGM.gameObject.SetActive(false);


		// swap rooms
		nextRoom = DungeonRooms.that.getRoom(roomPos, new Vector2(newRoomX, newRoomY));

		theDir = dir;

		if(dir == SpriteDir.DOWN){
			StartCoroutine(Utils.that.MoveToPosition(curRoom.transform, new Vector3(0, 9), 1.8f, done));
		}
		else if(dir == SpriteDir.UP){
			StartCoroutine(Utils.that.MoveToPosition(curRoom.transform, new Vector3(0, -13f), 1.8f, done));
		}
		else {
			StartCoroutine(Utils.that.MoveToPosition(curRoom.transform, new Vector3(-newRoomX, -2), 1.8f, done));
		}

		StartCoroutine(Utils.that.MoveToPosition(nextRoom.transform, new Vector3(0, -2), 1.8f, done));
	}
	
	void done(){
		if(++numDone >= 2){
			//DungeonRooms.that.destroyRoom(curRoom);
			Destroy(curRoom);

			curRoom = nextRoom;
			numDone = 0;
			isAnimating = false;


			// enable link
			float teleportX = 0;
			float teleportY = 0;

			if(theDir == SpriteDir.LEFT){
				teleportX = 5.25f;
			}
			else if(theDir == SpriteDir.RIGHT){
				teleportX = -5.25f;
			}
			else if(theDir == SpriteDir.UP){
				teleportY = -5f;
			}
			else if(theDir == SpriteDir.DOWN){
				teleportY = 1.4f;
			}

			if(theDir == SpriteDir.UP || theDir == SpriteDir.DOWN){
				linkGM.transform.position = new Vector3(0, teleportY, 0);
			}
			else {
				linkGM.transform.position = new Vector3(teleportX, -2, 0);
			}

			linkGM.gameObject.SetActive(true);
		}
	}

}

