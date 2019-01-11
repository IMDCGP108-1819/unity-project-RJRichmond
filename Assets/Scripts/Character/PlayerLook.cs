using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    void FixedUpdate() {
        MouseFollow();          // Run the mouse follow function
    }

    //This function is used to make the character follow the mouses position by updating and then changing the rotation of the player accordingly,
    //so that the player is facing the correct way for firing the weapon.
    // For this section I used the unity camera screen to world point documentation as it helped me greatly in getting this to work!
    void MouseFollow()
    {
        Vector3 mousePosition = Input.mousePosition;        // Stores the current mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);  // Makes the mouse position variable equal to the mouse position using the main camera 

        Vector2 mouseDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        // It then stores a new 2D vector based on the mouse poisition minus the current position on both the x and y.
        transform.up = mouseDirection; // Sets transform.up to the mouse direction vector.
    }

}
