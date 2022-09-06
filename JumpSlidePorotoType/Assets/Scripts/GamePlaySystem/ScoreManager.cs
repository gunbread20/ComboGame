using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text score;
    public int scoreCount;
    public Text highScore;
    public Text overScore;

    public GameObject newRecord;

    ComboManager comboManager;
    void Start()
    {
        scoreCount = 0;

        comboManager = FindObjectOfType<ComboManager>();
        highScore.text = "" + GameManager.instance.highScore;
    }


    public void AddScore()
    {
        //���� ����
        scoreCount++;
        score.text = "" + scoreCount;

        //�޺� ����
        comboManager.IncreaseCombo();
    }

    public void GameOverScore()
    {
        if (scoreCount > GameManager.instance.highScore)
        {
            GameManager.instance.NewRecord(scoreCount);
            newRecord.SetActive(true);
        }
        else
        {
            newRecord.SetActive(false);
        }

        overScore.text = "" + scoreCount;
    }
}
