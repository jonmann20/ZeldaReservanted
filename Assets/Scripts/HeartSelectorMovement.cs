using UnityEngine;
using System.Collections;

public class HeartSelectorMovement : MonoBehaviour {

	public float y;

	void Update () {

		if(Input.GetKeyDown(KeyCode.W)){
			transform.Translate(new Vector3(0, y, 0));
		}


		if(Input.GetKeyDown(KeyCode.S)){
			transform.Translate(new Vector3(0, -y, 0));
		}
	}
}
