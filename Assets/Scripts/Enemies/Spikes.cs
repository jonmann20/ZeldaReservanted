using UnityEngine;
using System.Collections;

public class Spikes : Enemy {

	Vector3 initialPosition;
	const float chargeSpeed = 5;
	const float reelSpeed = chargeSpeed * 0.5f;

	//0 = idle, 1 = charging, 2 = reeling
	int state = 0;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Movement(){

	}
}
