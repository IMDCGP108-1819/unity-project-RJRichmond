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
        AsteroidHealth = 10;

    }
    // In the start function I am getting all of the game objects/transform which I need and spawning the objects for the list of bugs.

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

    void FixedUpdate()
    {
        AsteroidDestroyed();
        if (CurrentEnemies < 6)
        {
            CurrentEnemies += 1;
			StartCoroutine(WaitForSpawn());
            EnemySpawn();
        }
    }
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
    /// it then acts accordingly, for example if it colliders with a building when spawned it will deactive it and then spawn it again.
    void OnCollisionEnter2D(Collision2D Collision)
    {
        AsteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");

        if (Collision.gameObject.tag == "Building")
        {
            AsteroidSpawner.GetComponent<SpawnAsteroids>().AsteroidCount -= 1;
            Debug.Log("Asteroid being deleted");
            gameObject.SetActive(false);

        }
        else if (Collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Asteroid is being shot!");
            Collision.gameObject.SetActive(false);
            AsteroidHealth -= 1;

        }
    }

    void EnemySpawn()
    {
        GameObject ABug = NewBug();
        if (ABug != null)
        {
            ABug.transform.SetPositionAndRotation(SpawnLocation.position, SpawnLocation.rotation);
            ABug.SetActive(true);
			StartCoroutine(WaitForSpawn());
            AmountOfBugsActive += 1;
        }
    }
	private IEnumerator WaitForSpawn(){
		yield return new WaitForSecondsRealtime(SpawnDelay);
	}
}