using UnityEngine;
using System.Collections;

public class RupeeNum : MonoBehaviour {
	SpriteRenderer sprRend;
	public Sprite[] spr;

	void Start () {
		sprRend = GetComponent<SpriteRenderer>() as SpriteRenderer;
	}

}
