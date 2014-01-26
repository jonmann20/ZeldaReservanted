using UnityEngine;
using System.Collections;

public class MapTileScript : MonoBehaviour {
	
	public string hexVal;
	public string spawnVal;
	//Use this index to locate Tile file in StoredTiles
	public int index;

	Vector2 desiredDisplacement;
	Vector2 deltaDisplacement;
	float desiredDisplacementTime;

	BoxCollider2D c2d;

	void Awake()
	{
		desiredDisplacementTime = 0;
		c2d = gameObject.AddComponent("BoxCollider2D") as BoxCollider2D;
		c2d.isTrigger = true;
	}

	public void setHexVal(string h)
	{
		hexVal = h;
	}
	public void setSpawnVal(string s)
	{
		spawnVal = s;
	}
	public void setIndex(int i)
	{
		index = i;
	}
	public void updateSprite()
	{
		GetComponent<SpriteRenderer>().sprite = MapTileEnum.getTileSprite(hexVal);
		if(MapTileEnum.isSolid(hexVal) || MapTileEnum.isWater(hexVal))
		{
			c2d.isTrigger = false;
		}
		if(spawnVal != "00")
		{
			//Instantiate();
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
