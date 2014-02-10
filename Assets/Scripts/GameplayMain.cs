using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayMain : MonoBehaviour {

	//overworld map is 256x88 tiles
	public GameObject MapTile;

	//ENEMY PREFABS
	public GameObject OctorokRed;
	public GameObject OctorokBlue;
	public GameObject TektikeRed;
	public GameObject TektikeBlue;
	public GameObject LeeverRed;
	public GameObject LeeverBlue;
	public GameObject LynelRed;
	public GameObject LynelBlue;
	public GameObject MoblinRed;
	public GameObject MoblinBlue;
	public GameObject Peahat;
	public GameObject Zola;
	public GameObject Rock;
	 
	public GameObject SpecialCollisionTile;
	public GameObject NPCEntity;
	public GameObject Fire;
	public GameObject ItemEntity;
	Tile[] storedTiles = new Tile[22528];
	Room[,] storedRooms = new Room[16,8];

	public GameObject[] activeTiles = new GameObject[176];
	GameObject[] oldTiles = new GameObject[176];
	//TODO: NOT CURRENTLY DESTROYING ALL ENEMIES

	public List<GameObject> enemies = new List<GameObject>();
	List<GameObject> specialTiles = new List<GameObject>();
	List<GameObject> NPCObjects = new List<GameObject>();

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
		// load saves
		Utils.that.loadSaves();

		//GUI
		matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));
		bitFont = Resources.Load("Fonts/prstartk") as Font;

		//CAM AND DEBUG GUI
		cam = GameObject.Find("MainCamera");
		dms = cam.GetComponent("debugMapScript") as debugMapScript;

		linkRef = GameObject.FindGameObjectsWithTag("Player")[0];
		hudPosMarker = GameObject.FindGameObjectsWithTag("hudposmarker")[0];

		//LOAD PREFABS
		MapTile = Resources.Load("MapTile") as GameObject;
		SpecialCollisionTile = Resources.Load("SpecialCollisionTile") as GameObject;
		NPCEntity = Resources.Load("NPCEntity") as GameObject;
		Fire = Resources.Load("Fire") as GameObject;
		ItemEntity = Resources.Load("ItemEntity") as GameObject;

		OctorokRed = Resources.Load("Enemies/OctorokRed") as GameObject;
		OctorokBlue = Resources.Load("Enemies/OctorokBlue") as GameObject;
		TektikeRed = Resources.Load("Enemies/TektikeRed") as GameObject;
		TektikeBlue = Resources.Load("Enemies/TektikeBlue") as GameObject;
		LeeverRed = Resources.Load("Enemies/LeeverRed") as GameObject;
		LeeverBlue = Resources.Load("Enemies/LeeverBlue") as GameObject;
		LynelRed = Resources.Load("Enemies/LynelRed") as GameObject;
		LynelBlue = Resources.Load("Enemies/LynelBlue") as GameObject;
		MoblinRed = Resources.Load("Enemies/MoblinRed") as GameObject;
		MoblinBlue = Resources.Load("Enemies/MoblinBlue") as GameObject;
		Peahat = Resources.Load("Enemies/Peahat") as GameObject;
		Zola = Resources.Load("Enemies/Zola") as GameObject;
		Rock = Resources.Load("Enemies/Rock") as GameObject;
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
				if(intcode >= 88)
				{
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
		if(currentRoom.xcoord == 7 && currentRoom.ycoord == 7)
		{

			print("HERE");
		}

		GameObject EnemyHolder = new GameObject("EnemyHolder");
		EnemyHolder.transform.parent = LvlCamera.transform;

		int debugOne = 0;

		foreach(GameObject t in activeTiles)
		{
			MapTileScript mts = t.GetComponent("MapTileScript") as MapTileScript;
			int intcode = int.Parse(mts.code);

			//ONLY CONSIDER ENEMY CODES (REFER TO CODE KEY GOOGLE DOC)
			if(intcode > 0 && intcode < 88)
			{
				float xVal = t.transform.position.x + 0.5f;
				float yVal = t.transform.position.y - 0.5f;
				GameObject enemy;

				switch(mts.code)
				{
				case "01":
					enemy = Instantiate(OctorokRed, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "02":
					enemy = Instantiate(TektikeRed, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "03":
					enemy = Instantiate(Zola, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "04":
					enemy = Instantiate(LeeverBlue, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "05":
					enemy = Instantiate(Peahat, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "06":
					enemy = Instantiate(LeeverRed, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "07":
					enemy = Instantiate(OctorokBlue, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "08":
					enemy = Instantiate(TektikeBlue, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "09":
					enemy = Instantiate(MoblinRed, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "10":
					enemy = Instantiate(MoblinBlue, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "11":
					enemy = Instantiate(LynelRed, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "12":
					enemy = Instantiate(LynelBlue, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				case "13":
					enemy = Instantiate(Rock, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				default:
					enemy = Instantiate(OctorokRed, new Vector3(xVal, yVal, 0), Quaternion.identity) as GameObject;
					break;
				}

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

		foreach(GameObject npc in NPCObjects)
		{
			Destroy(npc);
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
		}

		if(desiredSpeech == "" && !screenScrolling && MoveCurtain.isMoved)
			linkRef.SendMessage("setMovementEnabled", true);

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

		// delete items
		Utils.that.deleteItems();

		//IF UNUSUAL ROOM TRANSITION...
		if(specialExitCode != "00")
		{
			destroyCurrentRoom();
			screenScrolling = false;
			IntPair newCoords = getRoomCoords(specialExitCode);
			populateRoomWithCoords(newCoords.x, newCoords.y, 0, 0);
			initEnemiesCurrentRoom();
			//tilecode = 12. xcoord and ycoord look unset.
			Tile destinationTile = currentRoom.findTileWithCode(specialExitCode);
			linkRef.transform.position = new Vector3(topLeftX + destinationTile.xcoord, topLeftY - destinationTile.ycoord, 0);
		}
		else //ELSE, SCROLL TRANSITION AS NORMAL
		{
			screenScrolling = true;
			linkRef.SendMessage("setMovementEnabled", false);
			linkRef.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement * 0.92f, yMovement * 0.89f, desiredDisplacementTime));

			int counter = 0;
			foreach(GameObject t in activeTiles)
			{
				t.SendMessage("setDesiredDisplacementTime", new Vector3(xMovement, yMovement, desiredDisplacementTime));
				oldTiles[counter] = t;
				counter ++;
			}
			populateRoomWithCoords(newRoomX, newRoomY, -xMovement, -yMovement);
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

		//System.IO.File.WriteAllText("Assets/Resources/EnemyTileMap.txt", s);

	}

	public IntPair getRoomCoords(string val)
	{
		audio.Play();
		if(val == "99") return new IntPair(7, 7);
		if(val == "98") return new IntPair(6, 6);
		if(val == "97") return new IntPair(4, 6);
		if(val == "96") return new IntPair(5, 7);
		if(val == "95") return new IntPair(4, 4); //K
		if(val == "94") return new IntPair(10, 0); //I
		if(val == "93") return new IntPair(12, 0);
		if(val == "92") return new IntPair(14, 0);
		if(val == "91") return new IntPair(15, 6);

		if(val == "90") return new IntPair(14, 5); //M
		if(val == "89") return new IntPair(10, 4); //K
		if(val == "88") return new IntPair(0, 7);
		return new IntPair(-1, -1);
	}

	//Called by SpecialCollisionTiles when link runs into them.
	//For example, the black tile to the First Old Man.
	public void collideWithSpecialCode(string code)
	{
		disposeAllEnemies();
		destroyCurrentRoom();
		audio.Stop();
		if(code == "88")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("oldladytalker"));
			
			setDesiredSpeechString("PAY ME AND I'LL TALK. TALK ME AND I'LL PAY! HAHAHA");
			GameObject trader = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			trader.SendMessage("setSprite", "oldlady");
			NPCObjects.Add(trader);
			
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "89")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader6"));
			
			setDesiredSpeechString("I HAVE NOTHIN TA' SELL!");
			GameObject trader = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			trader.SendMessage("setSprite", "trader");
			NPCObjects.Add(trader);
			
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "90")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader5"));
			
			setDesiredSpeechString("GANON STOLE MY WARES!");
			GameObject trader = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			trader.SendMessage("setSprite", "trader");
			NPCObjects.Add(trader);
			
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "91")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader4"));

			setDesiredSpeechString("YOUR MONEY'S NO GOOD HERE. LITERALLY!");
			GameObject trader = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			trader.SendMessage("setSprite", "trader");
			NPCObjects.Add(trader);
			
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "92")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("empty"));
		}
		if(code == "93")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader3"));
			
			setDesiredSpeechString("NO TRIFORCE, NO TETRA, NO TRADE.");
			GameObject trader = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			trader.SendMessage("setSprite", "trader");
			NPCObjects.Add(trader);
			
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "94")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("whitesword"));
			
			if(!Inventory.hasWhiteSword)
			{
				setDesiredSpeechString("IT USED TO BE WHITE! ;D");
				GameObject oldman = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
				oldman.SendMessage("setSprite", "oldman");
				NPCObjects.Add(oldman);
				GameObject sword = Instantiate(ItemEntity, new Vector3(0, -2.5f, -1), Quaternion.identity) as GameObject;
				sword.SendMessage("setItemName", "woodensword");
				NPCObjects.Add(sword);
			}
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "95")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader2"));
			
			setDesiredSpeechString("I HAVE THIN AIR TO SELL YOU!");
			GameObject trader = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			trader.SendMessage("setSprite", "trader");
			NPCObjects.Add(trader);
			
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "96")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("oldladygrave"));
			
			setDesiredSpeechString("MEET THE OLD MAN AT THE GRAVE. CREEPY!");
	
			GameObject oldlady = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			oldlady.SendMessage("setSprite", "oldlady");
			NPCObjects.Add(oldlady);
			
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "97")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("oldladymedicine"));
			
			setDesiredSpeechString("BUY MEDICINE BEFORE YOU GO...OR JUST GO.");
			GameObject trader = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			trader.SendMessage("setSprite", "oldlady");
			NPCObjects.Add(trader);
			
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}
		if(code == "98")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("trader1"));

			setDesiredSpeechString("LIFE'S HARD FOR A STARTUP IN HYRULE!");
			GameObject trader = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
			trader.SendMessage("setSprite", "trader");
			NPCObjects.Add(trader);

			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}

		if(code == "99")
		{
			populateRoomWithRoom(MapTileEnum.getNpcRoom("oldmanwoodensword"));

			if(!Inventory.hasWoodenSword)
			{
				setDesiredSpeechString("IT'S DANGEROUS TO GO ALONE! TAKE THIS.");
				GameObject oldman = Instantiate(NPCEntity, new Vector3(0, -1, -1), Quaternion.identity) as GameObject;
				oldman.SendMessage("setSprite", "oldman");
				NPCObjects.Add(oldman);
				GameObject sword = Instantiate(ItemEntity, new Vector3(0, -2.5f, -1), Quaternion.identity) as GameObject;
				sword.SendMessage("setItemName", "woodensword");
				NPCObjects.Add(sword);
			}
			NPCObjects.Add(Instantiate(Fire, new Vector3(-3, -1, -1), Quaternion.identity) as GameObject);
			NPCObjects.Add(Instantiate(Fire, new Vector3(3, -1, -1), Quaternion.identity) as GameObject);
			GameAudio.playStairs();
		}

		if(code == "102")
		{
			Application.LoadLevel("dungeon");
		}

		Tile destinationTile2 = currentRoom.tiles[7, 9];
		linkRef.transform.position = new Vector3(topLeftX + destinationTile2.xcoord, topLeftY - destinationTile2.ycoord, 0);
	
	}

	void OnGUI()
	{
		GUI.matrix = matrix;
		GUIStyle style = GUI.skin.GetStyle("Label");
		style.font = bitFont;
		style.fontSize = 8;
		GUI.Label (new Rect(35, 100, 200, 30), currentSpeech, style);

		speechTimer --;
		if(speechTimer <= 0)
		{
			if(desiredSpeech.Length > 0)
			{
				GameAudio.playText();
				currentSpeech += desiredSpeech[0];
				desiredSpeech = desiredSpeech.Remove(0, 1);
				speechTimer = 12;
			}
		}
	}

	void setDesiredSpeechString(string s)
	{
		linkRef.SendMessage("setMovementEnabled", false);
		currentSpeech = "";
		desiredSpeech = s;
		speechTimer = 0;
	}

	public void acquireItem(string itemName)
	{
		//print("HELLO");
		bool importantItem = false;
		switch(itemName)
		{
			case "woodensword":
				Inventory.hasWoodenSword = true;
				importantItem = true;
				GameObject.Find("HUDwoodenSwordN").GetComponent<SpriteRenderer>().enabled = true;
				GameAudio.playItemObtained();
				GameAudio.playItemReceived();
				PlayerPrefs.SetInt("hasSword", 1);
				break;
			case "nothing":
				//print("ERROR: acquired item has no name defined");
				break;
		}

		if(importantItem)
		{
			linkRef.SendMessage("executeItemPose");
		}
	}
}
