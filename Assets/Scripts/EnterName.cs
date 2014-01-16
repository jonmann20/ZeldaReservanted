using UnityEngine;
using System.Collections;

public class EnterName : MonoBehaviour {

	public float speed;

	void Update () {
		if(Input.GetButtonDown("Left") && transform.position.x > -97){
			transform.position += new Vector3(-speed, 0);
		}

		if(Input.GetButtonDown("Right") && transform.position.x < 92){
			transform.position += new Vector3(speed, 0);
		}

		if(Input.GetButtonDown("Up") && transform.position.y < 12){
			transform.position += new Vector3(0, speed);
		}

		if(Input.GetButtonDown("Down") && transform.position.y > -43){
			transform.position += new Vector3(0, -speed);
		}

	}
}
