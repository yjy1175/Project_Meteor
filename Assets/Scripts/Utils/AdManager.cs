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
            // ����� ���� SDK�� �ʱ�ȭ
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
        // ���� �ε��ϱ��� ���� ���� ����
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // ���ο� ���� ���
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
                // ���� �����Ѵ�.
                rewardedAd.Show((Reward reward) =>
                {
                    // ���� ���� �ݹ�
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
