using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavour : MonoBehaviour {
	public int EnemyHealth = 20;
	public int MoveSpeed = 2;
    public float LookSpeed = 5f;
	public GameObject Player;
	public GameObject Asteroid;
    public GameObject Score;
	public int MaxDist = 10;
	public int MinDist = 4;
	
    // In the onenable function it just finds the player using the tag attached to them and resets their health so when they respawn they are reset.
	void OnEnable(){
        Player = GameObject.FindGameObjectWithTag("Player");
		EnemyHealth = 50;
	}

    /// 
    /// Here I was having trouble with making the enemy look at the player to be able to move properly so I used a 2D rotation tutorial since lookat doesn't work for 2D.
    /// (Rotate or Aim Towards Mouse or Object in 2D - Unity [ENG]), I did change it so it would work properly with what I wanted to do!
    /// 
    // What happens in the fixed update is that a variable stores the players location minus the enemies location every frame.
    // It then stores the players x and y location in radians and then converts them into degrees.
    // Then a variable stores a rotation based on the variable angle and the z axis.
    // Finally it then does a rotation based the current rotation of the enemy, the rotation quaternion and a variable called lookspeed (how fast the enemy turns) multiplied by the time from last frame and current frame.
    void FixedUpdate(){
        Vector2 PlayerPosition = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(PlayerPosition.x, PlayerPosition.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, LookSpeed * Time.deltaTime);

    // This variable just moves the enemy based on the current position and the players position and the distance between them.
        if (Vector2.Distance(transform.position, Player.transform.position)>= MinDist){
			transform.position += transform.up * MoveSpeed * Time.deltaTime;
		}
		
	}
	// This function is just misusing from the current enemy count on the asteroid script and then deactivating the gameobject.
	void EnemyDying(){
			print("Check for repeat");
			GetComponentInParent<ResetAsteroid>().CurrentEnemies -= 1;
			gameObject.SetActive(false);
	}
    // In the collision enter check if the enemy collides with a bullet it then minuses from the health, it then also deactivates the bullet and adds to the scoring variable.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            EnemyHealth -= 50;
            collision.gameObject.SetActive(false);
            Score = GameObject.Find("ScoringSystem");
            Score.GetComponent<Scoring>().NumberOfBugsKilled += 1;
     // This is performing the dying function when the enemy has no health.
            if (EnemyHealth <= 0)
            {
                EnemyDying();
            }
        }
    // Lastly if the enemy collides with the player it sets the player got hit variable to true if they haven't already been hit.
        else if (collision.gameObject.tag == "Player")
        {
            if (Player.GetComponent<CharController>().GotHit == false)
            {
                Player.GetComponent<CharController>().GotHit = true;
            }
        }
    }
}
