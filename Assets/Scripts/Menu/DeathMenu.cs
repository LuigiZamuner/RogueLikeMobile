using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class DeathMenu : AdsInitializer
{

    [SerializeField] RewardedAdsButton rewardedAds;
    private void Start()
    {
        Time.timeScale = 0;
        if (Advertisement.isInitialized)
        {
            rewardedAds.LoadAd();
            Debug.Log("deus");

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        SceneController.instance.NextLevel("InitialPlace");
        Destroy(gameObject);
        Time.timeScale = 1.0f;
    }
}
