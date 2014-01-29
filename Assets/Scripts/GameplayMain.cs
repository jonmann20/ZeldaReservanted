using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayMain : MonoBehaviour {

	//overworld map is 256x88 tiles
	public GameObject MapTile;
	public GameObject Enemy;
	public GameObject SpecialCollisionTile;
	Tile[] storedTiles = new Tile[22528];
	Room[,] storedRooms = new Room[16,8];

	GameObject[] activeTiles = new GameObject[176];
	GameObject[] oldTiles = new GameObject[176];
	//TODO: NOT CURRENTLY DESTROYING ALL ENEMIES

	List<GameObject> enemies = new List<GameObject>();
	List<GameObject> specialTiles = new List<GameObject>();

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;

	public GameObject linkRef;
	public GameObject hudPosMarker;
	
	Room currentRoom;
	Room oldRoom;

	GameObject LvlCamera;
	public static GameObject EnemyAudioSourceHolder;

	//DEBUG
	GameObject cam;
	debugMapScript dms;
	MapTileScript highlightedTile;

	//SCREEN SCROLL
	bool screenScrolling = false;
	float desiredDisplacementTime = 0;

	//GUI
	float virtualWidth = 256.0f;
	float virtualHeight = 240.0f;
	Matrix4x4 matrix;
	Font bitFont;
	string currentSpeech = "";
	string desiredSpeech = "";
	int speechTimer = 12;

	void Awake(){
		LvlCamera = GameObject.Find("LvlCamera");

		EnemyAudioSourceHolder = new GameObject("EnemyAudioSourceHolder");
		EnemyAudioSourceHolder.transform.parent = LvlCamera.transform;
	}
	
	void Start () {
		//GUI
		matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));
		bitFont = Resources.Load("Fonts/prstartk") as Font;
		print("font:");
		print(bitFont);
		//CAM AND DEBUG GUI
		cam = GameObject.Find("MainCamera");
		dms = cam.GetComponent("debugMapScript") as debugMapScript;

		linkRef = GameObject.FindGameObjectsWithTag("Player")[0];
		hudPosMarker = GameObject.FindGameObjectsWithTag("hudposmarker")[0];

		//LOAD PREFABS
		MapTile = Resources.Load("MapTile") as GameObject;
		SpecialCollisionTile = Resources.Load("SpecialCollisionTile") as GameObject;

		Enemy = Resources.Load("Enemies/OctorokRed") as GameObject;
		//Enemy = Resources.Load("Enemies/TektikeBlue") as GameObject;

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
				storedTiles[tileNumber].code = temp;
				tileNumber ++;
				temp = "";
			}
		}

		initRooms();
		populateRoomWithCoords(7, 7, 0, 0);
	}

	void populateRoomWithCoords(int roomX, int roomY, float offsetX, float offsetY)
	{
		currentRoom = storedRooms[roomX, roomY];
		int objectCount = 0;
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				GameObject go = Instantiate(MapTile, new Vector3(topLeftX + i + offsetX, topLeftY - j + offsetY, 0), Quaternion.identity) as GameObject;

				go.SendMessage("setTileCode", currentRoom.tiles[i, j].tilecode);
				go.SendMessage("setCode", currentRoom.tiles[i, j].code);
				go.SendMessage("setIndex", currentRoom.tiles[i, j].index);
				go.SendMessage("updateSprite");

				//CHECK FOR SPECIAL TILE
				string code = currentRoom.tiles[i, j].code;
				int intcode = int.Parse(code);
				if(intcode >= 90)
				{
					print("SPECIAL TILE: " + intcode.ToString());
					GameObject sct = Instantiate(SpecialCollisionTile, new Vector3(topLeftX + i + offsetX + 0.5f, topLeftY - j + offsetY - 0.5f, 0), Quaternion.identity) as GameObject;
	
					SpecialCollisionTileScript script = sct.GetComponent("SpecialCollisionTileScript") as SpecialCollisionTileScript;
					script.code = code;
					specialTiles.Add (sct);
				}

				activeTiles[objectCount] = go;
				objectCount ++; 
			}
		}
	}

	void populateRoomWithRoom(Room r)
	{
		currentRoom = r;

		GameObject TileHolder = new GameObject("TileHolder");
		TileHolder.transform.parent = LvlCamera.transform;

		int objectCount = 0;
		for(int i = 0; i < 16; i++)
		{
			for(int j = 0; j < 11; j++)
			{
				GameObject go = Instantiate(MapTile, new Vector3(topLeftX + i, topLeftY - j, 0), Quaternion.identity) as GameObject;
				go.transform.parent = TileHolder.transform;
				MapTileScript mts = go.GetComponent("MapTileScript") as MapTileScript;

				go.SendMessage("setTileCode", r.tiles[i, j].tilecode);
				go.SendMessage("setCode", r.tiles[i, j].code);
				go.SendMessage("setIndex", -1);
				go.SendMessage("updateSprite");
				
				activeTiles[objectCount] = go;
				objectCount ++; 
			}
		}
	}

	void initEnemiesCurrentRoom()
	{
		GameObject EnemyHolder = new GameObject("EnemyHolder");
		EnemyHolder.transform.parent = LvlCamera.transform;

		foreach(GameObject t in activeTiles)
		{
			MapTileScript mts = t.GetComponent("MapTileScript") as MapTileScript;
			if(mts.code == "01")
			{
				float xVal = t.transform.position.x + 0.5f;
				float yVal = t.transform.position.y - 0.5f;
				GameObject enemy = Instantiate(Enemy, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
				enemy.transform.parent = EnemyHolder.transform;
				enemies.Add(enemy);
			}
		}
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
		setDesiredSpeechString("");
		foreach(GameObject t in activeTiles)
		{
			Destroy(t);
		}
		foreach(GameObject st in specialTiles)
		{
			Destroy(st);
		}
	}

	void Update () {
		//RAYCAST STUFF
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if(hit.collider != null && Input.GetMouseButtonDown(0))
		{
			MapTileScript s = hit.collider.gameObject.GetComponent("MapTileScript") as MapTileScript;
			dms.setText(s.code, s.tilecode);
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

		//SHOULD ROOM TRANSITION??
		//////////////////////////
		if(!screenScrolling)
		{
			//n
			if(linkRef.transform.position.y > 3)
			{
				transitionRoom('n');
			}
			//e
			if(linkRef.transform.position.x > 7.5)
			{
				transitionRoom('e');
			}
			//s
			if(linkRef.transform.position.y < -7)
			{
				transitionRoom('s');
			}
			//w
			if(linkRef.transform.position.x < -7.5)
			{
				transitionRoom('w');
			}
		}
	}

	void disposeAllEnemies()
	{
		//DISPOSE OF ENEMIES
		foreach(GameObject e in enemies) 
		{
			if(e != null)
				Destroy(e);
		}
		enemies.Clear();
	}

	//'n'=north, 'e'=east, 's'=south, 'w'=west
	void transitionRoom(char d)
	{
		disposeAllEnemies();
		disposeOfSpecialTiles();

		desiredDisplacementTime = 90;

		int newRoomX = currentRoom.xcoord;
		int newRoomY = currentRoom.ycoord;

		//give the new tiles a head-start to remove room-break line.
		float headStartFactor = 1.00f;
		float hudmovedelta = 0.2f;
		int xMovement = 0;
		int yMovement = 0;

		//SPECIAL EXITS
		string specialExitCode = "00";

		if(d == 'n')
		{
			newRoomY --;
			hudPosMarker.transform.Translate(0, hudmovedelta, 0);
			xMovement = 0;
			yMovement = -11;
			specialExitCode = currentRoom.exitNorth;
		}
		else if(d == 'e')
		{
			hudPosMarker.transform.Translate(hudmovedelta, 0, 0);
			newRoomX ++;
			xMovement = -16;
			yMovement = 0;
			specialExitCode = currentRoom.exitEast;
		}
		else if(d == 's')
		{
			newRoomY ++;
			hudPosMarker.transform.Translate(0, -hudmovedelta, 0);
			xMovement = 0;
			yMovement = 11;
			specialExitCode = currentRoom.exitSouth;
		}
		else if(d == 'w')
		{
			newRoomX --;
			hudPosMarker.transform.Translate(-hudmovedelta, 0, 0);
			xMovement = 16;
			yMovement = 0;
			specialExitCode = currentRoom.exitWest;
		}

		//IF UNUSUAL ROOM TRANSITION...
		if(specialExitCode != "00")
		{
			destroyCurrentRoom();
			screenScrolling = false;
			IntPair newCoords = getRoomCoords(specialExitCode);
			populateRoomWithCoords(newCoords.x, newCoords.y, 0, 0);
			//tilecode = 12. xcoord and ycoord look unset.
			Tile destinationTile = currentRoom.findTileWithCode(specialExitCode);
			linkRef.transform.position = new Vector3(topLeftX + destinationTile.xcoord, topLeftY - destinationTile.ycoord, 0);
		}
		else //ELSE, SCROLL TRANSITION AS NORMAL
		{
			screenScrolling = true;
			linkRef.SendMessage("setMovementEnabled", false);
			linkRef.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement * 0.93f, yMovement * 0.9f, desiredDisplacementTime));

			int counter = 0;
			foreach(GameObject t in activeTiles)
			{
				t.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement, yMovement, desiredDisplacementTime));
				oldTiles[counter] = t;
				counter ++;
			}
			populateRoomWithCoords(newRoomX, newRoomY, -xMovement * headStartFactor, -yMovement * headStartFactor);
			foreach(GameObject t in activeTiles)
			{
				t.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement, yMovement, desiredDisplacementTime));
			}
			foreach(GameObject st in specialTiles)
			{
				if(st != null)
					st.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement, yMovement, desiredDisplacementTime));
			}
		}
	}

	void disposeOfArray(GameObject[] oldTiles)
	{
		foreach(GameObject g in oldTiles)
		{
			Destroy(g);
		}
	}

	void disposeOfSpecialTiles()
	{
		foreach(GameObject st in specialTiles)
		{
			Destroy(st);
		}
	}

	public void setNewSpawnValueForCurrentTile(string s)
	{
		highlightedTile.code = s;
		storedTiles[highlightedTile.index].code = s;
	}

	public void saveEnemyMapFile()
	{
		string s = "";
		foreach(Tile t in storedTiles)
		{
			s += t.code + ' ';
		}
		//System.IO.File.WriteAllText(@"Assets/Resources/EnemyTileMap.txt", s);
	}

	public IntPair getRoomCoords(string val)
	{
		audio.Play();
		if(val == "99") return new IntPair(7, 7);
		if(val == "98") return new IntPair(6, 6);
		if(val == "97") return new IntPair(4, 6);
		if(val == "96") return new IntPair(5, 7);
		if(val == "95") return new IntPair(4, 4);
		if(val == "94") return new IntPair(10, 0);
		if(val == "93") return new IntPair(12, 0);
		if(val == "92") return new IntPair(14, 0);
		if(val == "91") return new IntPair(15, 6);
		return new IntPair(-1, -1);
	}

	//Called by SpecialCollisionTiles when link runs into them.
	//For example, the black tile to the First Old Man.
	public void collideWithSpecialCode(string code)
	{
		disposeAllEnemies();
		destroyCurrentRoom();
		audio.Stop();
		if(code == "91")
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader4"));
		if(code == "92")
			populateRoomWithRoom(MapTileEnum.getNpcRoom("empty"));
		if(code == "93")
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader3"));
		if(code == "94")
			populateRoomWithRoom(MapTileEnum.getNpcRoom("whitesword"));
		if(code == "95")
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader2"));
		if(code == "96")
			populateRoomWithRoom(MapTileEnum.getNpcRoom("oldladygrave"));
		if(code == "97")
			populateRoomWithRoom(MapTileEnum.getNpcRoom("oldladymedicine"));
		if(code == "98")
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader1"));
		if(code == "99")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("oldmanwoodensword"));
			setDesiredSpeechString("IT'S DANGEROUS TO GO ALONE! TAKE THIS.");
		}

		Tile destinationTile2 = currentRoom.tiles[7, 9];
		linkRef.transform.position = new Vector3(topLeftX + destinationTile2.xcoord, topLeftY - destinationTile2.ycoord, 0);
	}
		
	void OnGUI () {

		GUI.matrix = matrix;
		GUIStyle style = GUI.skin.GetStyle("Label");
		style.font = bitFont;
		style.fontSize = 8;
		GUI.Label (new Rect(30, 100, 200, 30), currentSpeech, style);

		speechTimer --;
		if(speechTimer <= 0)
		{
			if(desiredSpeech.Length > 0)
			{
				currentSpeech += desiredSpeech[0];
				desiredSpeech = desiredSpeech.Remove(0, 1);
				speechTimer = 12;
			}
			else
				if(!screenScrolling)
					linkRef.SendMessage("setMovementEnabled", true);
		}
	}

	void setDesiredSpeechString(string s)
	{
		linkRef.SendMessage("setMovementEnabled", false);
		currentSpeech = "";
		desiredSpeech = s;
		speechTimer = 0;
	}
}
