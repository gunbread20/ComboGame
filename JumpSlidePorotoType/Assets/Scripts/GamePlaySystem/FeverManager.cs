using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FeverManager : MonoBehaviour
{
    ComboManager comboManager;

    public float feverTime = 5f;

    bool isInit = true;
    bool isCanFever = true;

    PlayerControl playerControl;

    [SerializeField] PlayerTrail trail;
    [SerializeField] Slider feverBar;
    [SerializeField] Text feverText;

    private void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        comboManager = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {
        if (GameManager.instance.state == GameState.RUNNING)
            feverBar.gameObject.SetActive(true);
        if (comboManager.feverComboCnt >= 10 && feverTime > 0 )
        {
            OnFever();
            trail.SetTrail(1f);
        }
        else if (feverBar.value <= 0)
        {
            OffFever();
            trail.RemoveTrail(1f);
        }
     
        if ( !playerControl.isFever)
        {
            ChargeFever();
        }
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
            playerControl.isInvincible = true;
            Invoke("InvincibleOff", playerControl.invincibleTime);
            comboManager.feverComboCnt = 0;
        }
        Fever(false);

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
