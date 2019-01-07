using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {
    public int NumberOfBugsKilled = 0;
    public int NumberOfAsteroidsKilled = 0;
    public float TimeSurvived = 0f;
    public int Score = 0;
    public bool ShouldRecord = true;
    public int TotalScore = 0;
    public bool RecordScore = false;
    
    // In the awake function when this object is awaken it then makes a list with all of the gameobjects with the score tag to remove duplicates of the scoring system
    // It then also performs the reset score function (resetting all counters).
    void Awake() {
        GameObject[] Dupe = GameObject.FindGameObjectsWithTag("Score");
        if (Dupe.Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
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
        
    // I have added in a check so that when the player dies it then calculates the total score.
        if (RecordScore == true) {
            CalScore();
            ResetScores();
            RecordScore = false;
        }

    }
	//The recording variable is adding to the time survived variable by adding time.deltatime which is the time between last and the current frame.
    void Recording(){
        if (ShouldRecord == true){
            TimeSurvived += 1*Time.deltaTime;
        }
    }
    // This function is used to calculate the total score.
    // It then also performs the reset score function (resetting all counters).
    void CalScore() {
        TotalScore = Mathf.RoundToInt(TimeSurvived * 5) + (NumberOfBugsKilled * 10) + (NumberOfAsteroidsKilled * 100);
    }

    // This function just resets all of the counter variables
    void ResetScores() {
        TimeSurvived = 0f;
        NumberOfAsteroidsKilled = 0;
        NumberOfBugsKilled = 0;
    }
}
