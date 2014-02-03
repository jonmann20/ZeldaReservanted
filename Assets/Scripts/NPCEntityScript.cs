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
			(renderer as SpriteRenderer).sprite = s_oldman;
			break;

			case "trader":
			(renderer as SpriteRenderer).sprite = s_trader;
			break;

			case "oldlady":
			(renderer as SpriteRenderer).sprite = s_oldlady;
			break;
		}
	}
}
