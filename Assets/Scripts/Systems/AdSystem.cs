using UnityEngine;
using System;
using GoogleMobileAds.Api;
using System.Collections.Generic;

public class AdSystem : MonoBehaviour
{
    [Header("Настройки AdMob")]
    [SerializeField] private string appId = "ca-app-pub-3940256099942544~3347511713"; // Тестовый ID
    [SerializeField] private bool testMode = true;
    
    [Header("ID рекламных блоков")]
    [SerializeField] private string bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111"; // Тестовый ID
    [SerializeField] private string interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712"; // Тестовый ID
    [SerializeField] private string rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917"; // Тестовый ID
    
    [Header("Настройки показа")]
    [SerializeField] private bool showBannerOnStart = true;
    [SerializeField] private int interstitialShowInterval = 3; // Показывать каждые N уровней
    [SerializeField] private float minTimeBetweenAds = 60f; // Минимальное время между показами
    
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private int levelCount = 0;
    private float lastAdTime;
    private static AdSystem instance;
    
    public static AdSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AdSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("AdSystem");
                    instance = go.AddComponent<AdSystem>();
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeAds();
    }
    
    private void InitializeAds()
    {
        MobileAds.Initialize(initStatus => {
            Debug.Log("AdMob инициализирован");
            LoadAds();
        });
    }
    
    private void LoadAds()
    {
        if (testMode)
        {
            // Тестовые устройства
            List<string> deviceIds = new List<string>();
            deviceIds.Add("33BE2250B43518CCDA7DE426D04EE231");
            RequestConfiguration requestConfiguration = new RequestConfiguration
            {
                TestDeviceIds = deviceIds
            };
            MobileAds.SetRequestConfiguration(requestConfiguration);
        }
        
        // Загрузка баннерной рекламы
        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        bannerView.OnAdLoaded += HandleBannerAdLoaded;
        bannerView.OnAdFailedToLoad += HandleBannerAdFailedToLoad;
        
        // Загрузка межстраничной рекламы
        LoadInterstitialAd();
        
        // Загрузка наградной рекламы
        LoadRewardedAd();
        
        if (showBannerOnStart)
        {
            ShowBannerAd();
        }
    }
    
    private void LoadInterstitialAd()
    {
        interstitialAd = new InterstitialAd(interstitialAdUnitId);
        interstitialAd.OnAdLoaded += HandleInterstitialAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;
        interstitialAd.OnAdClosed += HandleInterstitialAdClosed;
        
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }
    
    private void LoadRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAdUnitId);
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }
    
    public void ShowBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Show();
        }
    }
    
    public void HideBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }
    
    public void ShowInterstitialAd()
    {
        if (Time.time - lastAdTime < minTimeBetweenAds)
            return;
            
        if (interstitialAd != null && interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
            lastAdTime = Time.time;
        }
    }
    
    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
    
    public void OnLevelCompleted()
    {
        levelCount++;
        if (levelCount % interstitialShowInterval == 0)
        {
            ShowInterstitialAd();
        }
    }
    
    #region Ad Event Handlers
    
    private void HandleBannerAdLoaded()
    {
        Debug.Log("Баннерная реклама загружена");
    }
    
    private void HandleBannerAdFailedToLoad(LoadAdErrorEventArgs args)
    {
        Debug.LogError($"Ошибка загрузки баннерной рекламы: {args.Message}");
    }
    
    private void HandleInterstitialAdLoaded()
    {
        Debug.Log("Межстраничная реклама загружена");
    }
    
    private void HandleInterstitialAdFailedToLoad(LoadAdErrorEventArgs args)
    {
        Debug.LogError($"Ошибка загрузки межстраничной рекламы: {args.Message}");
        LoadInterstitialAd();
    }
    
    private void HandleInterstitialAdClosed()
    {
        LoadInterstitialAd();
    }
    
    private void HandleRewardedAdLoaded()
    {
        Debug.Log("Наградная реклама загружена");
    }
    
    private void HandleRewardedAdFailedToLoad(LoadAdErrorEventArgs args)
    {
        Debug.LogError($"Ошибка загрузки наградной рекламы: {args.Message}");
        LoadRewardedAd();
    }
    
    private void HandleRewardedAdClosed()
    {
        LoadRewardedAd();
    }
    
    private void HandleUserEarnedReward(Reward reward)
    {
        // Награда за просмотр рекламы
        GameManager.Instance.AddReward(reward.Amount);
    }
    
    #endregion
    
    private void OnDestroy()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
        
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
        
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
    }
} 