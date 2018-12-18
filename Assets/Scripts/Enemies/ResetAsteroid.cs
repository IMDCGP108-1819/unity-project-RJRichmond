using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAsteroid : MonoBehaviour
{

    public GameObject AsteroidSpawner;
    public int AsteroidHealth = 10;
    public int CurrentEnemies = 0;
    public GameObject Bug;
    public GameObject Score;
    public Transform SpawnLocation;
    public GameObject[] BugEnemies;
    public int AmountOfBugs = 6;
    public bool BugLeft = true;
    public int AmountOfBugsActive = 0;
	private float SpawnDelay = 2f;

    void OnEnable()
    {
        AsteroidHealth = 10;                        // Reseting the asteroid health when spawning a new asteroid

    }
    // In the start function I am getting all of the game objects/transform which I need and spawning the objects for the list of bugs.
    // This is the same method as how I spawn the asteroid and bullets which is using a list of gameobjects and activating when they are needed.
    void Start()
    {
        AsteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");
        SpawnLocation = this.gameObject.transform.GetChild(0);
        for (int i = 0; i < AmountOfBugs; i++)
        {
            BugEnemies[i] = Instantiate(Bug);
            BugEnemies[i].transform.parent = gameObject.transform;
            BugEnemies[i].SetActive(false);
        }

    }
    // This is storing the first non active gameobject from the list in the variable.
    private GameObject NewBug()
    {
        for (int i = 0; i < AmountOfBugs; i++)
        {
            if (!BugEnemies[i].activeSelf)
            {
                return BugEnemies[i];
            }
        }
        return null;
    }
    // In the fixed update it is checking to see if the asteroid with this script is active and if it isn't then it performs the asteroid destroyed function (health check),
    // and it checks to see if the max number of enemies are currently spawned from the asteroid being 6 and if they aren't performing the enemyspawn function (setting an enemy to active.)
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            AsteroidDestroyed();
            if (CurrentEnemies <= 6)
            {
                CurrentEnemies += 1;
                EnemySpawn();
                
            }

        }
    }
    // In the asteroid destroyed function it is just a basic health check, seeing if the asteroids current health is below 0 if it is,
    // then it finds the scoring gameobject and gets the script to add to the number of asteroids destroyed variable, finally it sets the object to deactivated and finds,
    // asteroid spawner script and minuses from the asteroid count variable.
    public void AsteroidDestroyed()
    {

        if (AsteroidHealth < 0)
        {
            Debug.Log("Asteroid has been destroyed!");
            Score = GameObject.Find("ScoringSystem");
            Score.GetComponent<Scoring>().NumberOfAsteroidsKilled += 1;
            gameObject.SetActive(false);
            AsteroidSpawner.GetComponent<SpawnAsteroids>().AsteroidCount -= 1;
        }

    }
    ///
    /// This function checks to see if anything is colliding with the asteroid and based on what is actually colliding with it,
    /// it then acts accordingly, for example if it collides with a building when spawned it will deactivate it and then spawn it again.
    void OnCollisionEnter2D(Collision2D Collision)
    {
        AsteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");

        if (Collision.gameObject.tag == "Building")
        {
            AsteroidSpawner.GetComponent<SpawnAsteroids>().AsteroidCount -= 1;
            Debug.Log("Asteroid being deleted");
            gameObject.SetActive(false);

        }
        // If the asteroid collides with a gameobject with the tag bullet and then minuses from the asteroids health and sets the object which collided with it to deactivated.
        else if (Collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Asteroid is being shot!");
            Collision.gameObject.SetActive(false);
            AsteroidHealth -= 1;
        }
    }
    // The enemy spawn function is running a coroutine which is waiting a spawn delay before then getting the deactivated bug gameobject from the list and then 
    // it does another check to make sure that a gameobject is in the variable and then it sets the position and location based of the asteroids enemy spawn location.
    // lastly adding to the current number of active bugs.
    void EnemySpawn()
    {
        StartCoroutine(WaitForSpawn());
    }
    private IEnumerator WaitForSpawn()
    {
        yield return new WaitForSecondsRealtime(SpawnDelay);
        GameObject ABug = NewBug();
        if (ABug != null)
        {
            ABug.transform.SetPositionAndRotation(SpawnLocation.position, SpawnLocation.rotation);
            ABug.SetActive(true);
            AmountOfBugsActive += 1;
        }
    }
}