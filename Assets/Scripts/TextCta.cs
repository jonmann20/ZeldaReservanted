using UnityEngine;
using System.Collections;

public class TextCta : MonoBehaviour {
	public string nextScene;

	public 
	
	void Update () {
		if(Input.GetButtonDown("Enter")){
			Application.LoadLevel(nextScene);
		}
	}
}
