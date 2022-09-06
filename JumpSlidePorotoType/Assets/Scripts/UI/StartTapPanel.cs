using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartTapPanel : MonoBehaviour
{
    public GameObject _startLayout;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameManager.instance.state = GameState.RUNNING;
            _startLayout.gameObject.SetActive(false);
        }
    }

    public void GameStart()
    {
        GameManager.instance.state = GameState.RUNNING;
        _startLayout.gameObject.SetActive(false);
    }
}
