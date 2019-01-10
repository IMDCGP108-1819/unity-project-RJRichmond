using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour {

    public Vector2 center; // These variables are used to set the area in which the asteroids can randomly spawn. Making them equal to the area which I want.
    public Vector2 size;

    public int AsteroidCount;
    public int AmountOfAsteroidsActive = 0;
    public int AllAsteroids = 5;

    public GameObject Asteroid;
    public GameObject [] Asteroids;

	public bool AsteroidAvaliable = true;
	
	
    // This is the same as the bullet and bug spawning, essentiually just instaintates a number of asteroids based on the max number (all asteroids) and sets them to false.
	void Start(){
		for(int i = 0; i < AllAsteroids; i++){
			Asteroids[i] = Instantiate(Asteroid);
			Asteroids[i].SetActive(false);
		}
	}
	// Returns a gameobject which isn't current active by going through the list until one is found.
	private GameObject NewAsteroid(){
		for (int i = 0; i < AllAsteroids; i++){
			if (!Asteroids[i].activeSelf){
				return Asteroids[i];
			}
		}
		return null; // If all gameobjects are active nothing is returned.
	}
	// In the fixed update it checks to see if there is currently 5 asteroids active and if not it adds to it and runs the random asteroid function.
    public void FixedUpdate() {
        if (AsteroidCount <= 5) {
            AsteroidCount += 1;
            RandomAsteroids();
        }

    }
    //This is the function which chooses a random location based on the variables of size and center, it then makes an asteroid active and places it some where on the map based on the vectors.
    public void RandomAsteroids()
    {
        // This is setting a vector based on the center variable which we set and a new vector which is created using random range between the size vector divided by 2 for both the x and y axis.
        // This then gives a random position on the map which we can use to spawn an asteroid.
        // For this I used a tutorial for this (How to spawn objects at random position in a given area) since I didn't really know about random locations.
        Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2)); 
		GameObject AnAsteroid = NewAsteroid();
		if (AnAsteroid != null){
			AnAsteroid.transform.SetPositionAndRotation(pos,Quaternion.identity);
			AnAsteroid.SetActive(true);
		}
    }

    //This is something which is only avaliable in the editor and I used it to show the vectors which I was setting for the random spawning.
    //It basically visualises the vector so I can set them appropriately.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f); // Sets the colour of the visualisation. 1,0,0,0.5f is red 
        Gizmos.DrawCube(center, size); // This is what allows the visualisation to happen since it draws a cube based on the vectors.
    }

}
