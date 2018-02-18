using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	public GameObject ending;
	public GameObject explosion;
	
	void Start () {
		Global.player1 = Instantiate(player1, new Vector3(-10, 0, 5), Quaternion.identity);
		Global.player2 = Instantiate(player2, new Vector3( 10, 0, 5), Quaternion.identity);
		Global.player1Life = 3;
		Global.player2Life = 3;
		Global.explosion = explosion;
	}
	
	void Update () {
		deathCheck();
	}

	private void deathCheck(){
		if (Global.player1.GetComponent<Player>().hp <= 0){
			Global.player1Life--;
			Destroy(Global.player1);
			if (Global.player1Life > 0){
				Global.player1 = Instantiate(player1, new Vector3(-10, 0, 5), Quaternion.identity);
			}
		}

		if (Global.player2.GetComponent<Player>().hp <= 0){
			Global.player2Life--;
			Destroy(Global.player2);
			if (Global.player2Life > 0){
				Global.player2 = Instantiate(player2, new Vector3(10, 0, 5), Quaternion.identity);
			}
		}

		if (Global.player1Life <= 0 && Global.player2){
			ending.SetActive(true);
		}
	}
}
