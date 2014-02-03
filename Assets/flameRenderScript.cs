using UnityEngine;
using System.Collections;

public class flameRenderScript : MonoBehaviour {

	public Sprite s_flame1;
	public Sprite s_flame2;

	int timer = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer ++;
		if(timer == 1)
			(renderer as SpriteRenderer).sprite = s_flame1;
		else if(timer == 6)
			(renderer as SpriteRenderer).sprite = s_flame2;
		else if(timer > 10)
			timer = 0;
	}
}
