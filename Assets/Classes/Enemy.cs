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

	protected GameObject rupeePrefab;

	public abstract void Movement();

	void Awake(){
		rupeePrefab = Resources.Load<GameObject>("Rupee");
		enemyZap = Resources.Load<AudioClip>("Audio/soundEffects/enemyZapped");
		
		audioSrc = new GameObject("audioSrc");
		audioSrc.AddComponent<AudioSource>();
		audioSrc.audio.clip = enemyZap;

		audioSrc.transform.parent = GameplayMain.EnemyAudioSourceHolder.transform;
	}

	public void kill(){
		audioSrc.audio.Play();
		Instantiate(rupeePrefab, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}

	// TODO: make movement on grid
}
