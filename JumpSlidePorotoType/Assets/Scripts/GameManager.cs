using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    PAUSE,
    RUNNING
}

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public GameObject panel;
    
    public GameState state;

    public GameObject startPanel;

    public Text score;
    public int scoreCount;

    public Text overScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        panel.SetActive(false);
        state = GameState.PAUSE;
        scoreCount = 0;
    }

    public void GameOver()
    {
        panel.gameObject.SetActive(true);
        overScore.text = "" + scoreCount;
    }

    public void AddScore()
    {
        scoreCount++;
        score.text = "Score: " + scoreCount;
    }
}
