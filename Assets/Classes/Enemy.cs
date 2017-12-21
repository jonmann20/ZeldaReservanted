using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {

	protected GameObject audioSrc;
	protected AudioClip enemyZap;
	public int health = 1;
	public Sprite[] spr;

	Color initialColor;
	const int COLOR_TIME = 5;
	int colorTimer = COLOR_TIME;
	bool colorFlip = false;

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;
	public SpriteRenderer sprRend;
	protected SpriteDir direction;

	public int invincibility = 0;

	protected GameObject rupeePrefab, rupee5Prefab, heartItemDropPrefab, bombPrefab;

	public GameplayMain gpm;

	protected bool waitForExit = false;

	public Vector2 currentCoordsInRoom;

	public Sprite poof;
	public int poofTimer = 10;
	public bool didPoof = false;

	public abstract void Movement();

	void Awake(){
		invincibility = 0;
		gpm = (GameObject.Find("MainCamera") as GameObject).GetComponent("GameplayMain") as GameplayMain;
		rupeePrefab = Resources.Load<GameObject>("Rupee");
		rupee5Prefab = Resources.Load<GameObject>("Rupee5");
		bombPrefab = Resources.Load<GameObject>("Bomb");
		enemyZap = Resources.Load<AudioClip>("Audio/soundEffects/enemyZapped");
		heartItemDropPrefab = Resources.Load<GameObject>("heartItemDrop");
		
		audioSrc = new GameObject("audioSrc");
		audioSrc.AddComponent<AudioSource>();
		audioSrc.GetComponent<AudioSource>().clip = enemyZap;

		initialColor = (GetComponent<Renderer>() as SpriteRenderer).color;
		initCoordsInRoom();
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Sword")
		{
			setHealth(health - 1);
		}
	}
	/*
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			--Link.health;
			Link.updateHealth();
		}
	}
*/

	void Update()
	{
		if(invincibility > 0)
			invincibility --;
		customUpdate();



		if(colorTimer > 0)
			colorTimer --;
		else
		{
			colorTimer = COLOR_TIME;
			colorFlip = !colorFlip;
		}

		if(invincibility > 0) 
		{
			if(colorFlip) (GetComponent<Renderer>() as SpriteRenderer).color = Color.red;
			if(!colorFlip) (GetComponent<Renderer>() as SpriteRenderer).color = Color.white;
		}
		else
			(GetComponent<Renderer>() as SpriteRenderer).color = initialColor;
	}

	public virtual void customUpdate()
	{

	}

	public virtual void justHurt()
	{
		
	}

	public virtual void killed()
	{

	}

	protected void initCoordsInRoom()
	{
		float topLeftX = -8f;
		float topLeftY = 3.5f;
		
		float currentX = transform.position.x;
		float currentY = transform.position.y;
		
		float tempX = currentX - topLeftX;
		float tempY = topLeftY - currentY;

		//WILL DROP DECIMALS. SHOULD BE ACCURATE.
		currentCoordsInRoom = new Vector2((int)tempX, (int)tempY);
	}

	public void kill(){
		killed ();
		GameAudio.playEnemyZap();

		dropRandomItem();

		BossScript bs = gameObject.GetComponent<BossScript>();
		if(bs != null){
			DungeonRooms.that.addStairsBoss();
		}

		Destroy(this.gameObject);
	}

	void dropRandomItem(){
		int rand = Random.Range(0, 40);

		GameObject item = new GameObject();

		if(rand <= 3){
			item = Instantiate(rupeePrefab, this.transform.position, Quaternion.identity) as GameObject;
		}
		else if(rand >= 4 && rand <= 13){
			item = Instantiate(heartItemDropPrefab, this.transform.position, Quaternion.identity) as GameObject;
		}
		else if(rand >= 14 && rand <= 17){
			item = Instantiate(rupee5Prefab, this.transform.position, Quaternion.identity) as GameObject;
		}
		else if(rand >= 18 && rand <= 21){
			item = Instantiate(bombPrefab, this.transform.position, Quaternion.identity) as GameObject;
		}

		item.transform.parent = GameObject.Find("ItemHolder").transform;
	}

	//ONLY DESCREASE HEALTH IF invincibility <= 0
	public void setHealth(int h)
	{
		if(h < health && invincibility > 0)
			return;
		if(h < health)
		{
			invincibility = 20;
			justHurt();
		}
		health = h;

		if(health <= 0)
			kill ();
	}

	public List<Vector3> getWaterPositions()
	{
		List<Vector3> waterPositions = new List<Vector3>();
		for(int i = 0; i < 176; i++)
		{
			MapTileScript mts = (gpm.activeTiles[i] as GameObject).GetComponent("MapTileScript") as MapTileScript;
			if(MapTileEnum.isWater(mts.tilecode))
				waterPositions.Add(mts.transform.position);
		}

		return waterPositions;
	}

	public List<Vector3> getTraversiblePositions()
	{
		List<Vector3> traversiblePositions = new List<Vector3>();
		for(int i = 0; i < 176; i++)
		{
			MapTileScript mts = (gpm.activeTiles[i] as GameObject).GetComponent("MapTileScript") as MapTileScript;
			if(!MapTileEnum.isWater(mts.tilecode) && !MapTileEnum.isSolid(mts.tilecode))
				traversiblePositions.Add(mts.transform.position);
		}
		
		return traversiblePositions;
	}


	public bool isTileTraversableLand(Vector2 pos)
	{
		if(pos.x < 1 || pos.x > 14 || pos.y < 1 || pos.y > 9)
		{
			//print("pos " + pos.ToString () + " is not traversible because of stage boundaries." );
			return false;
		}
		int index = (int)(pos.x * 11 + pos.y);
		MapTileScript mts = (gpm.activeTiles[index] as GameObject).GetComponent("MapTileScript") as MapTileScript;
		if(MapTileEnum.isSolid(mts.tilecode)
		   || MapTileEnum.isWater(mts.tilecode))
		{
			//print("pos " + pos.ToString () + " is not traversible." );
			return false;
		}

		//print("pos " + pos.ToString () + " is traversible." );
		return true;
	}

	public string getHexValue(Vector2 pos)
	{
		int index = (int)(pos.x * 11 + pos.y);
		MapTileScript mts = (gpm.activeTiles[index] as GameObject).GetComponent("MapTileScript") as MapTileScript;
		return mts.tilecode; 
	}

	public void destroyHexValue(Vector2 pos)
	{
		int index = (int)(pos.x * 11 + pos.y);
		Destroy(gpm.activeTiles[index]);
	}

	//RETURNS 'z' for failure (no items in list)
	public T getRandomElementInList<T>(List<T> L)
	{
		int index = (int)(Random.Range(0.0f, L.Count));
		return L[index];
	}
}
