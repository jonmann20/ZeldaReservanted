using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	public Sprite[] spr;
	public float speed;

	SpriteRenderer sprRend;

	void Start () {
		sprRend = renderer as SpriteRenderer;
	}

	void Update () {
		movement();
	}

	float vert, hor;
	void movement(){
		vert = Input.GetAxis("Vertical");
		hor = Input.GetAxis("Horizontal");

		if(!Input.GetButton("Up") || !Input.GetButton("Down")){
			if(vert == 1f){
				sprRend.sprite = spr[2];
				transform.Translate(new Vector2(0, speed));
			}
			
			if(vert == -1f){
				sprRend.sprite = spr[0];
				transform.Translate(new Vector2(0, -speed));
			}
		}

		if(vert == 0 && (!Input.GetButton("Left") || !Input.GetButton("Right"))){
			if(hor == -1f){
				sprRend.sprite = spr[1];
				transform.Translate(new Vector2(-speed, 0));
			}
			
			if(hor == 1f){
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
