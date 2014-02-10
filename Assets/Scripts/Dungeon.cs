using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour {

	public static Dungeon that;
	
	int roomPos = 6; //6 is start

	public static int NUM_ROOMS = 10;
	GameObject curRoom, nextRoom;

	SpriteDir theDir;
	public bool isAnimating = false;
	int numDone = 0;

	GameObject epona;

	GameObject linkGM;
	
	/*
		|x|x|			0, 1
		|x|z|x|  		2, 3, 4
		|x|y|x|			5, 6, 7
		|x|				8

		y: start
		z: boss
	*/

	//ENEMY PREFABS
	GameObject Stalfos, Keese, Epona, DungeonZola, Spikes, StoneStatue;

	void Awake(){
		that = this;

		//LOAD ENEMY PREFABS
		Stalfos = Resources.Load<GameObject>("Enemies/Stalfos");
		Keese = Resources.Load<GameObject>("Enemies/Keese");
		Epona = Resources.Load<GameObject>("Enemies/Epona");
		DungeonZola = Resources.Load<GameObject>("Enemies/DungeonZola");
		Spikes = Resources.Load<GameObject>("Enemies/Spikes");
		StoneStatue = Resources.Load<GameObject>("Enemies/StoneStatue");

		linkGM = GameObject.Find("Link");
		linkGM.transform.position = new Vector3(0, -5.6f, 0);
	}

	void Start(){
		// load saves
		Utils.that.loadSaves();

		curRoom = DungeonRooms.that.getRoom(roomPos, new Vector3(0, -2));
		Invoke ("showLink", 0.02f);
	}

	void showLink(){
		linkGM.GetComponent<SpriteRenderer>().enabled = true;
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
			case SpriteDir.DOWN_STEP:		// from stairs room8
				roomPos = 1;
				newRoomY = -2;
				break;
			case SpriteDir.UP_STEP:			// from stairs room1
				roomPos = 8;
				newRoomY = -2;
				break;
			case SpriteDir.RIGHT_STEP:		// from stairs room3 (boss --> triforce)
				roomPos = 9;
				newRoomY = -2;
				break;
		}

		if(roomPos != 9){
			string tmp = "hudRoom" + roomPos;
			GameObject.Find("hudlocation").transform.position = GameObject.Find(tmp).transform.position;
		}
		else {
			// TODO: hide HUD??
		}

		if(isAnimating){
			return;
		}
		isAnimating = true;

		// delete items
		Utils.that.deleteItems();

		// delete enemies
		deleteEnemies();


		// disable link
		linkGM.gameObject.SetActive(false);


		// swap rooms
		nextRoom = DungeonRooms.that.getRoom(roomPos, new Vector2(newRoomX, newRoomY));

		theDir = dir;

		if(dir == SpriteDir.DOWN_STEP || dir == SpriteDir.UP_STEP || dir == SpriteDir.RIGHT_STEP){
			numDone = 2;
			done();
		}
		else {
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
	}
	
	void done(){
		if(++numDone >= 2){
			Destroy(curRoom);

			curRoom = nextRoom;

			curRoom.transform.position = new Vector3(0, -2, 0);

			numDone = 0;
			isAnimating = false;


			// enable link
			float teleportX = 0;
			float teleportY = 0;

			if(theDir == SpriteDir.LEFT){
				teleportX = 6.35f;
			}
			else if(theDir == SpriteDir.RIGHT){
				teleportX = -6.35f;
			}
			else if(theDir == SpriteDir.UP){
				teleportY = -5.6f;
			}
			else if(theDir == SpriteDir.DOWN){
				teleportY = 1.7f;
			}

			if(theDir == SpriteDir.UP || theDir == SpriteDir.DOWN){
				linkGM.transform.position = new Vector3(0, teleportY, 0);
			}
			else if(theDir == SpriteDir.DOWN_STEP){
				linkGM.transform.position = new Vector3(-5.25f, 0.5f);	// just below staris

			}
			else if(theDir == SpriteDir.UP_STEP){
				linkGM.transform.position = new Vector3(-5.5f, -3.5f);	// just above staris
			}
			else if(theDir == SpriteDir.RIGHT_STEP){
				linkGM.transform.position = new Vector3(0, -3.5f);	// just above staris
			}
			else {
				linkGM.transform.position = new Vector3(teleportX, -2, 0);
			}

			linkGM.gameObject.SetActive(true);


			spawnEnemies();
		}
	}

	void deleteEnemies(){
		if(GameObject.Find("EnemyHolder") != null ){
			Destroy(GameObject.Find("EnemyHolder"));
		}
	}

	void spawnEnemies(){
		//print("done, with roomPos = " + roomPos.ToString());

		GameObject theRoom = null;
		GameObject enemyHolder = new GameObject("EnemyHolder");
		
		if(roomPos == 0)
		{
			GameObject dz = Instantiate(DungeonZola, new Vector3(5.5f, 1, 0), Quaternion.identity) as GameObject;

			GameObject k1 = Instantiate(Keese, new Vector3(-3.16f, -1.67f, 0), Quaternion.identity) as GameObject;
			GameObject k2 = Instantiate(Keese, new Vector3(3.16f, 1.67f, 0), Quaternion.identity) as GameObject;

			theRoom = GameObject.Find ("Room0");
			k1.transform.parent = enemyHolder.transform;
			k2.transform.parent = enemyHolder.transform;
			dz.transform.parent = enemyHolder.transform;

		}
		else if(roomPos == 2)
		{
			theRoom = GameObject.Find("Room2");

			// left side
			int theY = 1;
			for(int i=0; i < 7; ++i){
				GameObject spike = Instantiate(Spikes, new Vector3(-5.5f, theY), Quaternion.identity) as GameObject;
				spike.transform.parent = enemyHolder.transform;
				
				--theY;
			}

			// right side
			theY = 1;
			for(int i=0; i < 7; ++i){
				GameObject spike = Instantiate(Spikes, new Vector3(5.5f, theY), Quaternion.identity) as GameObject;
				spike.transform.parent = enemyHolder.transform;

				--theY;
			}
		}
		else if(roomPos == 3)
		{
			if(epona == null){
				epona = Instantiate(Epona, new Vector3(100f, 1, 0), Quaternion.identity) as GameObject;

				theRoom = GameObject.Find("Room3");
				epona.transform.parent = enemyHolder.transform;
			}
		}
		else if(roomPos == 5)
		{
			GameObject s1 = Instantiate(Stalfos, new Vector3(-3.16f, -1.67f, 0), Quaternion.identity) as GameObject;
			GameObject s2 = Instantiate(Stalfos, new Vector3(3.17f, 0.44f, 0), Quaternion.identity) as GameObject;

			theRoom = GameObject.Find ("Room5");

			s1.transform.parent = enemyHolder.transform;
			s2.transform.parent = enemyHolder.transform;
		}
		else if(roomPos == 7)
		{
			GameObject k1 = Instantiate(Keese, new Vector3(-3.16f, -1.67f, 0), Quaternion.identity) as GameObject;
			GameObject k2 = Instantiate(Keese, new Vector3(3.17f, -0.44f, 0), Quaternion.identity) as GameObject;
			GameObject k3 = Instantiate(Keese, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

			theRoom = GameObject.Find("Room7");
			k1.transform.parent = enemyHolder.transform;
			k2.transform.parent = enemyHolder.transform;
			k3.transform.parent = enemyHolder.transform;
		}
		else if(roomPos == 8){
			theRoom = GameObject.Find("Room8");

			GameObject s1 = Instantiate(Spikes, new Vector3(-5.5f, 1), Quaternion.identity) as GameObject;
			GameObject s2 = Instantiate(Spikes, new Vector3(5.5f, 1), Quaternion.identity) as GameObject;

			GameObject stat = Instantiate(StoneStatue, new Vector3(5.5f, -5), Quaternion.identity) as GameObject;
				
			s1.transform.parent = enemyHolder.transform;
			s2.transform.parent = enemyHolder.transform;
			stat.transform.parent = enemyHolder.transform;
		}

		if(theRoom != null){
			enemyHolder.transform.parent = theRoom.transform;
		}

	}
}

