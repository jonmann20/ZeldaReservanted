using UnityEngine;
using System.Collections;

public class octorokScript : Base_Enemy {

	public Sprite[] spr;
	// Use this for initialization
	void Start () {
		sprRend = gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
		sprRend.sprite = spr[2];
	}
	
	// Update is called once per frame
	void Update () {
		movement();
		animate();
	}

	void animate()
	{
		if(direction == 'n') sprRend.sprite = spr[2];
		else if(direction == 's') sprRend.sprite = spr[1];
		else if(direction == 'e') sprRend.sprite = spr[0];
		else if(direction == 'w') sprRend.sprite = spr[3];
	}
}