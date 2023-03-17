using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AppOpenAdManager : MonoBehaviour
{
    //유틸리티 클래서 생성 https://developers.google.com/admob/unity/app-open?hl=ko#create
    private static AppOpenAdManager instance;
    private AppOpenAd ad;
    private bool isShowingAd = false;

#if UNITY_ANDROID
        //string AD_UNIT_ID  = "ca-app-pub-7508688389834412/7141890164";
        string AD_UNIT_ID  = "ca-app-pub-3940256099942544/3419835294"; //sample
#elif UNITY_IPHONE
        //string AD_UNIT_ID  = "";
        string AD_UNIT_ID  = "ca-app-pub-3940256099942544/5662855259"; //sample
#else
    string AD_UNIT_ID = "unexpected_platform";
#endif

    public static AppOpenAdManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AppOpenAdManager();
            }

            return instance;
        }
    }
    // TODO: Add loadTime field
    private DateTime loadTime;

    private bool IsAdAvailable
    {
        get
        {
                // TODO: Consider ad expiration
                return ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;
        }
    }

    public void LoadAd()
    {
        if (IsAdAvailable)
        {
            return;
        }

        AdRequest request = new AdRequest.Builder().Build();
        AppOpenAd.LoadAd(AD_UNIT_ID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
    
                return;
            }

            ad = appOpenAd;
            loadTime = DateTime.UtcNow;
        }));
    }

    public void ShowAdIfAvailable()
    {
        if (!IsAdAvailable || isShowingAd)
        {
            return;
        }

        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;

        ad.Show();
    }

    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        isShowingAd = false;
        LoadAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
                args.AdValue.CurrencyCode, args.AdValue.Value);
    }



}

