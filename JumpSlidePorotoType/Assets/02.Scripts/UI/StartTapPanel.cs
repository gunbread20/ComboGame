using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class StartTapPanel : MonoBehaviour
{
    public Button _btnStart; 
    public GameObject _startLayout;

    SkinStorePanel skinStorePanel;

    public Text exceptionText;
    private void Awake()
    {
        _btnStart.onClick.AddListener(GameStart);
        skinStorePanel = GetComponent<SkinStorePanel>();
    }

    public void GameStart()
    {
        if (skinStorePanel.normalSkin == null)
        {
            DOTween.Kill(exceptionText);
            exceptionText.DOFade(1, 0.2f).OnComplete(() =>
            {
                exceptionText.DOFade(0, 1.5f);
            });
            return;
        }
        else
        {
            for (int i = 0; i < skinStorePanel.skinObj.Count; i++)
            {
                skinStorePanel.skinObj[i].SetActive(false);
            }
            skinStorePanel.normalSkin.SetActive(true);

            GameManager.instance.state = GameState.RUNNING;
            _startLayout.gameObject.SetActive(false);
        }
    }
}
