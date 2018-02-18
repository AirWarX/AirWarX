using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour {

	public int speed;
	
	void Update () {
		float pos = transform.position.z - speed * Time.deltaTime;
		if (pos > 1500) pos -= 1000;
		transform.position = new Vector3(-500, -50, pos);
	}
}
