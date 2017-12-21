using UnityEngine;
using System.Collections;

public class BlinkBossLocation : MonoBehaviour {

	SpriteRenderer sprRend;

	int prevIdx = 0;

	void Awake(){
		sprRend = (GetComponent<Renderer>() as SpriteRenderer);
	}

	void Update () {
		int idx = (int)(Time.timeSinceLevelLoad * 3);

		if(prevIdx != idx && idx % 2 == 0){
			sprRend.enabled = !sprRend.enabled;
		}

		prevIdx = idx;
	}
}
