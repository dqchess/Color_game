using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyProManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InitGameServices(); //

        // Grants the module-level consent for the Advertising module.
        Advertising.GrantDataPrivacyConsent();

        // Grants the vendor-level consent for AdMob.
        Advertising.GrantDataPrivacyConsent(AdNetwork.AdMob);

        // Grants the vendor-level consent for AdMob.
        Advertising.GrantDataPrivacyConsent(AdNetwork.UnityAds);

        // Grants the vendor-level consent for AdMob.
        Advertising.GrantDataPrivacyConsent(AdNetwork.AdColony);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void Awake()
    {
        // Init EM runtime if needed (useful in case only this scene is built).
        if (!EasyMobile.RuntimeManager.IsInitialized())
            EasyMobile.RuntimeManager.Init();
    }

    public void InitGameServices()
    {
        if (GameServices.IsInitialized())
        {
           // NativeUI.Alert("Alert", "The module is already initialized.");
        }
        else
        {
            GameServices.Init();
        }
    }

    public void ShowAchievements()
    {
        // Check for initialization before showing achievements UI
        if (GameServices.IsInitialized())
        {
            GameServices.ShowAchievementsUI();
        }
        else
        {
        #if UNITY_ANDROID
             GameServices.Init(); // start a new initialization process
        #elif UNITY_IOS
        Debug.Log("Cannot show achievements UI: The user is not logged in to Game Center.");
        #endif
        }
    }

    public void ShowLeaderBoardForTopUsers1()
    {
        GameServices.ShowLeaderboardUI(EM_GameServicesConstants.Leaderboard_Top_Users);
    }


}
