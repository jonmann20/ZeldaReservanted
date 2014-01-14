using UnityEngine;
using System.Collections;

public class TitleMain : MonoBehaviour {

	public GameObject bg;

	void Start () {
		TextCta cta = bg.GetComponent<TextCta>();
		cta.nextScene = "loadSave";
		Instantiate(bg);
	}
}
