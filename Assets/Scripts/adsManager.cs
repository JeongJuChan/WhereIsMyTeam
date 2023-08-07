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
                Debug.LogError("���� ���⿡ �����߽��ϴ�.");
                break;
            case ShowResult.Skipped:
                Debug.Log("���� ��ŵ�߽��ϴ�.");
                break;
            case ShowResult.Finished:
                // ���� ���� ���� ��� 
                Debug.Log("���� ���⸦ �Ϸ��߽��ϴ�.");
                break;
        }
    }*/

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError("���� ���⿡ �����߽��ϴ�.");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("���� ���⸦ �����߽��ϴ�.");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("���� ���⸦ Ŭ���߽��ϴ�.");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("���� ���⸦ �Ϸ��߽��ϴ�.");
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
