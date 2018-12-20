using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharController : MonoBehaviour {

    public int PlayerSpeed = 2;
    public int Health = 100;
    public GameObject Bullet;
    public float FireRate = 0.2f;
    public bool FireAble = true;
    private Transform FiringPosition;
	public GameObject [] PlayerBullets;
	public bool BulletsLeft = true;
	public int AmountOfBulletsActive = 0;
	public int AllBullets = 8;
	public GameObject HealthObject;
	public Slider HealthBar;
	public int MaxHealth = 100;
    public bool GotHit = false;
    public bool iFrames = false;
    public GameObject scoreSystem;

    // In the start function it uses a for loop to create all of the bullet objects and sets them to false ready to be fired, 
    // the other thing it does in the start function is it gets the health bar so it can be updated later on.
    void Start(){
		for (int i = 0; i < AllBullets; i++)
		{
			PlayerBullets[i] = Instantiate(Bullet);
			PlayerBullets[i].SetActive(false);
		}
		HealthObject = GameObject.FindGameObjectWithTag("HealthBar");
		HealthBar = HealthObject.GetComponent<Slider>();
	}
	// This is where it gets an deactivated bullet and makes the variable new bullet equal to it so it can be used to fire.
	private GameObject NewBullet(){
		for (int i = 0; i < AllBullets; i++){
			if (!PlayerBullets[i].activeSelf){
				return PlayerBullets[i];
			} 
		}
		return null;
	}
	// In the fixed update variable it gets the firing position (position of the gun) and it preforms all of the basic functions such as movement,shooting and checking,
    // if the player dies. The last thing is setting the slider value for the health bar equal to the players current health.
    void FixedUpdate() {
        FiringPosition = GameObject.FindGameObjectWithTag("FiringPosition").transform;
        movement();
        Shooting();
        LevelRestart();
		HealthBar.value = Health;
    }
    // In the shooting function it checks to see if the player has fired and then if he has make sure he can fire. if both are true then it starts the Coroutine.
    public void Shooting() {
        if (Input.GetButtonDown("Fire1") && FireAble) {
            
			Debug.Log("Shooting happens");
            StartCoroutine(Firing());
			
        }
    }

    //This void is used for movement of the player, it is used to perform when a movement key is pressed to move in the direction the player is trying to go.
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

    //The firing IEnumerator sets the fire check to false gets the bullet unactive bullet variable and then it sets its position and rotation based on the firing position,
    //and then sets the bullet to true which would give it the force to fire when its activated, the last thing it does is add 1 to the active bullet variable to keep track of them.
    IEnumerator Firing() {
        FireAble = false;
		GameObject ABullet = NewBullet();
		if (ABullet != null){
            ABullet.transform.SetPositionAndRotation(FiringPosition.position, FiringPosition.rotation);
            ABullet.SetActive(true);
			ABullet.transform.SetPositionAndRotation(FiringPosition.position, FiringPosition.rotation);
			AmountOfBulletsActive += 1;
		}
        yield return new WaitForSeconds(FireRate);
        FireAble = true;
   }
    // Level restart function is pretty simple it checks if the players health is less then 0 and if it is the game ends.
    // It also gets the scoring system and makes a boolean equal to true so that it calculates the total score.
    void LevelRestart()
    {
        if (Health < 0)
        {
            scoreSystem = GameObject.Find("ScoringSystem");
            scoreSystem.GetComponent<Scoring>().RecordScore = true;
            SceneManager.LoadScene(0);
        }

    }
    // The invincibility IEnumerator is used to give the player Iframes (a period where they cant be damaged) so it sets iframes to true, minuses from the players health
    // and after 2 seconds resets everything so the gothit check (checking if the player was hit) and iframes to false.
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
