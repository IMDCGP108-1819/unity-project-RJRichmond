using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public int PlayerSpeed = 0;



	

    void FixedUpdate() {
        movement();
        MouseFollow();
    }



    //This void is used for movement of the player, it is used to perform when a movement key is pressed to move in the direction the player is trying to go.
    void movement() {
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

    //This function is used to make the character follow the mouses position by updating and then changing the position of the player accordingly.
    void MouseFollow() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 mouseDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = mouseDirection;
    }
}
