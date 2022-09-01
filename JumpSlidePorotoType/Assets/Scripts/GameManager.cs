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

    private int gemCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        panel.SetActive(false);
        state = GameState.PAUSE;
    }

    public void GameOver()
    {
        panel.gameObject.SetActive(true);
        state = GameState.PAUSE;
    }

    public void GetGem()
    {
        gemCount++;
    }
}
