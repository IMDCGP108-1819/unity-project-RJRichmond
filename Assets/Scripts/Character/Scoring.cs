using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {
    public int NumberOfBugsKilled = 0;
    public int NumberOfAsteroidsKilled = 0;
    public float TimeSurvived = 0f;
    public int Score = 0;
    public Text BugKilled;
    public Text AsteroidsDestroyed;
    public Text TimeSurvivedText;
    public bool ShouldRecord = true;

    // The start function resets the variables so the time the player has survived and if it should be recording the time.
    void Start()
    {
        ShouldRecord = true;
        TimeSurvived = 0f;
    }

    // The fixed update function updates some text objects which are stored in variables for example Bugs and then number the enemy has defeated.
    void FixedUpdate()
    {
        Recording();
        BugKilled.text = ("Bugs: "+ NumberOfBugsKilled);
        AsteroidsDestroyed.text = ("Asteroids: "+ NumberOfAsteroidsKilled);
        TimeSurvivedText.text = ("Time Survived: " + Mathf.RoundToInt(TimeSurvived) + " seconds");

    }
	//The recording variable is adding to the time survived variable by adding time.deltatime which is the time between last and the current frame.
    void Recording(){
        if (ShouldRecord == true){
            TimeSurvived += 1*Time.deltaTime;
        }
    }
}
