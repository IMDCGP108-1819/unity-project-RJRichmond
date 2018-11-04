using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float BulletLife = 2f;
    public float StartTime;
    public float Force;
    public Rigidbody2D rigid;

    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.up * Force);
        StartTime = Time.time;
    }

    void FixedUpdate() {
        if (Time.time >= StartTime + BulletLife) {
            Destroy(gameObject);
        }
    }
	
    
}
