using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	public Sprite[] spr;
	public float speed;

	SpriteRenderer sprRend;

	bool isVert = false;

	void Start () {
		sprRend = renderer as SpriteRenderer;
	}

	void Update () {
		movement();
	}

	void movement(){
		isVert = false;

		if(Input.GetKey(KeyCode.W)){
			sprRend.sprite = spr[2];
			transform.Translate(new Vector2(0, speed));
			isVert = true;
		}
		
		if(Input.GetKey(KeyCode.S)){
			sprRend.sprite = spr[0];
			transform.Translate(new Vector2(0, -speed));
			isVert = true;
		}

		if(!isVert){
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

	void OnCollision2D(Collision2D col){
		if(col.gameObject.tag == "wall"){
			speed = 0;
		}
		else {
			speed = 1;
		}
	}
}
