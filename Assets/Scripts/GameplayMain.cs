using UnityEngine;
using System.Collections;

public class GameplayMain : MonoBehaviour {
	//overworld map is 256x88 tiles
	public GameObject MapTile;
	Tile[] storedTiles = new Tile[22528]; //fills to 22441 //Odd: 256x88 tiles says there should be 22528 tiles
	Room[,] storedRooms = new Room[16,8];

	GameObject[] activeTiles = new GameObject[176];
	GameObject[] newTiles = new GameObject[176];

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;

	public GameObject linkRef;
	public GameObject hudPosMarker;

	int currentRoomX = 7;
	int currentRoomY = 7;

	//SCREEN SCROLL
	bool screenScrolling = false;
	float desiredDisplacementTime = 0;
	
	void Start () {
		linkRef = GameObject.FindGameObjectsWithTag("Player")[0];
		hudPosMarker = GameObject.FindGameObjectsWithTag("hudposmarker")[0];
		//Debug.Log("attempting load");
		//overworld dataset via http://inventwithpython.com/blog/2012/12/10/8-bit-nes-legend-of-zelda-map-data/
		TextAsset txt = Resources.Load("nes_zelda_overworld_tile_map") as TextAsset;
		string content = txt.text;

		//STRING PARSE
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
				storedTiles[tileNumber] = tempTile;
				tileNumber ++;
				temp = "";
			}
		}
		//File seems to end in space || endline
		//So no 'final tile'
		Debug.Log("finished loading " + tileNumber + " tiles");
		initRooms();
		populateRoom(currentRoomX, currentRoomY, 0, 0);
	}

	void populateRoom(int roomX, int roomY, float offsetX, float offsetY)
	{
		int objectCount = 0;
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				//Debug.Log("i: " + i + " j: " + j + "hex: " + storedRooms[roomX, roomY].tiles[i, j].hexval);
				GameObject go = Instantiate(MapTile, new Vector3(topLeftX + i + offsetX, topLeftY - j + offsetY, 0), Quaternion.identity) as GameObject;
				go.SendMessage("setHexVal", storedRooms[roomX, roomY].tiles[i, j].hexval);
				go.SendMessage("updateSprite");

				activeTiles[objectCount] = go;
				objectCount ++; 
			}
		}
		//Debug.Log("finished populating room (" + roomX + ", " + roomY + ")");
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
		//Debug.Log("Finished init rooms");
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
		//Debug.Log("finished filling room (" + roomX + ", " + roomY + ")");
	}

	void destroyCurrentRoom()
	{
		foreach(GameObject t in activeTiles)
		{
			Destroy(t);
		}
	}

	void Update () {
		if(desiredDisplacementTime > 0)
			desiredDisplacementTime --;
		else
		{
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

		linkRef.SendMessage("setMovementEnabled", false);
		linkRef.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement * 0.93f, yMovement * 0.9f, desiredDisplacementTime));

		foreach(GameObject t in activeTiles)
		{
			t.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement, yMovement, desiredDisplacementTime));
		}
		populateRoom(currentRoomX, currentRoomY, -xMovement * headStartFactor, -yMovement * headStartFactor);
		foreach(GameObject t in activeTiles)
		{
			t.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement, yMovement, desiredDisplacementTime));
		}
	}
}
