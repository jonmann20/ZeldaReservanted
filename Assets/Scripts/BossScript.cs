using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {
	
	float sinCounter = 0;
	const int FLAME_FREQUENCY = 60;
	int flameTimer;
	//0 = intro
	//1 = vulnerable, move up and down

	GameObject FireProjectile;
	int state = 0;

	// Use this for initialization
	void Start () {
		FireProjectile = Resources.Load("Enemies/FireProjectile") as GameObject;
		transform.position = new Vector3(25, -8f, 0);
		flameTimer =  FLAME_FREQUENCY + Random.Range(0, FLAME_FREQUENCY);
	}
	
	// Update is called once per frame
	void Update () {

		if(state == 0)
		{
			sinCounter += 0.5f;
			float xPos = transform.position.x - 0.02f;
			float yPos = -8 + Mathf.Sin(sinCounter);

			transform.position = new Vector3(xPos, yPos, 0);

			if(transform.position.x < 15)
				state = 1;
			return;
		}

		if(state == 1)
		{
			flameTimer --;
			if(flameTimer <= 0)
			{
				flameTimer = FLAME_FREQUENCY + Random.Range(0, FLAME_FREQUENCY);
				//Vector3 pos = new Vector3(transform.position.x + );
				GameObject go = Instantiate(FireProjectile, transform.position, Quaternion.identity) as GameObject;
			}

		}
	}
}
