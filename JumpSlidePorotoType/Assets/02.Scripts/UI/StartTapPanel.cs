using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartTapPanel : MonoBehaviour
{
    public Button _btnStart; 
    public GameObject _startLayout;

    private void Awake()
    {
        _btnStart.onClick.AddListener(GameStart);
    }

    public void GameStart()
    {
        GameManager.instance.state = GameState.RUNNING;
        _startLayout.gameObject.SetActive(false);
    }
}
