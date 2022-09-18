using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
public int highScore = 0;
public string currentName = "";
public string savedName = "";
public bool highScoreBeaten = false;

   //allows for data - persistance between scenes
   private void Awake() 
   {
if (instance != null) 
{
    Destroy(gameObject);
    return;
}

instance = this;
 DontDestroyOnLoad(gameObject);
   }

  
  private void Start() {
   LoadHighScore();
    highScoreBeaten = false;
  }
   
    
  
  //makes a serializable class to hold the selected variables in a Json file later in the code
  [System.Serializable]
    private class SaveData
    {
        public string highScoreName;
        public string currentName;
        public int score;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();

         data.currentName = currentName;

//if the high score is beaten, the high score name is set to the current name, if not, it is set to the old saved name.
          if (highScoreBeaten == true) 
          {
         data.highScoreName = currentName;
          } else 
          {
            data.highScoreName = savedName;
          }

//if no new high score was gained, the program fetches the old highscore
        data.score = highScore; 
      
      //sends the data to a Json file to be accessed in a different game session
        string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

//fetches the data from the json file
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

            savedName = data.highScoreName;
             highScore = data.score;
        }
    }

   }








