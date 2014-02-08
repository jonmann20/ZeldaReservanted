using UnityEngine;
using System.Collections;

public class DeathScene : MonoBehaviour {

	void Start()
	{
		GameAudio.playGameOver();
	}

	void Update () {
		if(Input.GetButtonDown("Enter")){
			Link.health = Link.initHealth;

			Application.LoadLevel("loadSave");
		}
	}
}
