using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

	public Player player1;
	public Player player2;
	
	void Start ()
	{
		Instantiate(player1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
