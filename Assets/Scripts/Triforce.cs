using UnityEngine;
using System.Collections;

public class Triforce : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		Application.LoadLevel("main");
	}
}
