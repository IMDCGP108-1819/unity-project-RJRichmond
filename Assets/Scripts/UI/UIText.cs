using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Getting access to all of the unity UI functions and types like Text.
public class UIText : MonoBehaviour {
    public Text BugKilled;
    public Text AsteroidsDestroyed;
    public Text TimeSurvivedText;

    public GameObject ScoringSystem;

    // The start function is just getting the scoring system so it can access its variables.
    void Start () {
		ScoringSystem = GameObject.Find("ScoringSystem");
    }

    // The fixed update function updates some text objects which are stored in variables for example Bugs and then number the enemy has defeated.
    void FixedUpdate () {
        BugKilled.text = ("Bugs: " + ScoringSystem.GetComponent<Scoring>().NumberOfBugsKilled);
        AsteroidsDestroyed.text = ("Asteroids: " + ScoringSystem.GetComponent<Scoring>().NumberOfAsteroidsKilled);
        TimeSurvivedText.text = ("Time Survived: " + Mathf.RoundToInt(ScoringSystem.GetComponent<Scoring>().TimeSurvived) + " seconds");
    }
}
