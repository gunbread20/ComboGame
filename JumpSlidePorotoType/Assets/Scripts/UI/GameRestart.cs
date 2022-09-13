using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRestart : MonoBehaviour
{
    public AdManager adManager;
    public Button restartBtn;

    [Range(0.01f, 1)]
    public float adPersent;

    private void Start()
    {
        restartBtn.onClick.AddListener(RestartBtn);
    }

    public void RestartBtn()
    {
        int Rand = Random.Range(0, 101);

        if (adPersent * 100 >= Rand)
        {
            adManager.AdStart();
        }
        else
        {
            GameManager.instance.GameRestart();
        }
    }
}
