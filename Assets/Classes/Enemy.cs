using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

	protected GameObject audioSrc;
	protected AudioClip enemyZap;
	protected int health;
	public Sprite[] spr;

	public float topLeftX = -8f;
	public float topLeftY = 3.5f;
	public SpriteRenderer sprRend;
	protected SpriteDir direction;

	protected GameObject rupeePrefab, rupee5Prefab;

	protected bool waitForExit = false;

	// TODO: make movement on grid
	public abstract void Movement();

//	protected void OnTriggerEnter2d(Collider2D col){
//
//	}

//	protected void OnTriggerExit2d(Collider2D col){
//		waitForExit = false;
//	}

	void OnCollisionExit2D(Collision2D col){
		//print ("exit");
		waitForExit = false;
	}

	void Awake(){
		rupeePrefab = Resources.Load<GameObject>("Rupee");
		rupee5Prefab = Resources.Load<GameObject>("Rupee5");
		enemyZap = Resources.Load<AudioClip>("Audio/soundEffects/enemyZapped");
		
		audioSrc = new GameObject("audioSrc");
		audioSrc.AddComponent<AudioSource>();
		audioSrc.audio.clip = enemyZap;

		audioSrc.transform.parent = GameplayMain.EnemyAudioSourceHolder.transform;
	}

	public void kill(){
		audioSrc.audio.Play();

		int rand = Random.Range(0, 10);

		if(rand <= 2){
			Instantiate(rupeePrefab, this.transform.position, Quaternion.identity);
		}
		else if(rand >= 8){
			Instantiate(rupee5Prefab, this.transform.position, Quaternion.identity);
		}

		Destroy(this.gameObject);
	}
}
