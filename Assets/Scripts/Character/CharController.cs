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
	
	void Start(){
		for (int i = 0; i < AllBullets; i++)
		{
			PlayerBullets[i] = Instantiate(Bullet);
			PlayerBullets[i].SetActive(false);
		}
		HealthObject = GameObject.FindGameObjectWithTag("HealthBar");
		HealthBar = HealthObject.GetComponent<Slider>();
	}
	
	private GameObject NewBullet(){
		for (int i = 0; i < AllBullets; i++){
			if (!PlayerBullets[i].activeSelf){
				return PlayerBullets[i];
			} 
		}
		return null;
	}
	
    void FixedUpdate() {
        FiringPosition = GameObject.FindGameObjectWithTag("FiringPosition").transform;
        movement();
        Shooting();
        LevelRestart();
		HealthBar.value = Health;
		
    }
            
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

    void LevelRestart() {
        if (Health < 0) {
            SceneManager.LoadScene(0);
        }

    }
	
	void HealthSlider(){
		HealthBar.value = Health / MaxHealth;
	}
}
