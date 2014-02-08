﻿using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public bool isPickup = true;
	public Sprite explosion;


	void Start () {
		if(!isPickup){
			// not needed
			collider2D.enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(isPickup && (col.gameObject.tag == "Player" || col.gameObject.tag == "Sword")){
			GameObject.Find("HUDbombAction").GetComponent<SpriteRenderer>().enabled = true;

			Link.numBomb += 5;
			Inventory.hasBomb = true;
			PlayerPrefs.SetInt ("hasBomb", 1);
			PlayerPrefs.SetInt("numBomb", Link.numBomb);

			GUIText gt = GameObject.Find ("bombNum").GetComponent<GUIText>();
			gt.text = Link.numBomb.ToString();
			Destroy(gameObject);
			GameAudio.playRupeePickup(1);
		}
	}

	public void setBomb(){
		--Link.numBomb;
		PlayerPrefs.SetInt("numBomb", Link.numBomb);
		GUIText gt = GameObject.Find ("bombNum").GetComponent<GUIText>();
		gt.text = Link.numBomb.ToString();

		// TODO: play bomb audio

		Invoke("blowUp", 1.68f);
	}

	void blowUp(){
		SpriteRenderer spr = renderer as SpriteRenderer;
		spr.sprite = explosion;
		spr.sortingLayerName = "Link";

		transform.localScale = new Vector2(3, 3);
		checkExplosionCollision();
	}

	void checkExplosionCollision(){
		Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, 1.5f);

		foreach(Collider2D col in objectsInRange){
			if(col.gameObject.tag == "Enemy"){
				Destroy(col.gameObject);
			}
			else if(col.gameObject.tag == "BombDoor"){
				DungeonRooms.that.addBombDoor();
			}
		}

		Destroy (gameObject, 0.35f);
	}
}
