using UnityEngine;
using System.Collections;

public class HeartSelectorMovement : MonoBehaviour {

	public float dtY;
	bool onReg = true;

	void Update () {
		if(Input.GetButtonDown("Select")){
			transform.Translate(new Vector3(0, onReg ? -dtY : dtY, 0));
			onReg = !onReg;
		}
	}
}
