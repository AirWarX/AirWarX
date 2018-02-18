using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Player : MonoBehaviour {
	
	public bool isPLayer1;
	public int hp = 5;
	public GameObject fireLocation;
	public GameObject bullet;
	public float speed;
	public float tiltSpeed;
	public float acceleration;

	private float[] rate = {2, 0, 0, 0, 0}; //weapon 1 2 rate. skill 1 2 3 num
	private float[] cd = {0, 0.2f, 0.2f, 0.2f, 0.2f}; //weapon 1 2 cd.   skill 1 2 3 on
	private int vertical = 1;
	private float verticalC = 0;
	private int horizontal = 0;
	private float horizontalC = 0;
	private bool begin = true;

	private const int fireRateMax = 10;
	private string[] control1 = {"up", "down", "left", "right", "z", "x", "c"};
//	private string[] control1 = {"up", "down", "left", "right", ",", ".", "/"};
	private string[] control2 = {"f", "r", "g", "d", "a", "s", "g"};
//	private string[] control2 = {"w", "s", "a", "d", "z", "x", "c"};
	
	void Update () {
		if (begin){
			if (transform.position.z > 2){
				begin = false;
				GetComponent<SphereCollider>().enabled = true;
			}
		} else {
			inputCheck();
			posCheck();
			cdUpdate();
			autoFire();
		}
		move();
	}

	private void posCheck(){
		if (transform.position.z < 2){
			vertical = 1;
		} else if (transform.position.z > 50){
			vertical = -1;
		}

		if (transform.position.x < -18 - transform.position.z * 0.35f){
			horizontal = 1;
		} else if (transform.position.x > 18 + transform.position.z * 0.35f){
			horizontal = -1;
		}
	}

	private void move(){
		
		Vector3 pos = transform.position;
		horizontalC = Mathf.MoveTowards(horizontalC, horizontal * speed, Time.deltaTime * acceleration);
		verticalC = Mathf.MoveTowards(verticalC, vertical * speed, Time.deltaTime * acceleration);
		
		transform.position = new Vector3(pos.x + horizontalC * Time.deltaTime, 
			0, pos.z + verticalC * Time.deltaTime);

		Vector3 angle = transform.eulerAngles;
		
		transform.eulerAngles = 
			new Vector3(Mathf.MoveTowardsAngle(angle.x, 30 * vertical, Time.deltaTime * tiltSpeed),
				0, Mathf.MoveTowardsAngle(angle.z, -30 * horizontal, Time.deltaTime * tiltSpeed));
	}

	private void inputCheck() {
		
		string[] control;
		control = isPLayer1 ? control1 : control2;

		if (Input.GetKey(control[0])){
			vertical = 1;
		} else if (Input.GetKey(control[1])){
			vertical = -1;
		} else{
			vertical = 0;
		}
		
		if (Input.GetKey(control[2])){
			horizontal = -1;
		} else if (Input.GetKey(control[3])){
			horizontal = 1;
		} else{
			horizontal = 0;
		}

		if (cd[2] <= 0 && Input.GetKey(control[4])){
			fire(1);
		}
		
		if (cd[3] <= 0 && Input.GetKey(control[5])){
			fire(2);
		}
		
		if (cd[4] <= 0 && Input.GetKey(control[6])){
			fire(3);
		}
	}

	private void cdUpdate() {
		if (cd[0] > 0) cd[0] -= Time.deltaTime * rate[0];
		if (cd[1] > 0) cd[1] -= Time.deltaTime * rate[1];
		for (int i = 2; i < 5; i++) {
			if (cd[i] > 0) cd[i] -= Time.deltaTime;
		}
	}

	private void fire(int skill) {
		//todo
	}

	private void autoFire() {
		if (cd[0] <= 0) {
			Instantiate(bullet, fireLocation.transform.position, Quaternion.identity);
			cd[0] += 1;
		}

		if (cd[1] <= 0) {
			print(cd[1]);
			GameObject shot;
			shot = Instantiate(bullet, fireLocation.transform.position, Quaternion.identity);
			shot.transform.eulerAngles = new Vector3(0, 15, 0);
			shot = Instantiate(bullet, fireLocation.transform.position, Quaternion.identity);
			shot.transform.eulerAngles = new Vector3(0, -15, 0);
			shot = Instantiate(bullet, fireLocation.transform.position, Quaternion.identity);
			shot.transform.eulerAngles = new Vector3(0, 30, 0);
			shot = Instantiate(bullet, fireLocation.transform.position, Quaternion.identity);
			shot.transform.eulerAngles = new Vector3(0, -30, 0);
			cd[1] += 1;
		}
	}

	private void powerUp(int index){
		switch (index){
			case 0: hp += 2;
				break;
			case 1:
				if (isPLayer1){
					Global.player1Life++;
				} else{
					Global.player2Life++;
				}
				break;
			case 2:
				if (rate[0] < fireRateMax) rate[0] = rate[0] * 1.5f;
				break;
			case 3:
				if (rate[1] < fireRateMax) rate[1] = rate[1] * 1.5f;
				break;
			case 4:
				if (rate[2] < 5) rate[2]++;
				break;
			case 5:
				if (rate[3] < 5) rate[3]++;
				break;
			case 6:
				if (rate[4] < 5) rate[4]++;
				break;
		}
	}

	private void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag("EBullet")) {
			hp -= other.GetComponent<Bullet>().damage;
		} else if (other.CompareTag("Plane")) {
			hp = 0;
		}

		if (other.CompareTag("PowerUp")){
			powerUp(other.GetComponent<PowerUp>().powerUp);
			Destroy(other.gameObject);
		}
	}
}
