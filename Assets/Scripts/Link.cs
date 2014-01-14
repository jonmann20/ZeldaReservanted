using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	public Sprite[] spr;

	float speed = 0.1f;

	SpriteRenderer sprRend;

	void Start () {
		sprRend = renderer as SpriteRenderer;
	}

	void Update () {
		if(Input.GetKey(KeyCode.W)){
			sprRend.sprite = spr[2];
			transform.Translate(new Vector2(0, speed));
		}	

		if(Input.GetKey(KeyCode.S)){
			sprRend.sprite = spr[0];
			transform.Translate(new Vector2(0, -speed));
		}

		if(Input.GetKey(KeyCode.A)){
			sprRend.sprite = spr[1];
			transform.Translate(new Vector2(-speed, 0));
		}

		if(Input.GetKey(KeyCode.D)){
			sprRend.sprite = spr[3];
			transform.Translate(new Vector2(speed, 0));
		}
	}
}
