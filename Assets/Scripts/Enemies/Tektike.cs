using UnityEngine;
using System.Collections;

public class Tektike : Enemy {

	int moveRate = 3;
	float nextMove = 0;

	public override void Movement(){
		transform.position = pickRandom();
	}

	Vector3 pickRandom(){
		return new Vector3(Random.Range(-8,8), Random.Range(-5.5f,5.5f));
	}

	void Update(){
		if(Time.time > nextMove){
			nextMove = Time.time + moveRate;
			Movement();
		}
	}
}
