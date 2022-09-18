using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text highScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    public int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        
        //Upon the start of the game, the high score is loaded and displayed at the top of the screen (with the name of the highscore achiever)
        GameManager.instance.LoadHighScore();
            highScoreText.text = "Highest Score: " + GameManager.instance.savedName + " - " +  GameManager.instance.highScore.ToString();
        
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";


        if (m_Points > GameManager.instance.highScore) 
        {
GameManager.instance.highScore = m_Points;

 //this boolean is used for preventing bug errors like the "savedName" variable in GameManager not saving correctly
GameManager.instance.highScoreBeaten = true; 

//if the amount of points gained is higher than the current highscore the name displayed is the new name which has achieved the highscore (the current name)
highScoreText.text = "Highest Score: " + GameManager.instance.currentName + " - " +  GameManager.instance.highScore.ToString(); 
        } else {
            //if the amount of points is smaller than the highscore, the name displayed is the saved name (the name of player which achieved the old highscore)
           highScoreText.text = "Highest Score: " + GameManager.instance.savedName + " - " +  GameManager.instance.highScore.ToString(); 
        }
        
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

//when the game is over, the high score is saved
GameManager.instance.SaveHighScore();
    Debug.Log("Hello");
    }

    public void MainMenu() {
        GameManager.instance.SaveHighScore();
        GameManager.instance.highScoreBeaten = false;
        SceneManager.LoadScene(0);
    }
}
