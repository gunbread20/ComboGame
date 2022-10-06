using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    InterstitialAd interstitial = null;

    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        // Å×½ºÆ® ±¤°í
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        string adUnitId = "ca-app-pub-3688587815421766/5887004193";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);

        this.interstitial.OnAdClosed += Interstitial_OnAdClosed;
        this.interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;
        this.interstitial.OnAdFailedToShow += Interstitial_OnAdFailedToShow;
    }

    private void Interstitial_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        Debug.LogWarning("Fail to Show Ad");
        GameManager.instance.GameRestart();
    }

    private void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.LogWarning("Fail to Load Ad");
        GameManager.instance.GameRestart();
    }

    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        Debug.Log("Restart");
        GameManager.instance.GameRestart();
    }

    public void AdStart()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

}
