using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayMain : MonoBehaviour {

	//overworld map is 256x88 tiles
	public GameObject MapTile;
	public GameObject Enemy;
	Tile[] storedTiles = new Tile[22528];
	Room[,] storedRooms = new Room[16,8];

	GameObject[] activeTiles = new GameObject[176];
	GameObject[] oldTiles = new GameObject[176];
	//TODO: NOT CURRENTLY DESTROYING ALL ENEMIES

	List<GameObject> enemies = new List<GameObject>();

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;

	public GameObject linkRef;
	public GameObject hudPosMarker;

	int currentRoomX = 7;
	int currentRoomY = 7;

	//DEBUG
	GameObject cam;
	debugMapScript dms;
	MapTileScript highlightedTile;

	//SCREEN SCROLL
	bool screenScrolling = false;
	float desiredDisplacementTime = 0;
	
	void Start () {
		//CAM AND DEBUG GUI
		cam = GameObject.Find("MainCamera");
		dms = cam.GetComponent("debugMapScript") as debugMapScript;

		linkRef = GameObject.FindGameObjectsWithTag("Player")[0];
		hudPosMarker = GameObject.FindGameObjectsWithTag("hudposmarker")[0];

		//LOAD PREFABS
		MapTile = Resources.Load("MapTile") as GameObject;
		Enemy = Resources.Load("octorok(red)") as GameObject;

		//overworld dataset via http://inventwithpython.com/blog/2012/12/10/8-bit-nes-legend-of-zelda-map-data/

		//PARSE TILE MAP
		string content = ((TextAsset)Resources.Load("nes_zelda_overworld_tile_map")).text;
		string temp = "";
		int tileNumber = 0;
		for(int i = 0; i < content.Length; i++)
		{
			char currentChar = content[i];
			if(currentChar != ' ' && currentChar != '\n')
			{
				temp += currentChar;
			}
			else
			{
				Tile tempTile = new Tile(tileNumber, tileNumber, temp, "-1");
				tempTile.index = tileNumber;
				storedTiles[tileNumber] = tempTile;
				tileNumber ++;
				temp = "";
			}
		}

		//PARSE ENEMY TILE MAP
		string enemyContent = ((TextAsset)Resources.Load("EnemyTileMap")).text;
		temp = "";
		tileNumber = 0;
		for(int i = 0; i < enemyContent.Length; i++)
		{
			char currentChar = enemyContent[i];
			if(currentChar != ' ')
			{
				temp += currentChar;
			}
			else
			{
				storedTiles[tileNumber].spawnval = temp;
				tileNumber ++;
				temp = "";
			}
		}

		initRooms();
		initEnemyMap();
		populateRoom(currentRoomX, currentRoomY, 0, 0);
	}

	void initEnemyMap()
	{

	}

	void populateRoom(int roomX, int roomY, float offsetX, float offsetY)
	{
		int objectCount = 0;
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				GameObject go = Instantiate(MapTile, new Vector3(topLeftX + i + offsetX, topLeftY - j + offsetY, 0), Quaternion.identity) as GameObject;
				MapTileScript mts = go.GetComponent("MapTileScript") as MapTileScript;

				go.SendMessage("setHexVal", storedRooms[roomX, roomY].tiles[i, j].hexval);
				go.SendMessage("setSpawnVal", storedRooms[roomX, roomY].tiles[i, j].spawnval);
				go.SendMessage("setIndex", storedRooms[roomX, roomY].tiles[i, j].index);
				go.SendMessage("updateSprite");

				activeTiles[objectCount] = go;
				objectCount ++; 
			}
		}
	}

	void initEnemiesCurrentRoom()
	{
		print(enemies.Count);
		foreach(GameObject t in activeTiles)
		{
			MapTileScript mts = t.GetComponent("MapTileScript") as MapTileScript;
			if(mts.spawnVal != "00")
			{
				float xVal = t.transform.position.x + 0.5f;
				float yVal = t.transform.position.y - 0.5f;
				GameObject enemy = Instantiate(Enemy, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
				enemies.Add(enemy);
			}
		}
		print("# enemies:");
		print(enemies.Count);
	}

	//Iterate through the 16x8 possible rooms, initializing them.
	void initRooms()
	{
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 8; j++)
			{
				fillRoom (i, j);
			}
		}
	}

	//fill room at coordinates (<roomX>, <roomY>) with tiles from dataset
	void fillRoom(int roomX, int roomY)
	{
		Tile[,] roomTiles = new Tile[16,11];
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				int tileSpot = i + j * 256 + roomY * 2816 + roomX * 16;
				roomTiles[i,j] = storedTiles[tileSpot];
			}
		}

		Room tempRoom = new Room(roomX, roomY, roomTiles);
		storedRooms[roomX, roomY] = tempRoom;
	}

	void destroyCurrentRoom()
	{
		foreach(GameObject t in activeTiles)
		{
			Destroy(t);
		}
	}

	void Update () {
		//RAYCAST STUFF
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if(hit.collider != null && Input.GetMouseButtonDown(0))
		{
			MapTileScript s = hit.collider.gameObject.GetComponent("MapTileScript") as MapTileScript;
			dms.setText(s.spawnVal, s.hexVal);
			highlightedTile = s;
		}


		if(desiredDisplacementTime > 0)
			desiredDisplacementTime --;
		else
		{
			if(screenScrolling)
			{
				disposeOfArray(oldTiles);
				initEnemiesCurrentRoom();
			}
			screenScrolling = false;
			//NOT EFFICIENT

			linkRef.SendMessage("setMovementEnabled", true);
		}

		if(!screenScrolling)
		{
			//n
			if(linkRef.transform.position.y > 3)
			{
				float x = linkRef.transform.position.x;
				transitionRoom('n');
			}
			//e
			if(linkRef.transform.position.x > 7.5)
			{
				float y = linkRef.transform.position.y;
				transitionRoom('e');
			}
			//s
			if(linkRef.transform.position.y < -7)
			{
				float x = linkRef.transform.position.x;
				transitionRoom('s');
			}
			//w
			if(linkRef.transform.position.x < -7.5)
			{
				float y = linkRef.transform.position.y;
				transitionRoom('w');
			}
		}
	}

	//'n'=north, 'e'=east, 's'=south, 'w'=west
	void transitionRoom(char d)
	{
		screenScrolling = true;
		desiredDisplacementTime = 120;

		//give the new tiles a head-start to remove room-break line.
		float headStartFactor = 1.00f;
		float hudmovedelta = 0.2f;
		int xMovement = 0;
		int yMovement = 0;

		if(d == 'n')
		{
			currentRoomY --;
			hudPosMarker.transform.Translate(0, hudmovedelta, 0);
			xMovement = 0;
			yMovement = -11;
		}
		else if(d == 'e')
		{
			hudPosMarker.transform.Translate(hudmovedelta, 0, 0);
			currentRoomX ++;
			xMovement = -16;
			yMovement = 0;
		}
		else if(d == 's')
		{
			currentRoomY ++;
			hudPosMarker.transform.Translate(0, -hudmovedelta, 0);
			xMovement = 0;
			yMovement = 11;
		}
		else if(d == 'w')
		{
			currentRoomX --;
			hudPosMarker.transform.Translate(-hudmovedelta, 0, 0);
			xMovement = 16;
			yMovement = 0;
		}

		//DISPOSE OF ENEMIES
		print(enemies.Count);
		foreach(GameObject e in enemies) 
		{
			if(e != null)
				Destroy(e);
		}
		enemies.Clear();

		linkRef.SendMessage("setMovementEnabled", false);
		linkRef.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement * 0.93f, yMovement * 0.9f, desiredDisplacementTime));

		int counter = 0;
		foreach(GameObject t in activeTiles)
		{
			t.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement, yMovement, desiredDisplacementTime));
			oldTiles[counter] = t;
			counter ++;
		}
		populateRoom(currentRoomX, currentRoomY, -xMovement * headStartFactor, -yMovement * headStartFactor);
		foreach(GameObject t in activeTiles)
		{
			t.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement, yMovement, desiredDisplacementTime));
		}
	}

	void disposeOfArray(GameObject[] oldTiles)
	{
		foreach(GameObject g in oldTiles)
		{
			Destroy(g);
		}
	}

	public void setNewSpawnValueForCurrentTile(string s)
	{
		highlightedTile.spawnVal = s;
		storedTiles[highlightedTile.index].spawnval = s;
	}

	public void saveEnemyMapFile()
	{
		string s = "";
		foreach(Tile t in storedTiles)
		{
			s += t.spawnval + ' ';
		}
		System.IO.File.WriteAllText(@"Assets/Resources/EnemyTileMap.txt", s);
	}
}
