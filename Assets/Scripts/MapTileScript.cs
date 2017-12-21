using UnityEngine;
using System.Collections;

public class MapTileScript : MonoBehaviour {
	
	public string tilecode;
	public string code;
	//Use this index to locate Tile file in StoredTiles
	public int index;

	Vector2 desiredDisplacement;
	Vector2 deltaDisplacement;
	float desiredDisplacementTime;

	BoxCollider2D c2d;

	void Awake()
	{
		desiredDisplacementTime = 0;
		c2d = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
		c2d.isTrigger = true;
	}

	public void setTileCode(string tc)
	{
		tilecode = tc;
	}
	public void setCode(string c)
	{
		code = c;
	}
	public void setIndex(int i)
	{
		index = i;
	}
	public void updateSprite()
	{
		GetComponent<SpriteRenderer>().sprite = MapTileEnum.getTileSprite(tilecode);

		bool isSolid = MapTileEnum.isSolid(tilecode);
		bool isWater = MapTileEnum.isWater(tilecode);

		if(isSolid)
		{
			c2d.isTrigger = false;
			gameObject.tag = "physicaltile";
		}
		else if(isWater)
		{
			c2d.isTrigger = false;
			gameObject.tag = "watertile";
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
