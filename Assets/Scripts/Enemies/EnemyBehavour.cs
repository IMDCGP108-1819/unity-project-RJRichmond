using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavour : MonoBehaviour {
	public int EnemyHealth = 5;
	public int MoveSpeed = 2;
    public float LookSpeed = 5f;
	public Transform Player;
	public GameObject Asteroid;
	public int MaxDist = 10;
	public int MinDist = 4;
	
	void Start(){
        Player = GameObject.FindGameObjectWithTag("Player").transform;
	}

    /// 
    /// Here I was having trouble with making the enemy look at the player to be able to move properly so I used a 2D rotation tutorial since lookat doesn't work for 2D.
    /// (Rotate or Aim Towards Mouse or Object in 2D - Unity [ENG]), I did change it slightly so it would work properly!
    /// 

    void FixedUpdate(){
        Vector2 PlayerPosition = Player.position - transform.position;
        float angle = Mathf.Atan2(PlayerPosition.x, PlayerPosition.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, LookSpeed * Time.deltaTime);


        if (Vector2.Distance(transform.position, Player.position)>= MinDist){
			transform.position += transform.up * MoveSpeed * Time.deltaTime;
		}
		EnemyDying();
	}
	
	void EnemyDying(){
		if (EnemyHealth < 0){
			Destroy(gameObject);
			GetComponentInParent<ResetAsteroid>().CurrentEnemies -= 1;
		}
	}
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Bullet"){
			EnemyHealth -= 1;
			Destroy(collision.gameObject);
		}
	}
}
