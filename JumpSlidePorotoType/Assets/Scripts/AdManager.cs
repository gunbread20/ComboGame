using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public string restartAdID;
    public string doubleReWardAdID;

    private InterstitialAd restartAd;
    private InterstitialAd doubleRewardAd;

    public void Start()
    {
        //���� �ʱ�ȭ
        MobileAds.Initialize(initStatus =>
        {
            RequestInterstitial();
        });
    }
    private void RequestInterstitial()
    {
        //���� OS���� ����� �ڵ带 ����� ��� �̷��� �ϸ� ��
        //���� ���� ID�� /�� �� ���� ���� ���� ID
        //�� ID���� Google�� �����ϴ� �׽�Ʈ ID�̹Ƿ� ���� ���� ��� ����

#if UNITY_EDITOR
        restartAdID = "ca-app-pub-3940256099942544/1033173712";
        doubleReWardAdID = "ca-app-pub-3940256099942544/1033173712";
#endif


        //���� OS�� ��� ���⼭ �ٷ� ��Ʈ������ �Ⱦ��൵ ����
        this.restartAd = new InterstitialAd(restartAdID);
        AdRequest request = new AdRequest.Builder().Build();
        this.restartAd.OnAdClosed += RestartAd_OnAdClosed;
        this.restartAd.LoadAd(request);

        this.doubleRewardAd = new InterstitialAd(doubleReWardAdID);
        AdRequest request2 = new AdRequest.Builder().Build();
        this.doubleRewardAd.OnAdClosed += DoubleRewardAd_OnAdClosed;
        this.doubleRewardAd.LoadAd(request);
    }

    private void RestartAd_OnAdClosed(object sender, System.EventArgs e)
    {
        Debug.Log("Restart");
        GameManager.instance.GameRestart();
    }

    private void DoubleRewardAd_OnAdClosed(object sender, System.EventArgs e)
    {
       
        throw new System.NotImplementedException();
    }

    //���� �����ؾ� �� ���� �ܺο��� �� �Լ��� ȣ��
    public void AdStart()
    {
        if (this.restartAd.IsLoaded())
        {
            this.restartAd.Show();
        }
    }
}
