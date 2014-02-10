using UnityEngine;
using System.Collections;

public class DungeonRoom {
	public GameObject room;			// the basic generic room templates
	public GameObject[] objs;		// collidable tiles, locked door, ...
	public GameObject[] enemies;	// all enemies in the room
}


public class DungeonRooms : MonoBehaviour {
	public static DungeonRooms that;

	GameObject roomT, roomTL, roomTB, roomTRB, roomRB, roomRBL, roomB, roomRL, roomL, roomBombN, blackFloor, roomG;
	GameObject block, mat, stairs, triforce, water, key;
	GameObject lockedDoor, bombDoorS, bombDoorN, bombDoorTrigger;

	GameObject roomHolder;

	bool stairsFound = false;
	public bool keyIsFound = false;
	public bool doorIsOpen = false;
	bool bombDoorIsOpen = false;

	void Awake(){
		that = this;

		roomHolder = GameObject.Find ("RoomHolder");

		roomT = Resources.Load<GameObject>("Dungeon/RoomT");
		roomTL = Resources.Load<GameObject>("Dungeon/RoomTL");
		roomTB = Resources.Load<GameObject>("Dungeon/RoomTB");
		roomTRB = Resources.Load<GameObject>("Dungeon/RoomTRB");
		roomRB = Resources.Load<GameObject>("Dungeon/RoomRB");
		roomRBL = Resources.Load<GameObject>("Dungeon/RoomRBL");
		roomB = Resources.Load<GameObject>("Dungeon/RoomB");
		roomRL = Resources.Load<GameObject>("Dungeon/RoomRL");
		roomL = Resources.Load<GameObject>("Dungeon/RoomL");
		roomG = Resources.Load<GameObject>("Dungeon/GenericRoom");
		roomBombN = Resources.Load<GameObject>("Dungeon/RoomBombN");

		blackFloor = Resources.Load<GameObject>("Dungeon/blackFloor");
		block = Resources.Load<GameObject>("Dungeon/block");
		mat = Resources.Load<GameObject>("Dungeon/mat");
		stairs = Resources.Load<GameObject>("Dungeon/stairs");
		triforce = Resources.Load<GameObject>("Dungeon/triforce");
		water = Resources.Load<GameObject>("Dungeon/water");
		key = Resources.Load<GameObject>("Dungeon/key");

		lockedDoor = Resources.Load<GameObject>("Dungeon/lockedDoor");
		bombDoorS = Resources.Load<GameObject>("Dungeon/bombDoorS");
		bombDoorTrigger = Resources.Load<GameObject>("Dungeon/bombDoorTrigger");
	}

	public GameObject getRoom(int num, Vector3 pos){
		switch(num){
			case 0: return getRoomZero(pos);
			case 1: return getRoomOne(pos);
			case 2: return getRoomTwo(pos);
			case 3: return getRoomThree(pos);
			case 4: return getRoomFour(pos);
			case 5: return getRoomFive(pos);
			case 6:	return getRoomSix(pos);
			case 7: return getRoomSeven(pos);
			case 8: return getRoomEight(pos);
			case 9: return getRoomTriforce(pos);
		}

		return null;
	}

	GameObject getRoomTriforce(Vector3 pos){
		GameObject drHolder = new GameObject("RoomTriforce");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomG, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;

		GameObject objHolder = new GameObject("Objs");
		objHolder.transform.position = pos;
		objHolder.transform.parent = drHolder.transform;
		
		const int NUM_OBJS = 1;
		dr.objs = new GameObject[NUM_OBJS];
		
		dr.objs[0] = Instantiate(triforce, new Vector3(0, -1, 0), Quaternion.identity) as GameObject;

		// TODO: blocks around triforce

		for(int i=0; i < NUM_OBJS; ++i){
			dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			dr.objs[i].transform.parent = objHolder.transform;
		}

		return drHolder;
	}

	GameObject getRoomZero(Vector3 pos){
		GameObject drHolder = new GameObject("Room0");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomB, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;

		GameObject objHolder = new GameObject("Objs");
		objHolder.transform.position = pos;
		objHolder.transform.parent = drHolder.transform;
		
		const int NUM_OBJS = 39;
		dr.objs = new GameObject[NUM_OBJS];

		// top left corner
		dr.objs[0] = Instantiate(water, new Vector3(pos.x - 5.5f, pos.y + 3, 0), Quaternion.identity) as GameObject;
		dr.objs[1] = Instantiate(water, new Vector3(pos.x - 4.5f, pos.y + 3, 0), Quaternion.identity) as GameObject;
		dr.objs[2] = Instantiate(water, new Vector3(pos.x - 3.5f, pos.y + 3, 0), Quaternion.identity) as GameObject;
		dr.objs[3] = Instantiate(water, new Vector3(pos.x - 5.5f, pos.y + 2, 0), Quaternion.identity) as GameObject;

		// top right corner
		dr.objs[4] = Instantiate(water, new Vector3(pos.x + 5.5f, pos.y + 3, 0), Quaternion.identity) as GameObject;
		dr.objs[5] = Instantiate(water, new Vector3(pos.x + 4.5f, pos.y + 3, 0), Quaternion.identity) as GameObject;
		dr.objs[6] = Instantiate(water, new Vector3(pos.x + 1.5f, pos.y + 1, 0), Quaternion.identity) as GameObject;
		dr.objs[7] = Instantiate(water, new Vector3(pos.x + 5.5f, pos.y + 2, 0), Quaternion.identity) as GameObject;

		// bot right corner
		dr.objs[8] = Instantiate(water, new Vector3(pos.x + 5.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[9] = Instantiate(water, new Vector3(pos.x + 4.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[10] = Instantiate(water, new Vector3(pos.x + 3.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[11] = Instantiate(water, new Vector3(pos.x + 5.5f, pos.y - 2, 0), Quaternion.identity) as GameObject;

		// bot left corner
		dr.objs[12] = Instantiate(water, new Vector3(pos.x - 5.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[13] = Instantiate(water, new Vector3(pos.x - 4.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[14] = Instantiate(water, new Vector3(pos.x - 3.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[15] = Instantiate(water, new Vector3(pos.x - 5.5f, pos.y - 2, 0), Quaternion.identity) as GameObject;

		dr.objs[16] = Instantiate(water, new Vector3(pos.x - 2.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[17] = Instantiate(water, new Vector3(pos.x - 1.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;

		dr.objs[18] = Instantiate(water, new Vector3(pos.x + 2.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[19] = Instantiate(water, new Vector3(pos.x + 1.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[20] = Instantiate(water, new Vector3(pos.x - 5.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;
		dr.objs[21] = Instantiate(water, new Vector3(pos.x + 5.5f, pos.y + 1, 0), Quaternion.identity) as GameObject;

		dr.objs[22] = Instantiate(water, new Vector3(pos.x + 1.5f, pos.y + 2, 0), Quaternion.identity) as GameObject;
		dr.objs[23] = Instantiate(water, new Vector3(pos.x + 0.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;
		dr.objs[24] = Instantiate(water, new Vector3(pos.x - 0.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;

		dr.objs[25] = Instantiate(water, new Vector3(pos.x - 1.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;
		dr.objs[26] = Instantiate(water, new Vector3(pos.x - 1.5f, pos.y + 0, 0), Quaternion.identity) as GameObject;
		dr.objs[27] = Instantiate(water, new Vector3(pos.x - 1.5f, pos.y + 1, 0), Quaternion.identity) as GameObject;

		dr.objs[28] = Instantiate(water, new Vector3(pos.x + 1.5f, pos.y - 2, 0), Quaternion.identity) as GameObject;
		dr.objs[29] = Instantiate(water, new Vector3(pos.x + 1.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;
		dr.objs[30] = Instantiate(water, new Vector3(pos.x + 2.5f, pos.y - 2, 0), Quaternion.identity) as GameObject;
		dr.objs[31] = Instantiate(water, new Vector3(pos.x + 2.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;

		dr.objs[32] = Instantiate(water, new Vector3(pos.x - 4.5f, pos.y - 2, 0), Quaternion.identity) as GameObject;
		dr.objs[33] = Instantiate(water, new Vector3(pos.x - 4.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;

		dr.objs[34] = Instantiate(water, new Vector3(pos.x - 0.5f, pos.y + 1, 0), Quaternion.identity) as GameObject;
		dr.objs[35] = Instantiate(water, new Vector3(pos.x + 0.5f, pos.y + 1, 0), Quaternion.identity) as GameObject;

		dr.objs[36] = Instantiate(water, new Vector3(pos.x - 0.5f, pos.y + 2, 0), Quaternion.identity) as GameObject;
		dr.objs[37] = Instantiate(water, new Vector3(pos.x + 0.5f, pos.y + 2, 0), Quaternion.identity) as GameObject;

		if(!keyIsFound){
			dr.objs[38] = Instantiate(key, new Vector3(pos.x + -0.5f, pos.y, 0), Quaternion.identity) as GameObject;
		}

		for(int i=0; i < NUM_OBJS; ++i){
			if(i == 38 && keyIsFound){
				continue;
			}

			dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			dr.objs[i].transform.parent = objHolder.transform;
		}

		return drHolder;
	}

	GameObject getRoomOne(Vector3 pos){
		GameObject drHolder = new GameObject("Room1");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomG, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;

		GameObject objHolder = new GameObject("Objs");
		objHolder.transform.position = pos;
		objHolder.transform.parent = drHolder.transform;

		dr.objs = new GameObject[2];

		dr.objs[0] = Instantiate(stairs, new Vector3(pos.x - 5.5f, pos.y + 3, 0), Quaternion.identity) as GameObject;
		dr.objs[0].transform.parent = objHolder.transform;

		if(bombDoorIsOpen){
			dr.objs[1] = Instantiate(bombDoorS, new Vector3(pos.x, pos.y - 4f, 0), Quaternion.identity) as GameObject;
			GameObject grc = GameObject.Find("GenericRoom(Clone)");
			grc.transform.Find("Walls/DoorWalls/wallB").collider2D.enabled = false;

		}
		else {
			dr.objs[1] = Instantiate(bombDoorTrigger, new Vector3(pos.x, pos.y - 4), Quaternion.identity) as GameObject;

		}

		dr.objs[1].transform.parent = objHolder.transform;

		return drHolder;
	}

	GameObject getRoomTwo(Vector3 pos){
		GameObject drHolder = new GameObject("Room2");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomTB, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;
		
		return drHolder;
	}

	GameObject getRoomThree(Vector3 pos){
		// boss room

		GameObject drHolder = new GameObject("Room3");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomBombN, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;


		GameObject objHolder = new GameObject("Objs");
		objHolder.transform.position = pos;
		objHolder.transform.parent = drHolder.transform;
		
		dr.objs = new GameObject[1];
		dr.objs[0] = Instantiate(stairs, new Vector3(pos.x + 0.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[0].GetComponent<DungeonStairs>().isRoomBoss = true;

		for(int i=0; i < 1; ++i){
			dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			dr.objs[i].transform.parent = objHolder.transform;
		}

		return drHolder;
	}

	GameObject getRoomFour(Vector3 pos){
		GameObject drHolder = new GameObject("Room4");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomB, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;

		GameObject objHolder = new GameObject("Objs");
		objHolder.transform.position = pos;
		objHolder.transform.parent = drHolder.transform;
		
		dr.objs = new GameObject[1];
		dr.objs[0] = Instantiate(blackFloor, new Vector3(pos.x, pos.y, 0), Quaternion.identity) as GameObject;

		
		for(int i=0; i < 1; ++i){
			dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			dr.objs[i].transform.parent = objHolder.transform;
		}

		return drHolder;
	}

	GameObject getRoomFive(Vector3 pos){
		GameObject drHolder = new GameObject("Room5");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;

		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomTRB, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;

		if(!doorIsOpen){
			GameObject objHolder = new GameObject("Objs");
			objHolder.transform.position = pos;
			objHolder.transform.parent = drHolder.transform;
			
			dr.objs = new GameObject[1];

			dr.objs[0] = Instantiate(lockedDoor, new Vector3(pos.x, pos.y - 4.19f, 0), Quaternion.identity) as GameObject;

			for(int i=0; i < 1; ++i){
				dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
				dr.objs[i].transform.parent = objHolder.transform;
			}
		}

		return drHolder;
	}

	GameObject getRoomSix(Vector3 pos){
		GameObject drHolder = new GameObject("Room6");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;

		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomRBL, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;


		GameObject objHolder = new GameObject("Objs");
		objHolder.transform.position = pos;
		objHolder.transform.parent = drHolder.transform;

		dr.objs = new GameObject[10];

		// smiley face
		dr.objs[0] = Instantiate(block, new Vector3(pos.x - 1.5f, pos.y + 2, 0), Quaternion.identity) as GameObject;
		dr.objs[1] = Instantiate(block, new Vector3(pos.x + 1.5f, pos.y + 2, 0), Quaternion.identity) as GameObject;
		dr.objs[2] = Instantiate(block, new Vector3(pos.x - 2.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[3] = Instantiate(block, new Vector3(pos.x - 1.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[4] = Instantiate(block, new Vector3(pos.x - 0.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;
		dr.objs[5] = Instantiate(block, new Vector3(pos.x + 0.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;
		dr.objs[6] = Instantiate(block, new Vector3(pos.x + 1.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[7] = Instantiate(block, new Vector3(pos.x + 2.5f, pos.y, 0), Quaternion.identity) as GameObject;

		// floor mat
		dr.objs[8] = Instantiate(mat, new Vector3(pos.x - 0.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
		dr.objs[9] = Instantiate(mat, new Vector3(pos.x + 0.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;

		for(int i=0; i < 10; ++i){
			dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			dr.objs[i].transform.parent = objHolder.transform;
		}

		return drHolder;
	}

	GameObject getRoomSeven(Vector3 pos){
		GameObject drHolder = new GameObject("Room7");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomTL, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;

		GameObject objHolder = new GameObject("Objs");
		objHolder.transform.position = pos;
		objHolder.transform.parent = drHolder.transform;
		
		dr.objs = new GameObject[6];

		dr.objs[0] = Instantiate(block, new Vector3(pos.x - 0.5f, pos.y + 1, 0), Quaternion.identity) as GameObject;
		dr.objs[1] = Instantiate(block, new Vector3(pos.x - 0.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[2] = Instantiate(block, new Vector3(pos.x - 0.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;
		dr.objs[3] = Instantiate(block, new Vector3(pos.x + 0.5f, pos.y + 1, 0), Quaternion.identity) as GameObject;
		dr.objs[4] = Instantiate(block, new Vector3(pos.x + 0.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[5] = Instantiate(block, new Vector3(pos.x + 0.5f, pos.y - 1, 0), Quaternion.identity) as GameObject;

		for(int i=0; i < 6; ++i){
			dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			dr.objs[i].transform.parent = objHolder.transform;
		}

		return drHolder;
	}

	GameObject getRoomEight(Vector3 pos){
		GameObject drHolder = new GameObject("Room8");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomT, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;

		GameObject objHolder = new GameObject("Objs");
		objHolder.transform.position = pos;
		objHolder.transform.parent = drHolder.transform;

		const int NUM_OBJ = 3;
		dr.objs = new GameObject[NUM_OBJ];
		
		dr.objs[0] = Instantiate(block, new Vector3(pos.x - 1.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[1] = Instantiate(block, new Vector3(pos.x + 1.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[1].AddComponent<MagicBlock>();

		if(stairsFound){
			dr.objs[2] = Instantiate(stairs, new Vector3(pos.x - 5.5f, pos.y - 3, 0), Quaternion.identity) as GameObject;
			dr.objs[2].GetComponent<DungeonStairs>().isRoom8 = true;
		}

		for(int i=0; i < NUM_OBJ; ++i){
			if(i == 2 && !stairsFound){
				continue;
			}

			dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			dr.objs[i].transform.parent = objHolder.transform;
		}

		return drHolder;
	}

	public void addStairs(){
		if(stairsFound){
			return;
		}
		stairsFound = true;

		GameAudio.playMagical();

		GameObject g = GameObject.Find ("Objs");
		GameObject s = Instantiate(stairs, new Vector3(-5.5f, -5, 0), Quaternion.identity) as GameObject;
		s.transform.parent = g.transform;
		s.GetComponent<DungeonStairs>().isRoom8 = true;
	}

	public void addBombDoor(){
		if(bombDoorIsOpen){
			return;
		}
		bombDoorIsOpen = true;

		GameAudio.playMagical();

		GameObject g = GameObject.Find ("Objs");
		GameObject s = Instantiate(bombDoorS, new Vector3(0, -6, 0), Quaternion.identity) as GameObject;
		s.transform.parent = g.transform;

		GameObject grc = GameObject.Find("GenericRoom(Clone)");
		grc.transform.Find("Walls/DoorWalls/wallB").collider2D.enabled = false;
		Destroy(GameObject.Find ("BombDoorTrigger(Clone)"));
	}

}