using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	public static Dungeon that;
	GameObject tiles;

	GameObject curRoom, nextRoom;
	GameObject roomT, roomTL, roomTB, roomTRB, roomRB, roomB, roomRL, roomL, roomBoss;

	bool isAnimating = false;
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

	GameObject[] rooms;
	int roomPos = 6;
	SpriteDir theDir;

	void Awake(){
		that = this;

		linkGM = GameObject.Find("Link");

		//roomT = ;
		roomTL = Resources.Load<GameObject>("Dungeon/RoomTL");
		//roomTB = ;
		roomTRB = Resources.Load<GameObject>("Dungeon/RoomTRB");
		//roomRB = ;
		roomB = Resources.Load<GameObject>("Dungeon/RoomB");
		roomRL = Resources.Load<GameObject>("Dungeon/RoomLR");
		//roomL = ;
		//roomBoss =;


		rooms = new GameObject[8];

		//rooms[0] = roomRB;
		//rooms[1] = roomL;
		//rooms[2] = roomTB;
		//rooms[3] = roomBoss;
		//rooms[4] = roomB;
		rooms[5] = roomTRB;
		rooms[6] = roomRL;
		rooms[7] = roomTL;
		//rooms[8] = roomT;

		curRoom = Instantiate(rooms[roomPos], new Vector3(0, -2), Quaternion.identity) as GameObject;
		curRoom.transform.parent = GameObject.Find("RoomHolder").transform;
	}

	void Start(){
		tiles = Resources.Load<GameObject>("dungeontiles");
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

				break;
			case SpriteDir.DOWN:
				if(roomPos == 0 || roomPos == 1){
					roomPos += 2;
				}
				else {
					roomPos += 3;
				}

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
		nextRoom = Instantiate(rooms[roomPos], new Vector3(newRoomX, newRoomY), Quaternion.identity) as GameObject;
		nextRoom.transform.parent = GameObject.Find("RoomHolder").transform;

		theDir = dir;
		StartCoroutine(Utils.that.MoveToPosition(curRoom.transform, new Vector3(-newRoomX, -2), 1.8f, done));	// TODO: or -newRoomY
		StartCoroutine(Utils.that.MoveToPosition(nextRoom.transform, new Vector3(0, -2), 1.8f, done));
	}
	
	void done(){
		if(++numDone >= 2){
			Destroy(curRoom);

			curRoom = nextRoom;
			numDone = 0;
			isAnimating = false;


			// enable link
			float teleportX = 0;

			if(theDir == SpriteDir.LEFT){
				teleportX = 5.45f;
			}
			else if(theDir == SpriteDir.RIGHT){
				teleportX = -5.45f;
			}

			linkGM.transform.position = new Vector3(teleportX, -2, 0);
			linkGM.gameObject.SetActive(true);
		}
	}

}

