using UnityEngine;
using System.Collections;

public class GameplayMain : MonoBehaviour {
	//overworld map is 256x88 tiles
	public GameObject MapTile;
	Tile[] tiles = new Tile[22528]; //fills to 22441 //Odd: 256x88 tiles says there should be 22528 tiles
	Room[,] rooms = new Room[16,8];
	public float topLeftX = -8f;
	public float topLeftY = 3.5f;

	int currentRoomX = 7;
	int currentRoomY = 8;
	
	void Start () {
		Debug.Log("attempting load");
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
				tiles[tileNumber] = tempTile;
				tileNumber ++;
				temp = "";
			}
		}
		//File seems to end in space || endline
		//So no 'final tile'
		Debug.Log("finished loading " + tileNumber + " tiles");
		initRooms();
		populateRoom(7, 7);
	}

	void populateRoom(int roomX, int roomY)
	{
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				Debug.Log("i: " + i + " j: " + j + "hex: " + rooms[roomX, roomY].tiles[i, j].hexval);
				GameObject go = Instantiate(MapTile, new Vector3(topLeftX + i, topLeftY - j, 0), Quaternion.identity) as GameObject;
				go.SendMessage("setHexVal", rooms[roomX, roomY].tiles[i, j].hexval);
				go.SendMessage("updateSprite");
			}
		}
		Debug.Log("finished populating room (" + roomX + ", " + roomY + ")");
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
		Debug.Log("Finished init rooms");
	}

	//fill room at coordinates (<roomX>, <roomY>) with tiles from dataset
	void fillRoom(int roomX, int roomY)
	{
		Tile[,] roomTiles = new Tile[16,11];
		for(int i = 0; i < 15; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				int tileSpot = i + j * 256 + roomY * 2816 + roomX * 16;
				roomTiles[i,j] = tiles[tileSpot];
			}
		}

		Room tempRoom = new Room(roomX, roomY, roomTiles);
		rooms[roomX, roomY] = tempRoom;
		Debug.Log("finished filling room (" + roomX + ", " + roomY + ")");
	}

	void Update () {
		
	}
}
