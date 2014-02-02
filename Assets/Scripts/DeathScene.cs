using UnityEngine;
using System.Collections;

public class DeathScene : MonoBehaviour {
	void Update () {
		if(Input.GetButtonDown("Enter")){
			Link.health = Link.initHealth;

			Application.LoadLevel("loadSave");
		}
	}
}
