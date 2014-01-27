using UnityEngine;
using System.Collections;

public class ItemSprite : MonoBehaviour {

	public Sprite[] spr;
	public static SpriteRenderer sprRend;

	void Start () {
		sprRend = renderer as SpriteRenderer;
	}
}
