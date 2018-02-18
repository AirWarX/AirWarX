
using UnityEngine;

public class PlayGround : MonoBehaviour {
	void OnTriggerExit(Collider other){
		Destroy(other.gameObject);
	}
}
