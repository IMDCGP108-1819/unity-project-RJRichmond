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

    void Start()
    {
        ShouldRecord = true;
        TimeSurvived = 0f;
    }

    void FixedUpdate()
    {
        Recording();
        BugKilled.text = ("Bugs: "+ NumberOfBugsKilled);
        AsteroidsDestroyed.text = ("Asteroids: "+ NumberOfAsteroidsKilled);
        TimeSurvivedText.text = ("Time Survived: " + Mathf.RoundToInt(TimeSurvived) + " seconds");

    }
	
    void Recording(){
        if (ShouldRecord == true){
            TimeSurvived += 1*Time.deltaTime;
        }
    }
}
