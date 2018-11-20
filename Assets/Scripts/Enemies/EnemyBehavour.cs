using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavour : MonoBehaviour {
	public int EnemyHealth = 50;
	public int MoveSpeed = 2;
    public float LookSpeed = 5f;
	public GameObject Player;
	public GameObject Asteroid;
	public int MaxDist = 10;
	public int MinDist = 4;
	
	void OnEnable(){
        Player = GameObject.FindGameObjectWithTag("Player");
		EnemyHealth = 50;
	}

    /// 
    /// Here I was having trouble with making the enemy look at the player to be able to move properly so I used a 2D rotation tutorial since lookat doesn't work for 2D.
    /// (Rotate or Aim Towards Mouse or Object in 2D - Unity [ENG]), I did change it so it would work properly with what I wanted to do!
    /// 

    void FixedUpdate(){
        Vector2 PlayerPosition = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(PlayerPosition.x, PlayerPosition.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, LookSpeed * Time.deltaTime);


        if (Vector2.Distance(transform.position, Player.transform.position)>= MinDist){
			transform.position += transform.up * MoveSpeed * Time.deltaTime;
		}
		
	}
	
	void EnemyDying(){
			print("Check for repeat");
			GetComponentInParent<ResetAsteroid>().CurrentEnemies -= 1;
			gameObject.SetActive(false);
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Bullet"){
			EnemyHealth -= 100;
			collision.gameObject.SetActive(false);
			if (EnemyHealth <= 0){
			EnemyDying();
			}
		}
		else if (collision.gameObject.tag == "Player"){
			StartCoroutine(DamagePlayer());
		}
	}
	
	IEnumerator DamagePlayer(){
		Player.GetComponent<CharController>().Health -= 10;
		yield return new WaitForSeconds(2f);
	}
	
}
