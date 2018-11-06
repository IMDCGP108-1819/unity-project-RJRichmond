using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavour : MonoBehaviour {
	public int EnemyHealth = 5;
	public int MoveSpeed = 2;
	public Transform Player;
	public GameObject Asteroid;
	public int MaxDist = 10;
	public int MinDist = 4;
	
	void Start(){
		//Asteroid = this.gameObject.transform.parent;
	}
	
	void FixedUpdate(){
		transform.LookAt(Player);
		if (Vector2.Distance(transform.position, Player.position)>= MinDist){
			transform.position += transform.forward * MoveSpeed * Time.deltaTime;
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
