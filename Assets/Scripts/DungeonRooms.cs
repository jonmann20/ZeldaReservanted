using UnityEngine;
using System.Collections;

public class DungeonRoom {
	public GameObject room;			// the basic generic room templates
	public GameObject[] objs;		// collidable tiles, locked door, ...
	public GameObject[] enemies;	// all enemies in the room
}


public class DungeonRooms : MonoBehaviour {
	public static DungeonRooms that;
	GameObject roomT, roomTL, roomTB, roomTRB, roomRB, roomRBL, roomB, roomRL, roomL, roomBoss, blackFloor;
	GameObject block, mat;

	GameObject roomHolder;

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
		//roomBoss =;

		blackFloor = Resources.Load<GameObject>("Dungeon/blackFloor");
		block = Resources.Load<GameObject>("Dungeon/block");
		mat = Resources.Load<GameObject>("Dungeon/mat");
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
		}

		return null;
	}

	GameObject getRoomZero(Vector3 pos){
		GameObject drHolder = new GameObject("Room0");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomRB, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;
		
		return drHolder;
	}

	GameObject getRoomOne(Vector3 pos){
		GameObject drHolder = new GameObject("Room1");
		drHolder.transform.position = pos;
		drHolder.transform.parent = roomHolder.transform;
		
		DungeonRoom dr = new DungeonRoom();
		dr.room = Instantiate(roomL, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;
		
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
		dr.room = Instantiate(roomT, pos, Quaternion.identity) as GameObject;
		dr.room.transform.parent = drHolder.transform;
		
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

		dr.objs = new GameObject[2];
		
		dr.objs[0] = Instantiate(block, new Vector3(pos.x - 1.5f, pos.y, 0), Quaternion.identity) as GameObject;
		dr.objs[1] = Instantiate(block, new Vector3(pos.x + 1.5f, pos.y, 0), Quaternion.identity) as GameObject;

		dr.objs[1].AddComponent<MagicBlock>();

		for(int i=0; i < 2; ++i){
			dr.objs[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
			dr.objs[i].transform.parent = objHolder.transform;
		}

		return drHolder;
	}

}