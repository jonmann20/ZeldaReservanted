using UnityEngine;
using System.Collections;

public class ItemEntityScript : MonoBehaviour {

	string itemName = "nothing";
	GameplayMain gpm;

	void Awake()
	{
		gpm = GameObject.Find("MainCamera").GetComponent("GameplayMain") as GameplayMain;
	}

	void OnTriggerEnter2D (Collider2D coll){
		gpm.acquireItem(itemName);

		// TODO: Destroy...  GameObject.find("NPCEntity") 

		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		gameObject.transform.position = Link.that.transform.position + new Vector3(-0.288f, 1f, 0);
		Destroy(gameObject, 2.365f);
	}

	public void setItemName(string n){
		itemName = n;
	}
}
