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

    [SerializeField]
    private float level_0Max = 20;
    [SerializeField]
    private float level_1Max = 30;
    [SerializeField]
    private float level_2Max = 40;
    [SerializeField]
    private float level_3Max = 50;
    [SerializeField]
    private float level_4Max = 60;

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
        //점수 증가
        scoreCount++;
        score.text = "" + scoreCount;

        //콤보 증가
        comboManager.IncreaseCombo();

        // 점수에 따라 레벨 업
        LevelCount();

        if (scoreCount % 5 == 0 && scoreCount != 0)
            GroundManager.Instance.speedUp.Invoke();
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

    private void LevelCount()
    {
        if (scoreCount > level_4Max)
        {
            return;
        }
        else if (scoreCount > level_3Max)
        {
            GroundManager.Instance.LevelUp();
        }
        else if (scoreCount > level_2Max)
        {
            GroundManager.Instance.LevelUp();
        }
        else if (scoreCount > level_1Max)
        {
            GroundManager.Instance.LevelUp();
        }
        else if (scoreCount > level_0Max)
        {
            GroundManager.Instance.LevelUp();
        }
        else
        {
            return;
        }
    }
}
