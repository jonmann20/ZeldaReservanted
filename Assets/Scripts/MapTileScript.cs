using UnityEngine;
using System.Collections;

public class MapTileScript : MonoBehaviour {
	
	string hexVal = "";

	//TILE SPRITES
	public Sprite tile43;
	public Sprite tile02;
	public Sprite tile18;
	public Sprite tile24;
	public Sprite tile12;
	public Sprite tile44;
	public Sprite tile42;
	public Sprite tile2f;
	public Sprite tile30;
	public Sprite tile2e;


	public void setHexVal(string h)
	{
		hexVal = h;
	}
	public void updateSprite()
	{
		switch(hexVal)
		{
			case "43":
				GetComponent<SpriteRenderer>().sprite = tile43;
			break;
			case "02":
				GetComponent<SpriteRenderer>().sprite = tile02;
			break;
			case "18":
				GetComponent<SpriteRenderer>().sprite = tile18;
			break;
			case "24":
				GetComponent<SpriteRenderer>().sprite = tile2f;
			break;
			case "12":
				GetComponent<SpriteRenderer>().sprite = tile12;
			break;
			case "44":
				GetComponent<SpriteRenderer>().sprite = tile44;
			break;
			case "42":
				GetComponent<SpriteRenderer>().sprite = tile42;
			break;
			case "2f":
				GetComponent<SpriteRenderer>().sprite = tile2f;
			break;

			case "30":
				GetComponent<SpriteRenderer>().sprite = tile30;
			break;
			case "2e":
				GetComponent<SpriteRenderer>().sprite = tile2e;
			break;
		}
	}
}
