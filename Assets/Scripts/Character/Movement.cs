using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public int PlayerSpeed = 0;



	

    void FixedUpdate() {
        movement();
    }



    //This void is used for movement of the player, it is used to perform when a movement key is pressed to move in the direction the player is trying to go.
     public void movement() {
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(1, 0, 0) * PlayerSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            Debug.Log("This should work");
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

   
}
