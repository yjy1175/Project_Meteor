using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;

public class IAPManager : MonoBehaviour
{
    [SerializeField] private CodelessIAPButton pd10;
    [SerializeField] private CodelessIAPButton pd30;
    [SerializeField] private CodelessIAPButton pd50;
    [SerializeField] private CodelessIAPButton pd100;
    [SerializeField] private CodelessIAPButton pd200;
    [SerializeField] private CodelessIAPButton pd500;
    [SerializeField] private CodelessIAPButton removeAds;
    [SerializeField] private CodelessIAPButton doubleDropRate;

    private void Start()
    {
        pd10.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
        {
            StartCoroutine(BuyDregSuccess(10));
        }));
        pd30.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
        {
            StartCoroutine(BuyDregSuccess(30));
        }));
        pd50.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
        {
            StartCoroutine(BuyDregSuccess(50));
        }));
        pd100.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
        {
            StartCoroutine(BuyDregSuccess(100));
        }));
        pd200.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
        {
            StartCoroutine(BuyDregSuccess(200));
        }));
        pd500.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
        {
            StartCoroutine(BuyDregSuccess(500));
        }));
        removeAds.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
        {
            StartCoroutine(BuyRemoveAdsSuccess());
        }));
        doubleDropRate.onPurchaseComplete.AddListener(new UnityAction<Product>((product) =>
        {
            StartCoroutine(BuyDoubleDropRateSuccess());
        }));
    }

    IEnumerator BuyDregSuccess(int count)
    {
        yield return new WaitForEndOfFrame();
        PlayerSaveData data = DataManager.GetInstance().playerSaveData.GetData();
        data.perfectDregCount += count;

        DataManager.GetInstance().UpdatePlayerSaveData(data);
        SystemMessage.GetInstance().OnMessage("온전한운석 x" + count + " 구매완료.");
    }

    IEnumerator BuyRemoveAdsSuccess()
    {
        yield return new WaitForEndOfFrame();
        PlayerSaveData data = DataManager.GetInstance().playerSaveData.GetData();
        data.isAdRemove = true;

        DataManager.GetInstance().UpdatePlayerSaveData(data);

        LobbyUIManager.GetInstance().ToggleShopPannel(true);
        SystemMessage.GetInstance().OnMessage("광고 비활성화 구매완료.");
    }

    IEnumerator BuyDoubleDropRateSuccess()
    {
        yield return new WaitForEndOfFrame();
        PlayerSaveData data = DataManager.GetInstance().playerSaveData.GetData();
        data.isDoubleDropRate = true;

        DataManager.GetInstance().UpdatePlayerSaveData(data);

        LobbyUIManager.GetInstance().ToggleShopPannel(true);
        SystemMessage.GetInstance().OnMessage("부스러기 드롭 x2 구매완료.");
    }
}
