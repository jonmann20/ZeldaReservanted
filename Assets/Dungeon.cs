using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	public static Dungeon that;
	GameObject tiles;

	GameObject curRoom, nextRoom;
	GameObject roomB, roomLR;

	bool isAnimating = false;


	GameObject linkGM;

	void Awake(){
		that = this;

		linkGM = GameObject.Find("Link");

		roomB = Resources.Load<GameObject>("Dungeon/RoomB");
		roomLR = Resources.Load<GameObject>("Dungeon/RoomLR");

		curRoom = Instantiate(roomLR, new Vector3(0, -2), Quaternion.identity) as GameObject;
		curRoom.transform.parent = GameObject.Find("RoomHolder").transform;
	}

	void Start(){
		tiles = Resources.Load<GameObject>("dungeontiles");
	}
	

	public void changeRoom(SpriteDir dir){

//		switch(dir){
//
//		}
		if(isAnimating){
			return;
		}

		isAnimating = true;


		// disable link
		linkGM.gameObject.SetActive(false);


		// swap rooms
		float newX = -16;
		float newY = -2;

		nextRoom = Instantiate(roomB, new Vector3(newX, newY), Quaternion.identity) as GameObject;
		nextRoom.transform.parent = GameObject.Find("RoomHolder").transform;

		StartCoroutine(Utils.that.MoveToPosition(curRoom.transform, new Vector3(16, -2), 1.8f, done));
		StartCoroutine(Utils.that.MoveToPosition(nextRoom.transform, new Vector3(0, -2), 1.8f, done));
	}

	int numDone = 0;
	void done(){
		if(++numDone >= 2){
			print ("done moving dungeon rooms");
			curRoom = nextRoom;
			numDone = 0;
			isAnimating = false;


			// enable link
			linkGM.transform.position = new Vector3(5.4f, -2, 0);
			linkGM.gameObject.SetActive(true);

		}
	}

}

