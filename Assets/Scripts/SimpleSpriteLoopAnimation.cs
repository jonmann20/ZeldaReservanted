using UnityEngine;
using System.Collections;

public class SimpleSpriteLoopAnimation : MonoBehaviour {

	public Sprite[] spr;
	public float fps;
	
	SpriteRenderer sprRend;
	
	void Start () {
		sprRend = GetComponent<Renderer>() as SpriteRenderer;
	}
	
	void Update () {
		int idx = (int)(Time.timeSinceLevelLoad * fps);
		idx = idx % spr.Length;
		sprRend.sprite = spr[idx];
	}
}
