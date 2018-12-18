using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
// The purpose of this script is just to change scenes, this is used for the buttons on the main menu and one is mapped to starting loading the game scene,
// and the other is set to quit which stops the application, this is mostly for a standalone build and I don't think will do anything in the webGL version.
    public void StartGame() {
        SceneManager.LoadScene(1);
    }
    public void QuitGame() {
        print("Game quiting");
        Application.Quit();
    }
}
