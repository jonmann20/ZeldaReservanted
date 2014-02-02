using UnityEngine;
using System.Collections;

public class ItemEntityScript : MonoBehaviour {

	string itemName = "nothing";
	GameplayMain gpm;

	void Awake()
	{
		gpm = GameObject.Find("MainCamera").GetComponent("GameplayMain") as GameplayMain;
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		print("collided!");
		gpm.acquireItem(itemName);
		Destroy(gameObject);
	}

	void Update () {
	
	}

	public void setItemName(string n)
	{
		itemName = n;
	}
}
