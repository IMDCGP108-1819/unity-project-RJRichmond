using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float BulletLife = 2f;
    public float StartTime;
    public float Force;
    public Rigidbody2D rigid;
	public GameObject Player;
	
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.up * Force);
        StartTime = Time.time;
		Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate() {
        if (Time.time >= StartTime + BulletLife) {
            this.gameObject.SetActive(false);
			Player.GetComponent<CharController>().AmountOfBulletsActive -= 1;
        }
    }
	
    
}
