using UnityEngine;
using System.Collections;

public delegate void Callback ();

public class Utils : MonoBehaviour {

	public static Utils that;

	GameObject itemHolder;

	void Awake(){
		that = this;
		itemHolder = GameObject.Find ("ItemHolder");
	}

	public IEnumerator MoveToPosition(Transform tForm, Vector3 newPos, float time, Callback callback){
		float elapsedTime = 0;
		Vector3 startingPos = tForm.position;

		while (elapsedTime < time){
			tForm.position = Vector3.Lerp(startingPos, newPos, (elapsedTime / time));
			elapsedTime += Time.deltaTime;

			yield return null;

			if(elapsedTime >= time && callback != null){
				callback();
			}
		}
	}

	public void loadSaves(){
		if(PlayerPrefs.GetInt("hasSword") == 1){
			Inventory.hasWoodenSword =  true;
			GameObject.Find("HUDwoodenSwordN").GetComponent<SpriteRenderer>().enabled = true;
		}
		
		if(PlayerPrefs.GetInt("hasBomb") == 1){
			Inventory.hasBomb =  true;
			GameObject.Find("HUDbombAction").GetComponent<SpriteRenderer>().enabled = true;
			
			Link.numBomb = PlayerPrefs.GetInt("numBomb");
			GUIText gt = GameObject.Find ("bombNum").GetComponent<GUIText>();
			gt.text = Link.numBomb.ToString();
		}
	}

	public void deleteItems(){
		foreach(Transform child in itemHolder.transform){
			Destroy(child.gameObject);
		}
	}
}