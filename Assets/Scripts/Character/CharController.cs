using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharController : MonoBehaviour {

    public int PlayerSpeed = 2;
    public int Health = 100;
    public int AmountOfBulletsActive = 0;
    public int AllBullets = 8;
    public int MaxHealth = 100;

    public float FireRate = 0.2f;

    private Transform FiringPosition;           // Stores the position of an object (in this case where the bullet should spawn)

	public GameObject [] PlayerBullets;         // List of bullets 
    public GameObject Bullet;
    public GameObject HealthObject;
    public GameObject scoreSystem;

    public Slider HealthBar;                    // This stores a UI element which is used to control the health bar slider.

    public bool FireAble = true;
    public bool BulletsLeft = true;
    public bool GotHit = false;
    public bool iFrames = false;

    // In the start function it uses a for loop to create all of the bullet objects and sets them to false ready to be fired, 
    // the other thing it does in the start function is it gets the health bar object and then gets the slider component so it can be updated later on.
    void Start(){
		for (int i = 0; i < AllBullets; i++)
		{
			PlayerBullets[i] = Instantiate(Bullet);
			PlayerBullets[i].SetActive(false);
		}
		HealthObject = GameObject.FindGameObjectWithTag("HealthBar");
		HealthBar = HealthObject.GetComponent<Slider>();
	}
	// This is where it finds a deactivated bullet and makes the variable "NewBullet" equal to the object so it can be used to fire.
	private GameObject NewBullet(){
		for (int i = 0; i < AllBullets; i++){
			if (!PlayerBullets[i].activeSelf){
				return PlayerBullets[i];
			} 
		}
		return null;
	}
	// In the fixed update variable it gets the firing position (position of the gun) and it performs all of the basic functions such as movement,shooting and checking and
    // if the player dies. The last thing is setting the slider value for the health bar equal to the players current health.
    void FixedUpdate() {
        FiringPosition = GameObject.FindGameObjectWithTag("FiringPosition").transform; // Finding the gameobject which has the firing position tag and then getting its transform (locaiton).
        movement();
        Shooting();
        LevelRestart();
		HealthBar.value = Health;
    }
    // In the shooting function it checks to see if the player has fired and then to see if he fire again, if both are true then it starts the Coroutine.
    public void Shooting() {
        if (Input.GetButton("Fire1") && FireAble) {
            
			Debug.Log("Shooting happens");
            StartCoroutine(Firing());
			
        }
    }

    //This function is used for movement of the player, it is used to perform when a movement key is pressed to move in the direction the player is trying to go.
    public void movement() {
       if (Input.GetKey("d"))
       {
            transform.position += new Vector3(1, 0, 0) * PlayerSpeed * Time.deltaTime;
       }

       if (Input.GetKey("a"))
       {
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

    //The firing IEnumerator sets the fire check to false, gets the variable which has the unactive bullet stored and then it sets its position and rotation based on the firing position,
    //it then sets the bullet active to true which would give it the force when its activated, the last thing it does is add 1 to the active bullet variable to keep track of active bullets.
    IEnumerator Firing() {
        FireAble = false;
		GameObject ABullet = NewBullet();
		if (ABullet != null){ // If the stored bullet isn' equal to none.
            ABullet.transform.SetPositionAndRotation(FiringPosition.position, FiringPosition.rotation); // Double checking position is correct so that the bullet is being fired from the right place.
            ABullet.SetActive(true);
			ABullet.transform.SetPositionAndRotation(FiringPosition.position, FiringPosition.rotation);
			AmountOfBulletsActive += 1;
		}
        yield return new WaitForSeconds(FireRate); // Wait the firing rate delay
        FireAble = true;
   }
    // Level restart function is pretty simple, it checks if the players health is less then 0 and if it is the game ends.
    // It also gets the scoring system and makes a boolean equal to true so that it calculates the total score and also stops the time from recording by setting that boolean to false.
    void LevelRestart()
    {
        if (Health < 0)
        {
            scoreSystem = GameObject.Find("ScoringSystem"); // Finding the scoring system variable.
            scoreSystem.GetComponent<Scoring>().RecordScore = true;
            scoreSystem.GetComponent<Scoring>().ShouldRecord = false;
            SceneManager.LoadScene(0); // Sending back to the main menu
        }

    }
    // The invincibility IEnumerator is used to give the player Iframes (a period where they cant be damaged) so it sets iframes to true, minuses from the players health
    // and after 2 seconds resets everything, being the "GotHit" check (checking if the player was hit) and "iframes" to false.
    IEnumerator Invincibility() {
        Health -= 10;
        iFrames = true;
        yield return new WaitForSeconds(2f);
        GotHit = false;
        iFrames = false;
    }
    // This collision checker variable is what I use to apply damage to the player, if the player collides with something and the tag on the gameobject is "bug" (enemies),
    // then it goes through the process of checking if the player has Iframes and if they did get hit then runs the invincibility IEnumerator
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bug")
        {
            if (iFrames == false)
            {
                if (GotHit == true)
                {
                    StartCoroutine(Invincibility());
                }
            }
        }
    }
}
