
using UnityEngine;

public class Bullet : MonoBehaviour{
    
    public int damage;
    public float speed;

    void Update(){
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }
}