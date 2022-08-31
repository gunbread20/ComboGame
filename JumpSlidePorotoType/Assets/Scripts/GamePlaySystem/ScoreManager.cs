using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    

    public Text score;
    public int scoreCount;

    public Text overScore;

    ComboManager comboManager;
    void Start()
    {
        scoreCount = 0;

        comboManager = FindObjectOfType<ComboManager>();
    }


    public void AddScore()
    {
        //점수 증가
        scoreCount++;
        score.text = "" + scoreCount;

        //콤보 증가
        comboManager.IncreaseCombo();
    }

    public void GameOverScore()
    {
        overScore.text = "" + scoreCount;
    }
}
