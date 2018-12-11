using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.Advertisements;

public class LevelManager : MonoBehaviour
{


    public int starting_level;
    private int current_level;
    public int max_level;
    public string mode; // BEGINNER OR CHALLENGE
    public GameObject original_gb;
    public GameObject[] hints;

    public int timerSpeed = 20; //Seconds Overall
    public bool boTimerActive = true;
    private int current_time;
    public Text countdown; //UI Text Object

    public Transform button;

    public delegate void HandleUnityAdResult(ShowResult result);
    public HandleUnityAdResult adResultHandle;
    // keep a copy of the executing script
    private IEnumerator timer_coroutine;


    public GameObject adpopup;

    public int GetCurrentLevel()
    {
        return current_level;
    }
    void Start()
    {
        boTimerActive = true;
        timer_coroutine = Timer();

        current_level = starting_level;
        Load_level(current_level);

        if(mode == "BEGINNER")
            DisplayHint();

        if (mode == "CHALLENGE")
        {
            
            current_time = timerSpeed;
            Debug.Log("Starting Timer");

            StartCoroutine(timer_coroutine);
        }
    }
    public void MoveToNextObject(GameObject to)//For disabling the first and enabling the next obj
    {
        gameObject.SetActive(false);
        if (to.name != "LastHint")
        {
            to.SetActive(true);
        }
    }
    void DisplayHint()
    {
        for (int i = 0; i < hints.Length; ++i)
        {
            hints[hints.Length - 1 - i].SetActive(false);

        }
        Debug.Log(PlayerPrefs.GetInt("PlayedTimes"));

        //use another variable for example "DisplayHint" (true/false) in case user clicks
        // on help tool
        if (PlayerPrefs.GetInt("PlayedTimes") < 4)
        {
            hints[0].SetActive(true);
            
        }
            int played_times = PlayerPrefs.GetInt("PlayedTimes");
            PlayerPrefs.SetInt("PlayedTimes", played_times + 1);
    }

    void Load_level(int level)
    {



        //Replace sprites
        //original image
        //string original = "Flags/" + GetCountryForLevel(current_level) + "/" + GetCountryForLevel(current_level) + "_original";
        string original = "Flags/" + "Resized/"+ GetCountryForLevel(current_level) + "_original";
        Sprite original_flag = Resources.Load<Sprite>(original);
        original_gb.GetComponent<Image>().sprite = original_flag;

        //string bw = "Flags/" + GetCountryForLevel(current_level) + "/" + GetCountryForLevel(current_level) + "_bw";
    string bw = "Flags/Resized/" + GetCountryForLevel(current_level) + "_bw";

        Sprite bw_flag = Resources.Load<Sprite>(bw);
        GameObject.Find("EditableMap").GetComponent<Image>().sprite = bw_flag;
        GameObject.Find("Color_Object").GetComponent<InitMap>().Initiate();
        current_time = timerSpeed;

        if(mode == "BEGINNER")
            LoadAdForBeginner();

        InitMap init = GameObject.Find("Color_Object").GetComponent<InitMap>();
        init.ShowHideHint();

        Button next_btn = GameObject.Find("Next_button").GetComponent<Button>();
        next_btn.interactable = false;

        Debug.Log("HoHo");
        boTimerActive = true;

        //Hide Particle System
        GameObject particles = GameObject.Find("Particles");
        if(particles)
            particles.SetActive(false);
    }

    void LoadAdForBeginner()
    {
        if(current_level % 5 == 0)
        {
            ShowRewardedAd();
        }
    }

    public void ResetFlag()
    {
        Load_level(current_level);
        GameObject.Find("Color_Object").GetComponent<InitMap>().Init();
        InitMap init = GameObject.Find("Color_Object").GetComponent<InitMap>();
        init.ShowHideHint();
    }

    public void Load_next_level()
    {

        GameObject.Find("Color_Object").GetComponent<InitMap>().Reset();
        current_level++;

        if (current_level > max_level)
        {
            current_level = 1;
        }

        Load_level(current_level);
        GameObject.Find("Color_Object").GetComponent<InitMap>().Init();
        InitMap init = GameObject.Find("Color_Object").GetComponent<InitMap>();
        init.ShowHideHint();
    }

    void Button_active(){
        GameObject myEditable_map = GameObject.Find("EditableMap");
        Texture2D editable_Texture = TextureExtension.textureFromSprite(myEditable_map.GetComponent<Image>().sprite);
        if (myEditable_map == true){
            button.GetComponent<Button>().interactable = true;
        }
        else{
            button.GetComponent<Button>().interactable = true;
        }


    }


    public string GetCountryForLevel(int level)
    {
        switch (level)
        {
            case 1: return "Algeria";
            case 2: return "Austria";
            case 3: return "Armenia";
            case 4: return "Azerbaijan";
            case 5: return "Bahamas";
            case 6: return "Bahrain";
            case 7: return "Bangladesh";
            case 8: return "Belgium";
            case 9: return "Benin";
            case 10: return "Bolivia";
            case 11: return "Botswana";
            case 12: return "Ukrain";
            case 13: return "Burkina_faso";
            case 14: return "Cameroon";
            case 15: return "Chad";
            case 16: return "Chile";
            case 17: return "Colombia";
            case 18: return "Congo_Republic";
            case 19: return "Costa_Rica";
            case 20: return "Cote_d_Ivoire";
            case 21: return "Czech_Republic";
            case 22: return "Denmark";
            case 23: return "Estonia";
            case 24: return "Finland";
            case 25: return "France";
            case 26: return "Gabon";
            case 27: return "Gambia";
            case 28: return "Georgia";
            case 29: return "Germany";
            case 30: return "Ghana";
            case 31: return "Guinea";
            case 32: return "Guinea_Bissau";
            case 33: return "Hungary";
            case 34: return "Iceland";
            case 35: return "Indonesia";
            case 36: return "Ireland";
            case 37: return "Italya";
            case 38: return "Japan";
            case 39: return "Lithuania";
            case 40: return "Luxembourg";
            case 41: return "Madagascar";
            case 42: return "Mali";
            case 43: return "Mauritius";
            case 44: return "Monaco";
            case 45: return "Netherlands";
            case 46: return "Nigera";
            case 47: return "Nigeria";
            case 48: return "Pakistan";
            case 49: return "Yemen";
            case 50: return "Peru";
            case 51: return "Poland";
            case 52: return "Qatar";
            case 53: return "Romania";
            case 54: return "Russia";
            case 55: return "Sierra_Leone";
            case 56: return "Somalia";
            case 57: return "Sudan";
            case 58: return "Sweden";
            case 59: return "Thailand";
            case 60: return "Turkey";
            case 61: return "Bulgaria";
            case 62: return "Palau";


        }
        return "Algeria";
    }



    // Update is called once per frame
    void Update()
    {
        

    }

    public void RequestVideoForReward()
    {
        Debug.Log("Rewarded Video");

        ShowRewardedAd();
    }
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            if(mode == "CHALLENGE")
                adpopup.SetActive(false);

            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);

            return;
        }
        if (mode == "CHALLENGE")
            adpopup.SetActive(false);
        Load_next_level();
    }

    public void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                if (mode == "CHALLENGE")
                    ResetFlag();
                else
                    Load_next_level();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                Load_next_level();
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                Load_next_level();
                break;
        }
    }
    public IEnumerator  Timer()
    {
        Debug.Log("Timer Entered");
        while (true)
        {
            if (boTimerActive)
            {
                if (current_time >= 0)
                {
                    countdown.text = (current_time).ToString("f0"); //Showing the Score on the Canvas
                    current_time--;


                }
                if (current_time == 0)
                    adpopup.SetActive(true);
                
            }
            yield return new WaitForSeconds(1f);
        }
        
      


    }
}