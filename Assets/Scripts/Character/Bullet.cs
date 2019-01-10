using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float BulletLife = 2f;
    public float StartTime;
    public float Force;

    public Rigidbody2D rigid;                       //Stores rigidbody
	public GameObject Player;                       //Stores player object

	// The on enable function is used to give the bullet momentum by making it go in a direction based on the force applied.
    // It then makes a variable equal to the time which will be used for deactivaing the bullet after its been fired. The last thing is it gets gameobject which is tagged with player.
    void OnEnable() {
        rigid = GetComponent<Rigidbody2D>();                // Getting rigidbody from object
        rigid.AddForce(transform.up * Force);               // Adding force to the rigidbody to make the bullet have momentum.
        StartTime = Time.time;
		Player = GameObject.FindGameObjectWithTag("Player");
    }
    // After the bullet has been active for more then 2 seconds (bullet life) it then deactivates the bullet and subtracts 1 from the variable which keeps track of active bullets.
    void FixedUpdate() {
        if (Time.time >= StartTime + BulletLife) {
            this.gameObject.SetActive(false);
			Player.GetComponent<CharController>().AmountOfBulletsActive -= 1;
        }
    }
	
    
}
