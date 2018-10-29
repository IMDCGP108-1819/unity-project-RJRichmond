using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    void FixedUpdate() {
        MouseFollow();
    }

    //This function is used to make the character follow the mouses position by updating and then changing the position of the player accordingly.
    void MouseFollow()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 mouseDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = mouseDirection;
    }

}
