using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "wall"){

		}
		else {

		}
	}
}
