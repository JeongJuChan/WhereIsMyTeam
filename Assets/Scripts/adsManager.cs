using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class adsManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsInitializationListener, IUnityAdsLoadListener
{
    public static adsManager I;

    string adType;
    string gameId;
    void Awake()
    {
        I = this;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            adType = "Rewarded_iOS";
            gameId = "5369710";
        }
        else
        {
            adType = "Rewarded_Android";
            gameId = "5369711";
        }

        Advertisement.Initialize(gameId, true, this);
    }

    public void ShowRewardAd()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Show(adType, this);
        }
    }

    /*void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.LogError("광고 보기에 실패했습니다.");
                break;
            case ShowResult.Skipped:
                Debug.Log("광고를 스킵했습니다.");
                break;
            case ShowResult.Finished:
                // 광고 보기 보상 기능 
                Debug.Log("광고 보기를 완료했습니다.");
                break;
        }
    }*/

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError("광고 보기에 실패했습니다.");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("광고 보기를 시작했습니다.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("광고 보기를 클릭했습니다.");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("광고 보기를 완료했습니다.");
        gameManager.I.retryGame();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Load Failed: [{error}:{placementId}] {message}");
    }
}
