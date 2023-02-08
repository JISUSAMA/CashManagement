using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    //andriod ID: ca-app-pub-7508688389834412~7299369509

    // 배너 광고 
    //Banner Ad ID :  ca-app-pub-7508688389834412/6495222599
    //Banner Ad Sample : ca-app-pub-3940256099942544/6300978111
    //Banner IOS Sample ID: ca-app-pub-3940256099942544/2934735716

    //전면 광고
    //InterstitialAd Ad ID :  ca-app-pub-7508688389834412/6275730747
    //InterstitialAd Ad Sample : ca-app-pub-3940256099942544/1033173712
    //InterstitialAD IOS Sample: ca-app-pub-3940256099942544/4411468910

    //앱 열기 광고
    //AppOpenAd Ad Id ; ca-app-pub-7508688389834412/7141890164
    //AppOpenAd Ad Sample : ca-app-pub-3940256099942544/3419835294
    //AppOpenAd IOS Sample:  ca-app-pub-3940256099942544/5662855259

    private BannerView bannerView;
    public AdPosition position;
    private InterstitialAd interstitialAd;


    public void OnGUI()
    {
        GUI.skin.label.fontSize = 60;
        Rect textOutputRect = new Rect(
   
            0.15f * Screen.width,
            0.25f * Screen.height,
            0.7f * Screen.width,
            0.3f * Screen.height);
    }

    private void Start()
    {
        //  RequestBanner();
        AppOpenAdManager.Instance.LoadAd();
    }
    //////////////////////////////    배너 광고 로드하기   /////////////////////////////////////////////////
    private void RequestBanner()
    {
#if UNITY_ANDROID
        //string adUnitId = "ca-app-pub-7508688389834412/6495222599";
        string adUnitId = "ca-app-pub-3940256099942544/6300978111"; //sample
#elif UNITY_IPHONE
        //string adUnitId = "";
        string adUnitId = "ca-app-pub-3940256099942544/2934735716"; //sample
#else
        string adUnitId = "unexpected_platform";
#endif
        //새로운 광고 배너를 만들기 전에 정리를 해주어야한다.
        if(this.bannerView != null)
        {
            this.bannerView.Destroy();
        }
        AdSize adaptiveSize =
        AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        //AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth((int)MobileAds.Utils.GetDeviceScale());
        //광고 이벤트를 등록
        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;//OnAdLoaded 이벤트는 광고 로드가 완료되면 실행됩니다.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad; //이벤트는 광고 로드에 실패할 때 실행됩니다
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;//사용자가 광고를 탭하면 호출됩니다. 
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;//사용자가 광고의 도착 URL을 조회한 후 앱으로 돌아가면 이 메서드가 호출

        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);

        Canvas banner = GameObject.Find("ADAPTIVE(Clone)").GetComponent<Canvas>();
        banner.renderMode = RenderMode.ScreenSpaceCamera;
        banner.worldCamera = Camera.main;

    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
      print("HandleAdLoaded event received");
    }

    private void HandleOnAdOpened(object sender, EventArgs e)
    {
        print("HandleFailedToReceiveAd event received with message: "
                + e.ToString());
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        print("HandleAdOpened event received");
        RequestBanner();
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
       print("HandleAdClosed event received");
        print(String.Format("Ad Height: {0}, width: {1}",
         this.bannerView.GetHeightInPixels(),
         this.bannerView.GetWidthInPixels()));

    }

    public void ShowBanner(bool b)
    {
        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////



}

