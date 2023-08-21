using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    #region Sgt
    private static AdManager instance;
    public static AdManager GetInstance() => instance;

    private void Awake()
    {
        instance = null;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [SerializeField] private Button adButton;
    [SerializeField] private RewardedAd rewardedAd;
    [SerializeField] private string _adUnitId = "ca-app-pub-1233628081227721/6326780440";

    private void Start()
    {
        try
        {
            // 모바일 광고 SDK를 초기화
            MobileAds.Initialize(initStatus => { });
            LoadRewardedAd();            
            adButton.onClick.AddListener(() =>
            {
                ShowRewardedAd();
            });
        }
        catch(Exception e)
        {
            SystemMessage.GetInstance().OnMessage("Error : " + e.Message);
        }

    }

    private void LoadRewardedAd()
    {
        // 광고를 로드하기전 기존 광고 삭제
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // 세로운 광고를 등록
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

#if UNITY_EDITOR
        _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif

        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    SystemMessage.GetInstance().OnMessage("Faild load Ads.");
                    return;
                }

                rewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        if (DataManager.GetInstance().playerSaveData.GetData().isAdRemove)
        {
            adButton.interactable = false;
            PlayerStats.GetInstance().SaveAdsReward();
            return;
        }
        else
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                adButton.interactable = false;
                // 광고를 시작한다.
                rewardedAd.Show((Reward reward) =>
                {
                    // 광고 보상 콜백
                    Time.timeScale = 0;
                    PlayerStats.GetInstance().SaveAdsReward();
                });
            }
            else
            {
                SystemMessage.GetInstance().OnMessage("Not Ready : " + rewardedAd.CanShowAd());
            }
        }
    }
}
