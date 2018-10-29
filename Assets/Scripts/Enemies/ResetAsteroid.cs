using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAsteroid : MonoBehaviour {

    public GameObject AsteroidSpawner;

    void Start()
    {
        AsteroidSpawner = GameObject.Find("AsteroidSpawner");
    }

    //This function checks to see if an asteroid collides with a building it then deletes it and updates the asteroid variable in the spawner script.
    void OnCollisionEnter2D(Collision2D Collision)
    {
        AsteroidSpawner = GameObject.Find("AsteroidSpawner");
        if (Collision.gameObject.tag == "Building")
        {
            Debug.Log("Asteroid being deleted");
            Destroy(gameObject);
            AsteroidSpawner.GetComponent<SpawnAsteroids>().AsteroidCount -= 1;
        }
    }
}
