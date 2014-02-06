using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

	protected GameObject audioSrc;
	protected AudioClip enemyZap;
	protected int health = 1;
	public Sprite[] spr;

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;
	public SpriteRenderer sprRend;
	protected SpriteDir direction;

	protected GameObject rupeePrefab, rupee5Prefab, heartItemDropPrefab, bombPrefab;

	public GameplayMain gpm;

	protected bool waitForExit = false;

	public Vector2 currentCoordsInRoom;

	// TODO: make movement on grid
	public abstract void Movement();

	void Awake(){
		gpm = (GameObject.Find("MainCamera") as GameObject).GetComponent("GameplayMain") as GameplayMain;
		rupeePrefab = Resources.Load<GameObject>("Rupee");
		rupee5Prefab = Resources.Load<GameObject>("Rupee5");
		bombPrefab = Resources.Load<GameObject>("Bomb");
		enemyZap = Resources.Load<AudioClip>("Audio/soundEffects/enemyZapped");
		heartItemDropPrefab = Resources.Load<GameObject>("heartItemDrop");
		
		audioSrc = new GameObject("audioSrc");
		audioSrc.AddComponent<AudioSource>();
		audioSrc.audio.clip = enemyZap;
		
		initCoordsInRoom();
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			--Link.health;
			Link.updateHealth();
		}
		else if(col.gameObject.tag == "Sword")
		{
			setHealth(health - 1);
		}
	}

	void OnCollisionExit2D(Collision2D col){
		//print ("exit");
		waitForExit = false;
	}

	void initCoordsInRoom()
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
		GameAudio.playEnemyZap();

		//dropRandomItem();
		Instantiate(bombPrefab, this.transform.position, Quaternion.identity);

		Destroy(this.gameObject);
	}

	void dropRandomItem(){
		int rand = Random.Range(0, 40);
		
		if(rand <= 3){
			Instantiate(rupeePrefab, this.transform.position, Quaternion.identity);
		}
		else if(rand >= 4 && rand <= 7){
			Instantiate(heartItemDropPrefab, this.transform.position, Quaternion.identity);
		}
		else if(rand >= 8 && rand <= 11){
			Instantiate(rupee5Prefab, this.transform.position, Quaternion.identity);
		}
		else if(rand >= 12 && rand <= 15){
			Instantiate(bombPrefab, this.transform.position, Quaternion.identity);
		}
	}

	public void setHealth(int h)
	{
		health = h;
		if(health <= 0)
			kill ();
	}

	public bool isTileTraversableLand(Vector2 pos)
	{
		if(pos.x < 1 || pos.x >= 10 || pos.y < 1 || pos.y > 14)
			return false;
		int index = (int)(pos.x * 11 + pos.y);
		MapTileScript mts = (gpm.activeTiles[index] as GameObject).GetComponent("MapTileScript") as MapTileScript;
		if(MapTileEnum.isSolid(mts.tilecode)
		   || MapTileEnum.isWater(mts.tilecode))
			return false;

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
}
