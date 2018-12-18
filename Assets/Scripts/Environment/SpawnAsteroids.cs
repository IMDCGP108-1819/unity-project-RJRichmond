using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour {

    public Vector2 center;
    public Vector2 size;
    public GameObject Asteroid;
    public int AsteroidCount;
	public GameObject [] Asteroids;
	public bool AsteroidAvaliable = true;
	public int AmountOfAsteroidsActive = 0;
	public int AllAsteroids =5;
	
    // This is the same as the bullet and bug spawning, essentiually just instaintates a number of asteroids based on the max number (all asteroids) and sets them to false.
	void Start(){
		for(int i = 0; i < AllAsteroids; i++){
			Asteroids[i] = Instantiate(Asteroid);
			Asteroids[i].SetActive(false);
		}
	}
	// stores a gameobject which isn't currently active.
	private GameObject NewAsteroid(){
		for (int i = 0; i < AllAsteroids; i++){
			if (!Asteroids[i].activeSelf){
				return Asteroids[i];
			}
		}
		return null;
	}
	// In the fixed update it checks to see if there is currently 5 asteroids active and if not it adds to it and runs the random asteroid function.
    public void FixedUpdate() {
        if (AsteroidCount <= 5) {
            AsteroidCount += 1;
            RandomAsteroids();
        }

    }
    //This is the function which chooses a random location based on the variables of size and center, it then spawns a prefab being the asteroid.
    public void RandomAsteroids()
    {
		Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2));
		GameObject AnAsteroid = NewAsteroid();
		if (AnAsteroid != null){
			AnAsteroid.transform.SetPositionAndRotation(pos,Quaternion.identity);
			AnAsteroid.SetActive(true);
		}
    }

    //This is something which is only avaliable in the editor and it is used to show location of the size and center of the area.
    //so I could set the overall spawn areas for the asteroids.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

}
