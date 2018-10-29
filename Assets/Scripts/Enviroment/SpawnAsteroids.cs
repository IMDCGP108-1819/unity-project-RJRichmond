using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour {

    public Vector2 center;
    public Vector2 size;
    public GameObject Asteroid;
    public int AsteroidCount;

    public void FixedUpdate() {
        if (AsteroidCount < 5) {
            AsteroidCount += 1;
            RandomAsteroids();
        }

    }
    //This is the function which chooses a random location based on the variables of size and center, it then spawns a prefab being the asteroid.
    public void RandomAsteroids()
    {
        Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.x / 2, size.x / 2));
        Instantiate(Asteroid, pos, Quaternion.identity);
    }

    //This is something which is only avaliable in the editor and it is used to show location of the size and center of the area.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

}
