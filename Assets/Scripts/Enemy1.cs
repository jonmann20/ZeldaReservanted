using UnityEngine;
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
<<<<<<< HEAD
=======
		//Debug.Log("Collision2D");
>>>>>>> f60057d2e0d70f3a9c85ef550685e80bfdd6bec9
		if(col.gameObject.tag == "wall"){

		}
		else {

		}
	}
}
