using UnityEngine;
using System.Collections;

public class MapTileScript : MonoBehaviour {
	
	string hexVal = "";

	public void setHexVal(string h)
	{
		hexVal = h;
		//Debug.Log(tile00);
	}
	public void updateSprite()
	{
		GetComponent<SpriteRenderer>().sprite = MapTileEnum.getTileSprite(hexVal);
	}
}
