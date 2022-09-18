using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public Text nameInput;
    public Text highScoreText;

    public void StartGame() 
    {    
//the name input by the user with the input field is sent into the "currentName" variable
GameManager.instance.currentName = nameInput.text;
SceneManager.LoadScene(1);
    }

//checks if the game is being played in the unity editor. if true, it ends the session, if false, it closes the game application.
    public void Quit() 
    {
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
#else 
Application.Quit();
#endif
    }

//when the menu opens, the high score is loaded and showed at the top left of the screen
    private void Start() {
        GameManager.instance.LoadHighScore();
highScoreText.text = "Highest Score: " + GameManager.instance.savedName + " - " +  GameManager.instance.highScore.ToString(); 
    }
}
