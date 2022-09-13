using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FeverManager : MonoBehaviour
{
    ComboManager comboManager;

    public float feverTime = 5f;
    public float coolTime = 15f;

    bool isInit = true;
    bool isCanFever = true;

    PlayerControl playerControl;

    [SerializeField] Slider feverBar;
    [SerializeField] Text feverText;

    private void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        comboManager = FindObjectOfType<ComboManager>();
        coolTime = 0f;
    }

    private void Update()
    {
        if (GameManager.instance.state == GameState.RUNNING)
            feverBar.gameObject.SetActive(true);

        if (comboManager.feverComboCnt >= 10 && feverTime > 0 && coolTime <= 0)
        {
            OnFever();
        }
        else if (feverBar.value <= 0)
        {
            OffFever();

        }

        if(coolTime <= 0 && !playerControl.isFever)
            ChargeFever();
    }

    public void OnFever()
    {
        if (isInit)
        {
            StartCoroutine(Fever());
            FeverTextAnim();
        }

        feverTime -= Time.deltaTime;

        feverBar.value = feverTime / 5f;

        isInit = false;
    }

    public void ChargeFever()
    {
        feverBar.value = comboManager.feverComboCnt / 10f;
    }

    public void OffFever()
    {
        if (!isInit)
        {
            feverTime = 5f;
            coolTime = 15f;
            playerControl.isInvincible = true;
            Invoke("InvincibleOff", playerControl.invincibleTime);
            comboManager.feverComboCnt = 0;
        }
        Fever(false);

        coolTime -= Time.deltaTime;
        isInit = true;
    }

    public void InvincibleOff()
    {
        playerControl.InvincibleOff();
    }

    IEnumerator Fever()
    {
        Time.timeScale = 0.2f;

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1;
    }

    void FeverTextAnim()
    {
        Fever(true);
        feverText.rectTransform.DOAnchorPos(new Vector2(0f, 0f), 1f).SetUpdate(true).SetEase(Ease.OutSine).OnComplete(() =>
        {
            feverText.rectTransform.DOAnchorPos(new Vector2(-1000f, 0f), 1f).SetUpdate(true).OnComplete(() =>
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
