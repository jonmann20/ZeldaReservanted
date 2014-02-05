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

	protected bool waitForExit = false;

	// TODO: make movement on grid
	public abstract void Movement();

	void OnTriggerEnter2D(Collider2D col){
		print("hoya!");
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

	void Awake(){
		rupeePrefab = Resources.Load<GameObject>("Rupee");
		rupee5Prefab = Resources.Load<GameObject>("Rupee5");
		bombPrefab = Resources.Load<GameObject>("Bomb");
		enemyZap = Resources.Load<AudioClip>("Audio/soundEffects/enemyZapped");
		heartItemDropPrefab = Resources.Load<GameObject>("heartItemDrop");

		audioSrc = new GameObject("audioSrc");
		audioSrc.AddComponent<AudioSource>();
		audioSrc.audio.clip = enemyZap;

		//audioSrc.transform.parent = GameplayMain.EnemyAudioSourceHolder.transform;
	}

	public void kill(){
		GameAudio.playEnemyZap();

		dropRandomItem();

		Destroy(this.gameObject);
	}

	void dropRandomItem(){
		int rand = Random.Range(0, 40);

		GameObject item = new GameObject();

		if(rand <= 3){
			item = Instantiate(rupeePrefab, this.transform.position, Quaternion.identity) as GameObject;
		}
		else if(rand >= 4 && rand <= 7){
			item = Instantiate(heartItemDropPrefab, this.transform.position, Quaternion.identity) as GameObject;
		}
		else if(rand >= 8 && rand <= 11){
			item = Instantiate(rupee5Prefab, this.transform.position, Quaternion.identity) as GameObject;
		}
		else if(rand >= 12 && rand <= 15){
			item = Instantiate(bombPrefab, this.transform.position, Quaternion.identity) as GameObject;
		}

		item.transform.parent = GameObject.Find("ItemHolder").transform;
	}

	public void setHealth(int h)
	{
		health = h;
		if(health <= 0)
			kill ();
	}
}
