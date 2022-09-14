using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
     string restartAdID;
    string doubleReWardAdID;

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
        //restartAdID = "ca-app-pub-3940256099942544/1033173712";
        restartAdID =  "ca-app-pub-3940256099942544/5354046379";
        doubleReWardAdID = "ca-app-pub-3940256099942544/1033173712";
#else
        restartAdID = "ca-app-pub-3688587815421766/1060590486";
        doubleReWardAdID = "ca-app-pub-3688587815421766/5738202097";
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
