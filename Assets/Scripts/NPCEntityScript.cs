using UnityEngine;
using System.Collections;

public class NPCEntityScript : MonoBehaviour {

	public Sprite s_oldman;
	public Sprite s_trader;
	public Sprite s_oldlady;

	void setSprite(string s)
	{

		switch(s)
		{
			case "oldman":
			(GetComponent<Renderer>() as SpriteRenderer).sprite = s_oldman;
			break;

			case "trader":
			(GetComponent<Renderer>() as SpriteRenderer).sprite = s_trader;
			break;

			case "oldlady":
			(GetComponent<Renderer>() as SpriteRenderer).sprite = s_oldlady;
			break;
		}
	}
}
