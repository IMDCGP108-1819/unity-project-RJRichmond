using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAsteroid : MonoBehaviour {

    public GameObject AsteroidSpawner;
	public int AsteroidHealth = 10;
	public int CurrentEnemies = 0;
	public GameObject Bug;
	public Transform SpawnLocation; 
	public GameObject [] BugEnemies;
	public int AmountOfBugs = 6;
	public bool BugLeft = true;
	public int AmountOfBugsActive = 0;
	
	void OnEnable(){
		AsteroidHealth = 10;
	
	}
	
    void Start()
    {
        AsteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");
		SpawnLocation = this.gameObject.transform.GetChild(0);
		for (int i = 0; i < AmountOfBugs; i++){
			BugEnemies[i] = Instantiate(Bug);
			BugEnemies[i].transform.parent = gameObject.transform;
			BugEnemies[i].SetActive(false);
		}
    }
	
	private GameObject NewBug(){
		for (int i = 0; i < AmountOfBugs; i++){
			if (!BugEnemies[i].activeSelf){
				return BugEnemies[i];
			}
		}
		return null;
	}
	
	void FixedUpdate(){
		AsteroidDestroyed();
		if (CurrentEnemies < 6){
			CurrentEnemies += 1;
			EnemySpawn();
		}
	}
	public void AsteroidDestroyed(){
	
		if (AsteroidHealth < 0){
			Debug.Log("Asteroid has been destroyed!");
			//Add something here later on talking about adding points and getting the object to be able to haddle that!
			gameObject.SetActive(false);
			AsteroidSpawner.GetComponent<SpawnAsteroids>().AsteroidCount -= 1;
		}
	
	}
    //This function checks to see if an asteroid collides with a building it then deletes it and updates the asteroid variable in the spawner script.
    void OnCollisionEnter2D(Collision2D Collision)
    {
        AsteroidSpawner = GameObject.FindGameObjectWithTag("AsteroidSpawner");

        if (Collision.gameObject.tag == "Building")
        {
			AsteroidSpawner.GetComponent<SpawnAsteroids>().AsteroidCount -= 1;
            Debug.Log("Asteroid being deleted");
            gameObject.SetActive(false);
            
        }
		else if (Collision.gameObject.tag == "Bullet"){
			Debug.Log("Asteroid is being shot!");
			AsteroidHealth -= 1;
			//Collision.SetActive(false);
		}
    }
	
	void EnemySpawn(){
		GameObject ABug = NewBug();
		if (ABug != null){
			ABug.transform.SetPositionAndRotation(SpawnLocation.position, SpawnLocation.rotation);
			ABug.SetActive(true);
			AmountOfBugsActive += 1;
		}
		//Instantiate(Bug);
		//Bug.transform.SetPositionAndRotation(SpawnLocation.position, SpawnLocation.rotation);
	}
}
