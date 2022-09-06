using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FeverManager : MonoBehaviour
{
    ScoreManager scoreManager;
    ComboManager comboManager;

    public float feverTime = 5f;
    public float coolTime = 15f;

    bool isInit = true;

    PlayerControl playerControl;

    [SerializeField] Slider feverBar;
    [SerializeField] Text feverText;

    private void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        scoreManager = FindObjectOfType<ScoreManager>();
        comboManager = FindObjectOfType<ComboManager>();
        coolTime = 0;
    }

    private void Update()
    {
        if (comboManager.currentCombo >= 10 && feverTime > 0 && coolTime <= 0)
        {
            OnFever();
        }
        if (feverBar.value <= 0)
        {
            OffFever();
        }
    }

    public void OnFever()
    {
        if(isInit)
        {
            FeverTextAnim();
        }


        feverBar.gameObject.SetActive(true);

        feverTime -= Time.deltaTime;

        feverBar.value = feverTime / 5f;

        Fever(true);

        isInit = false;
    }

    public void OffFever()
    {
        if(!isInit)
        {
            feverTime = 5f;
            coolTime = 15f;
        }
        Fever(false);


        feverBar.gameObject.SetActive(false);
        coolTime -= Time.deltaTime;
        isInit = true;
    }


    void FeverTextAnim()
    {
        feverText.rectTransform.DOAnchorPos(new Vector2(0f, 0f), 1f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            feverText.rectTransform.DOAnchorPos(new Vector2(-1000f, 0f), 1f).OnComplete(() =>
            {
                feverText.rectTransform.anchoredPosition = new Vector3(1000f, 0f);
            });
        });
    }


    void Fever(bool isFever)
    {
        playerControl.isFever = isFever;
    }
}
