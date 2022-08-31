using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnStart : MonoBehaviour
{
    public GameObject _startLayout;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickStartBtn);
    }

    public void OnClickStartBtn()
    {
        GameManager.instance.state = GameState.RUNNING;
        _startLayout.gameObject.SetActive(false);
    }
}
