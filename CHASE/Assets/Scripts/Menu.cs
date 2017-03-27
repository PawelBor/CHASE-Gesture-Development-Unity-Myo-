using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Canvas MainCanvas;
    public Canvas GameLevelCanvas;
    public Canvas HighScoreCanvas;
    public HighScoreManager hsm;
    public Text highscores;

    private void Awake()
    {
        GameLevelCanvas.enabled = false;
        HighScoreCanvas.enabled = false;
    }

    public void GameCanvasOn()
    {
        GameLevelCanvas.enabled = true;
        MainCanvas.enabled = false;
        HighScoreCanvas.enabled = false;
    }

    public void MainMenuOn()
    {
        GameLevelCanvas.enabled = false;
        MainCanvas.enabled = true;
        HighScoreCanvas.enabled = false;
    }

    public void ScoresOn()
    {
        GameLevelCanvas.enabled = false;
        MainCanvas.enabled = false;
        HighScoreCanvas.enabled = true;
        highscores.text = "";

        for (int i = 0; i < hsm.highScoresList.Count; i++)
        {
            highscores.text += hsm.highScoresList[i].Level + " " + hsm.highScoresList[i].Time + " Points: " + hsm.highScoresList[i].Points + "\n";
        }
    }

    public void ClearScoresDB()
    {
        hsm.DeleteScores();
        hsm.highScoresList.Clear();
        highscores.text = "";
    }

    public void LoadLabyrinth()
    {
        SceneManager.LoadScene("lab");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("BasicPlate");
    }

    public void LoadRamps()
    {
        SceneManager.LoadScene("scrap");
    }

}
