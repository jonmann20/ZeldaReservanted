using UnityEngine;
using System.Collections;

public class SpecialCollisionTileScript : MonoBehaviour {

	public string code = "00";
	public GameplayMain gpm;

	//Don't activate if Link spawns on tile.
	int awakeTimer = 5;
	bool onTileSafely = false;

	// SCREEN SCROLL
	Vector2 desiredDisplacement, deltaDisplacement;
	float desiredDisplacementTime, vert, hor;
	
	void Start () {
		SpriteRenderer sr = this.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer;
		Color c = sr.color;
		c.a = 0.05f;
		sr.color = c;

		GameObject go = GameObject.FindWithTag("MainCamera") as GameObject;
		gpm = go.GetComponent("GameplayMain") as GameplayMain;
	}

	void Update () {
		awakeTimer --;

		// SCREEN SCROLL
		if(desiredDisplacementTime > 0){
			transform.Translate(deltaDisplacement);
			--desiredDisplacementTime;
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if(awakeTimer > 0) onTileSafely = true;

		if(!onTileSafely && coll.gameObject.tag == "Player")
		{
			gpm.collideWithSpecialCode(code);
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if(onTileSafely) onTileSafely = false;
	}

	//First two elements are X and Y displacement.
	//Third element is number of frames (time).
	public void setDesiredDisplacementTime(Vector3 v)
	{
		desiredDisplacement = new Vector2(v.x, v.y);
		desiredDisplacementTime = v.z;
		deltaDisplacement = desiredDisplacement / (desiredDisplacementTime);
	}
}
