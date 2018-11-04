using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public int PlayerSpeed = 0;
    public int Health = 100;
    public GameObject Bullet;
    public float FireRate = 0.2f;
    public bool FireAble = true;
    public Transform FiringPosition;

    void FixedUpdate() {
        movement();
        Shooting();
    }

    public void Shooting() {
        if (Input.GetKey("p") && FireAble) {
            Debug.Log("Shooting happens");
            StartCoroutine(Firing());
        }
    }

    //This void is used for movement of the player, it is used to perform when a movement key is pressed to move in the direction the player is trying to go.
    public void movement() {
       if (Input.GetKey("d"))
       {
            transform.position += new Vector3(1, 0, 0) * PlayerSpeed * Time.deltaTime;
       }

       if (Input.GetKey("a"))
       {
            transform.position += new Vector3(-1, 0, 0) * PlayerSpeed * Time.deltaTime;
       }

       if (Input.GetKey("w"))
       {
            transform.position += new Vector3(0, 1, 0) * PlayerSpeed * Time.deltaTime;
       }

       if (Input.GetKey("s"))
       {
            transform.position += new Vector3(0, -1, 0) * PlayerSpeed * Time.deltaTime;
       }
   }

    IEnumerator Firing() {
        FireAble = false;
        Instantiate(Bullet);
        Bullet.transform.SetPositionAndRotation(FiringPosition.position, FiringPosition.rotation);
        yield return new WaitForSeconds(FireRate);
        FireAble = true;
   } 
   
}
