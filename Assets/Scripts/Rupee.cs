﻿using UnityEngine;
using System.Collections;

public class Rupee : MonoBehaviour {

	public int val = 1;

	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player" || col.gameObject.tag == "Sword"){
			Link.numRupee += val;
			GUIText gt = GameObject.Find ("rupeeNum").GetComponent<GUIText>();
			gt.text = Link.numRupee.ToString();
			GameAudio.playRupeePickup(val);

			Destroy(gameObject);
		}
	}
}
