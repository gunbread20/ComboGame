using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FeverManager : MonoBehaviour
{
    ComboManager comboManager;

    public float feverTime = 5f;
    public float offFeverTime = 0f;
    private float feverMinTime = 5f;
    private float feverCurTime = 5f;
    private float feverMaxTime = 15f;

    bool isInit = true;
    bool isCanFever = true;

    PlayerControl playerControl;
    GroundMove groundMove;

    [SerializeField] PlayerTrail trail;
    [SerializeField] GameObject feverParent;
    [SerializeField] Image feverBar;
    [SerializeField] Text feverText;

    private void Start()
    {
        feverMinTime = feverTime;
        feverCurTime = feverTime;
        feverMaxTime = feverMinTime * GroundManager.Instance.maxTimeSpeed;

        GroundManager.Instance.speedUp.AddListener(AddFeverTime);
        GroundManager.Instance.speedClear.AddListener(ClearFeverTime);
        playerControl = FindObjectOfType<PlayerControl>();
        comboManager = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {
        
        if (GameManager.instance.state == GameState.RUNNING)
            feverParent.gameObject.SetActive(true);
        if (comboManager.feverComboCnt >= 10 * Time.timeScale && feverTime > 0 )
        {
            OnFever();
            trail.SetTrail(1f);
        }
        else if (feverBar.transform.localScale.x <= 0)
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

        feverBar.transform.localScale = new Vector2(feverTime / feverCurTime, 1f);

        isInit = false;
    }

    public void ChargeFever()
    {
        feverBar.transform.localScale = new Vector2(comboManager.feverComboCnt / (10f * Time.timeScale), 1f);

    }
    

    public void OffFever()
    {
        if (!isInit)
        {
            feverTime = feverCurTime;
            playerControl.isInvincible = true;
            Invoke("InvincibleOff", playerControl.invincibleTime);
            playerControl.InvincibleEffect();

            comboManager.feverComboCnt = 0;
        }

        offFeverTime += Time.deltaTime;

        Fever(false);

        isInit = true;
    }

    public void InvincibleOff()
    {
        playerControl.InvincibleOff();
    }

    IEnumerator Fever()
    {
        float temp = Time.timeScale;
        Time.timeScale *= 0.2f;

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = temp;
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
        playerControl.SetFeverEffect(isFever);
    }

    void AddFeverTime()
    {
        feverCurTime = feverMinTime * (Time.timeScale + GroundManager.Instance.addTimeSpeed);

        if (feverCurTime > feverMaxTime)
        {
            feverCurTime = feverMaxTime;
        }
    }

    void ClearFeverTime()
    {
        feverCurTime = feverMinTime;
    }
}
