using UnityEngine;
using System.Collections;

public class TextCta : MonoBehaviour {
	public KeyCode key = KeyCode.Return;
	public string nextScene;
	
	void Update () {
		if(Input.GetKey(key)){
			Application.LoadLevel(nextScene);
		}
	}
}
