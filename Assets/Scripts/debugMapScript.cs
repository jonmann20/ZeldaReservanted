using UnityEngine;
using System.Collections;

public class debugMapScript : MonoBehaviour {

	public float xspread = 14f;
	public float yspread = 14f;
	public float width = 14f;
	public float height = 14f;

	GameplayMain gpm; 

	public string spawnVal = "none";
	public string tileVal = "none";

	void Start()
	{
		gpm = GameObject.Find("MainCamera").GetComponent("GameplayMain") as GameplayMain;
	}
	void OnGUI() {
		spawnVal = GUI.TextField(new Rect(10, 10, 60, 20), spawnVal, 25);
		tileVal = GUI.TextField(new Rect(10, 50, 60, 20), tileVal, 25);
		if(GUI.Button(new Rect(70, 10, 60, 20), "Apply"))
		{
			gpm.setNewSpawnValueForCurrentTile(spawnVal);
		}
		if(GUI.Button(new Rect(10, 30, 70, 20), "Save File"))
		{
			gpm.saveEnemyMapFile();
		}
	}

	public void setText(string st, string ht)
	{
		spawnVal = st;
		tileVal = ht;
	}
}