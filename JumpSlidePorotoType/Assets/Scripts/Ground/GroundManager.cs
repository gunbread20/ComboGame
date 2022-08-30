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

public class GroundManager : MonoBehaviour
{
    static public GroundManager instance;

    public GameObject panel;
    public GameObject[] grounds;
    public float speed;
    public GameState state;

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

    public void SpawnGround()
    {
        int r = Random.Range(0, grounds.Length);

        Instantiate(grounds[r], new Vector3(0, 0, 10), Quaternion.identity, transform);
    }

    public void GameOver()
    {
        panel.gameObject.SetActive(true);
        overScore.text = "" + scoreCount;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Test");
    }

    public void AddScore()
    {
        scoreCount++;
        score.text = "Score: " + scoreCount;
    }
}
