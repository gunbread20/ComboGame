using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{

    private InterstitialAd restartAd;

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

#if UNITY_ANDROID
        string restartAdID = "ca-app-pub-3940256099942544/5354046379";
#elif UNITY_IPHONE
        string restartAdID = "ca-app-pub-3688587815421766/5887004193";
#else
        string restartAdID = "ca-app-pub-3688587815421766/5887004193";
#endif

        //���� OS�� ��� ���⼭ �ٷ� ��Ʈ������ �Ⱦ��൵ ����
        this.restartAd = new InterstitialAd(restartAdID);
        AdRequest request = new AdRequest.Builder().Build();
        this.restartAd.OnAdClosed += RestartAd_OnAdClosed;
        this.restartAd.LoadAd(request);
    }

    private void RestartAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("Restart");
        GameManager.instance.GameRestart();
    }

    private void RestartAd_OnAdClosed(object sender, System.EventArgs e)
    {
        Debug.Log("Restart");
        GameManager.instance.GameRestart();
    }

    //���� �����ؾ� �� ���� �ܺο��� �� �Լ��� ȣ��
    public void AdStart()
    {
        if (this.restartAd.IsLoaded())
        {
            this.restartAd.OnAdFailedToLoad += RestartAd_OnAdFailedToLoad;
            this.restartAd.Show();
        }
    }
}
