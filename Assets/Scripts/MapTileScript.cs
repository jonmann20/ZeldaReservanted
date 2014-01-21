using UnityEngine;
using System.Collections;

public class MapTileScript : MonoBehaviour {
	
	public string hexVal;

	Vector2 desiredDisplacement;
	Vector2 deltaDisplacement;
	float desiredDisplacementTime;

	void Awake()
	{
		desiredDisplacementTime = 0;
	}

	public void setHexVal(string h)
	{
		hexVal = h;
		//Debug.Log(tile00);
	}
	public void updateSprite()
	{
		GetComponent<SpriteRenderer>().sprite = MapTileEnum.getTileSprite(hexVal);
		if(!MapTileEnum.isSolid(hexVal))
		{
			//Do something?
		}
	}

	//First two elements are X and Y displacement.
	//Third element is number of frames (time).
	public void setDesiredDisplacementTime(Vector3 v)
	{
		desiredDisplacement = new Vector2(v.x, v.y);
		desiredDisplacementTime = v.z;
		deltaDisplacement = desiredDisplacement / (desiredDisplacementTime);
	}

	public void Update()
	{
		if(desiredDisplacementTime > 0)
		{
			transform.Translate(deltaDisplacement);
			desiredDisplacementTime --;
		}
	}
}
