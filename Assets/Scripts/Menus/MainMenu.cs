using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {
    public Text highestScore;
    public GameObject ScoreSystem;
    public int TotalScore;

// The purpose of this script is just to change scenes, this is used for the buttons on the main menu and one is mapped to starting loading the game scene.
// I don't actually need any other functions like this since I can do the rest using unity UI and canvas, since with buttons you can set gameobject to active.
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    void Start() {
        ScoreSystem = GameObject.Find("ScoringSystem"); // Getting the scoring variable
    }
    // I have then added a piece of text which updates so that the the previous score is displayed on the main menu screen.
    void FixedUpdate() {
        if (ScoreSystem != null) { // If the scoring system variable has a gameobject in it
            TotalScore = ScoreSystem.GetComponent<Scoring>().TotalScore; // Get the total score from last run and store it.
            highestScore.text = ("" + TotalScore); // Changing text to show the score.
        }
        
    }
}
