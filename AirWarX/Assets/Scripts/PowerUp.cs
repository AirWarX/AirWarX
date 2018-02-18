
using UnityEngine;

public class PowerUp : MonoBehaviour{

	public int powerUp;
	public float speed;
	public GameObject[] pics;
	private const int health = 0;
	private const int life = 1;
	private const int weapon1 = 2;
	private const int weapon2 = 3;
	private const int shield = 4;
	private const int missile = 5;
	private const int beam = 6;
	
	void Start (){
		float prob = Random.Range(0, 1f);
		if (prob < 0.25){
			powerUp = weapon1;
		} else if (prob < 0.5){
			powerUp = weapon2;
		} else if (prob < 0.6){
			powerUp = shield;
		} else if (prob < 0.7){
			powerUp = missile;
		} else if (prob < 0.8){
			powerUp = beam;
		} else if (prob < 0.98){
			powerUp = health;
		} else{
			powerUp = life;
		}

		pics[powerUp].SetActive(true);
	}
	
	void Update (){
		float angle = pics[powerUp].transform.localEulerAngles.y + Time.deltaTime * speed;
		pics[powerUp].transform.localEulerAngles = new Vector3(0, angle, 0);
		Vector3 pos = transform.position;
		transform.position = pos - new Vector3(0, 0, 5) * Time.deltaTime;
	}
}
