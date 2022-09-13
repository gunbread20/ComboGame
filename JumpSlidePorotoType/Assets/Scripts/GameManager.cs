using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PAUSE,
    RUNNING
}

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    const string ScoreKey = "score";
    const string GemKey = "gem";

    public GameObject panel;
    public GameState state;
    public GameObject startPanel;

    public int gemCount;
    public int highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        if (!CheckPlayerPrefs(ScoreKey))
            SetPlayerPrefs(ScoreKey, 0);
        else
            highScore = GetPlayerPrefs(ScoreKey);

        if (!CheckPlayerPrefs(GemKey))
            SetPlayerPrefs(GemKey, 0);
        else
            gemCount = GetPlayerPrefs(GemKey);

        panel.SetActive(false);
        state = GameState.PAUSE;
    }

    private void OnApplicationQuit()
    {
        if (GetPlayerPrefs(ScoreKey) != highScore)
            SetPlayerPrefs(ScoreKey, highScore);

        if (GetPlayerPrefs(GemKey) != gemCount)
            SetPlayerPrefs(GemKey, gemCount);
    }

    public void GameOver()
    {
        panel.gameObject.SetActive(true);
        state = GameState.PAUSE;
    }

    public void GameRestart()
    {
        GenericPoolManager.FlushPool();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void NewRecord(int record)
    {
        highScore = record;
        SetPlayerPrefs(ScoreKey, highScore);
    }

    public void GetGem()
    {
        gemCount++;
        SetPlayerPrefs(GemKey, gemCount);
    }

    public bool CheckPlayerPrefs(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public void SetPlayerPrefs(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public int GetPlayerPrefs(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            return -1;
        }
    }
}
