﻿using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		//transform.Translate(1, 0, 0);
		//Debug.Log("Collision2D");
		if(col.gameObject.tag == "wall"){

		}
		else {

		}
	}
}
