using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.Advertisements;
using EasyMobile;
using System.Linq;

public class LevelManager : MonoBehaviour
{


    public int starting_level;
    private int current_level;
    public int max_level;
    public string mode; // BEGINNER , CHALLENGE OR EXPERT
    public GameObject original_gb;
    public GameObject dyk_flag_img;


    public GameObject dyk_country_name;
    public GameObject dyk_country_description;

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

    public Facts country_facts;

    public Image progressBar;
    public Text progressBarText;

    public void UpdateMatchPercentage(float percentage)
    {
        Debug.Log("Setting Fill Percentage: " + percentage);
        progressBar.fillAmount = percentage / 100f; ;
        progressBarText.text = progressBar.fillAmount.ToString();
    }

    public void ResetZoom()
    {
        GameObject.Find("Color_Object_Parent").GetComponent<PinchZoomScrollRect>()._currentZoom= 1;
    }


    public void setDyKInfo()
    {

        dyk_flag_img.GetComponent<Image>().sprite = original_gb.GetComponent<Image>().sprite;
        dyk_country_description.GetComponent<Text>().text = GetCountryDescriptionForLevel(current_level);
        dyk_country_name.GetComponent<Text>().text = GetCountryForLevel(current_level);


        // Use LINQ to find the country fact
        FactsData[] factsArray = country_facts.dataArray;
        FactsData fact = factsArray.Where(s => s.Countryname == GetCountryForLevel(current_level)).FirstOrDefault();
        //       dyk_country_description.GetComponent<Text>().text = fact.Facts;


        Debug.Log("Fact:" + fact);
        List<string> fact_list  = fact.Facts.Split('$').ToList();
        string current_fact_toshow = fact_list[Random.Range(0, fact_list.Count)];
        dyk_country_description.GetComponent<Text>().text = current_fact_toshow;

    }


    public int GetCurrentLevel()
    {
        return current_level;
    }

    private int GetLevelToLoad()
    {
         
        int level = 12;
        return level;
        if (mode == "BEGINNER")
            level = PlayerPrefs.GetInt("LastBeginnerLevelCracked");
        if (mode == "CHALLENGE")
            level = PlayerPrefs.GetInt("LastChallengeLevelCracked");
        if (mode == "EXPERT")
            level = PlayerPrefs.GetInt("LastExpertLevelCracked");
        return level;

    }

    // Subscribe to the event
    void OnEnable()
    {
        Advertising.InterstitialAdCompleted += InterstitialAdCompletedHandler;
        Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
        Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
    }
    // The event handler
    void InterstitialAdCompletedHandler(InterstitialAdNetwork network, AdLocation location)
    {
        Debug.Log("Interstitial ad has been closed.");
        
    }
    // Unsubscribe
    void OnDisable()
    {
        Advertising.InterstitialAdCompleted -= InterstitialAdCompletedHandler;
        Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
        Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
    }

    // Event handler called when a rewarded ad has completed
    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
    {
        Debug.Log("Rewarded ad has completed. The user should be rewarded now.");
        ResetFlag(); // For Challenge and Expert Mode
    }
    // Event handler called when a rewarded ad has been skipped
    void RewardedAdSkippedHandler(RewardedAdNetwork network, AdLocation location)
    {
        Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
    }

    void Start()
    {

        // Show banner ad
        Advertising.ShowBannerAd(BannerAdPosition.Bottom);


        boTimerActive = true;
        timer_coroutine = Timer();

        //current_level = starting_level;
        current_level = GetLevelToLoad();

        Load_level(current_level);

        if (mode == "BEGINNER")
            DisplayHint();

        if ((mode == "CHALLENGE") || (mode == "EXPERT"))
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
        ResetZoom();
        UpdateMatchPercentage(0);
        if (current_level > max_level)
        {
            current_level = 1;
        }

        if (mode == "BEGINNER")
        {
            PlayerPrefs.SetInt("LastBeginnerLevelCracked", level);
            if (Application.platform == RuntimePlatform.Android)
            {
                GameServices.ReportScore(level, EM_GameServicesConstants.Leaderboard_Champions);
                if (level == 10)
                {
                    GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_10_Flags);
                    GameServices.ShowAchievementsUI();
                }
            }

            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                Debug.Log("Not supported on iOS");

        }
        if (mode == "CHALLENGE")
             PlayerPrefs.SetInt("LastChallengeLevelCracked", level);

       if (mode == "EXPERT")
            PlayerPrefs.SetInt("LastExpertLevelCracked", level);




        //Replace sprites
        //original image
        //string original = "Flags/" + GetCountryForLevel(current_level) + "/" + GetCountryForLevel(current_level) + "_original";
        string original = "Flags/" + "Resized/" + GetCountryForLevel(current_level) + "_original";
        Sprite original_flag = Resources.Load<Sprite>(original);
        original_gb.GetComponent<Image>().sprite = original_flag;

        //string bw = "Flags/" + GetCountryForLevel(current_level) + "/" + GetCountryForLevel(current_level) + "_bw";
        string bw = "Flags/Resized/" + GetCountryForLevel(current_level) + "_bw";

        Sprite bw_flag = Resources.Load<Sprite>(bw);
        GameObject.Find("EditableMap").GetComponent<Image>().sprite = bw_flag;
        GameObject.Find("Color_Object").GetComponent<InitMap>().Initiate();
        current_time = timerSpeed;

        //if ((current_level % 5 == 0) && (mode == "BEGINNER"))
        //    LoadAdForBeginner();

        InitMap init = GameObject.Find("Color_Object").GetComponent<InitMap>();
        init.ShowHideHint();

        Button next_btn = GameObject.Find("Next_button").GetComponent<Button>();
        next_btn.interactable = false;

        boTimerActive = true;

        //Hide Particle System
        GameObject particles = GameObject.Find("Particles");
        if(particles)
            particles.SetActive(false);

        setDyKInfo();
    }

    void LoadAdForBeginner()
    {

        //// Check if interstitial ad is ready
        //bool isReady = Advertising.IsInterstitialAdReady();
        //// Show it if it's ready
        //if (isReady)
        //{
        //    Advertising.ShowInterstitialAd();
        //}

        InterStitialAdsUnity();
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
        if (level > 62) {

            // Use LINQ to find the country fact
            FactsData[] factsArray = country_facts.dataArray;
            FactsData fact = factsArray.Where(s => s.Levelno ==level).FirstOrDefault();
            //       dyk_country_description.GetComponent<Text>().text = fact.Facts;



            string country_name = fact.Countryname;
            Debug.Log("Country Selected:" + country_name);
            return country_name;

        }

        switch (level)
        {
            case 1: return "Botswana";
            case 2: return "Austria";
            case 3: return "Armenia";
            case 4: return "Azerbaijan";
            case 5: return "Bahamas";
            case 6: return "Bahrain";
            case 7: return "Bangladesh";
            case 8: return "Belgium";
            case 9: return "Benin";
            case 10: return "Algeria";
            case 11: return "Bolivia";
            case 12: return "Ukrain";
            case 13: return "Burkina_Faso";
            case 14: return "Cameroon";
            case 15: return "Chad";
            case 16: return "Chile";
            case 17: return "Colombia";
            case 18: return "Congo_Republic";
            case 19: return "Costa_Rica";
            case 20: return "Cote_D_Ivoire";
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


    public void ShowRewardedAd()
    {
        // Check if rewarded ad is ready
        //bool isReady = Advertising.IsRewardedAdReady();
        bool isReady = Advertisement.IsReady("rewardedVideo");
        Debug.Log("Rewarded Ad is " + isReady);
        if (isReady)
        {
            if((mode == "CHALLENGE") || (mode == "EXPERT"))
                adpopup.SetActive(false);

            //Advertising.ShowRewardedAd();
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);

            adpopup.SetActive(false); // For Beginner mode
            return;
        }
    }




    public void InterStitialAdsUnity()
    {
        // Check if rewarded ad is ready
        //bool isReady = Advertising.IsRewardedAdReady();
        bool isReady = Advertisement.IsReady("rewardedVideo");
        Debug.Log("Rewarded Ad is " + isReady);
        if (isReady)
        {
            
            Advertisement.Show("rewardedVideo");

            
            return;
        }
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
                if ((mode == "CHALLENGE") || (mode == "EXPERT"))
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



    public string GetCountryDescriptionForLevel(int level)
    {
        switch (level)
        {
            case 1: return "Botswana is approximately the size of France, but has only 2 million people living in the country, compared to France’s 66.9 million.";
            case 2: return "The name Austria derives from a Germanic word ‘austro’, meaning ‘east’. Ferdinand Porsche, who is the founder of the German sports car company ‘Porsche’, was from Austria.";
            case 3: return "Chess is a compulsory subject in schools. All students have to take chess as a compulsory subject in school and there are even exams for it.";
            case 4: return "When families are matchmaking in Azerbaijan, the tea tray gives a good indication of how arrangements are progressing. If it’s served without sugar, more negotiating needs to be done; if it’s sweet, a wedding is definitely on the cards.";
            case 5: return "Dean’s Blue Hole, West of Clarence Town, on Long Island Bahamas. Plunging 202 metres into the sea, it’s the deepest blue hole in the world.";
            case 6: return "In addition to freshwater wells, which were once in abundance, there are places in the sea north of Bahrain where fresh water bubbles up in the middle of the salt water.";
            case 7: return "Cox's Bazar, Bangladesh is longest unbroken sea beach in the world.";
            case 8: return "Belgian men are the second tallest in the world – with a height of 181.7cm, only Dutchmen are taller at 182.5cm. The same can't be said for Belgian women, however, who rank 21 at 165.5cm tall.";
            case 9: return "Benin was the first country in the 1990s to make the transition from a dictatorship to a multiparty democracy.";
            case 10: return "The Sahara Desert covers 90% of the land and with some the hottest temperatures ever recorded, it is not suitable for occupation. As a result of this, only 12% of the country has human communities.";
            case 11: return "Bolivia is home to Salar de Uyuni, the 'largest natural mirror on Earth'Bolivia lost its colony on the Pacific Ocean to Chile following the war of the Pacific.";
            case 12: return "Data from the World Health Organisation (WHO) ranks Ukraine sixth for alcohol consumption, with 13.9 litres glugged per capita per year. Only Belarus, which tops the chart, Moldova, Lithuania, Russia and Romania consume more.";
            case 13: return "497 species of birds have been found in Burkina Faso, only one of which is endangered, which is the Egyptian vulture. The country has ten important bird areas.";
            case 14: return "Every variety of flora and fauna that is available in tropical Africa can be found in Cameroon as well? It is home to at least 409 species of mammals and 165 species of birds.";
            case 15: return "People of Chad use Kakaki, a long metal trumpet in traditional ceremonial music. The instrument signifies power and is always played by men.";
            case 16: return "16,000 fireworks were exploded in the Chilean city of Valparaiso in 2007. This remains a Guinness World Record.";
            case 17: return "Colombia is named after the legendary Italian explorer, navigator, and colonizer – Christopher Columbus.";
            case 18: return "The country is among the most resource-rich countries on the planet, with an abundance of gold, tantalum, tungsten, and tin – all minerals used in electronics such as cell phones and laptops – yet it continues to have an extremely poor population.";
            case 19: return "Costa Ricans refer to themselves as “Ticos” (males) and “Ticas” (females).  Foreigners are often called “Gringos” (males) and “Gringas” (females).";
            case 20: return "Cote d’Ivorie has the largest church in the world. The Basilica of Our Lady of Peace of Yamoussoukro surpasses even St Peter’s Basilica, with an exterior area of 30,000 square metres and It can hold about 18,000 worshipers, though is very rarely full.";
            case 21: return "The language has a formal and an informal form, and the natives are patient with foreigners who use them inappropriately. They are proud of their Czech language, which the U. S. Foreign Institute ranks as the second most difficult language to learn.";
            case 22: return "In Denmark, it rains or snows every second day";
            case 23: return "Estonians love their forests, bogs and all the creatures that live there such as lynxes, brown bears, wolves, foxes, rabbits and deers. It's right to say that Estonians come with a tree hugging trait.";
            case 24: return "There are exactly 179,584 islands in and around Finland, a world record. Off the southwest coast are the  land Islands, which are populated by the Swedish speaking people and have been autonomous since 1921.";
            case 25: return "The French Army was the first to use camouflage in 1915 (World War I) – the word camouflage came from the French verb ‘to make up for the stage’. Guns and vehicles were painted by artists called camofleurs.";
            case 26: return "The entire country of Gabon is just slightly smaller than the state of Colorado in the United States";
            case 27: return "The Gambia left the Commonwealth in 2013. Its president said the British had taught them nothing except how to sing Baa, Baa Black Sheep and God Save the Queen.";
            case 28: return "Georgia was founded in 1732 by British Member of Parliament James Oglethorpe as a felon colony. Oglethorpe wanted to use the colony as a place for prisoners who could not pay their debts.";
            case 29: return "Germany was the first country in the world to adopt Daylight saving time – DST, also known as summer time. This occured in 1916 in the midst of WWI and was put in place to conserve energy";
            case 30: return "Ghana is part of one of Africa’s oldest civilizations – the Ghana Empire. This is a civilization that is over one millennium old. It is famed for its legendary warriors who ruthlessly conquered territories and exerted a powerful reign.";

            case 31: return "Formerly known as French Guinea, Guinea gained independence in 1958. It is now sometimes known as Guinea-Conakry to distinguish it from Guinea-Bissau.The average life expectancy here is 49 years. Around 60% of the land is classified as woodland or forest. We bet it’s amazing.";

            case 32: return "Once hailed as a potential model for African development, Guinea-Bissau is now one of the poorest countries in the world.";

            case 33: return "Thanks to an abundance of natural hot springs, Hungary can boast around 450 public spas and bathhouses. A prominent bathing culture has existed since Roman times; it is supposedly the best cure for a hangover – or “cat’s wail” as the Hungarian term macskajaj translates.";

            case 34: return "There is a volcanic eruption every 4 years on average. A majority of Icelanders believe in elves. There are no forests in Iceland. At 43.5 hours per week, they have the longest work week in Europe. Babies in Iceland are routinely left outside to nap.";

            case 35: return "The world’s largest flower, Rafflesia Arnoldi, weighs up to 7 kg (15 pounds) and only grows on the island of Sumatra, Indonesia. Its petals grow to 0.5 meters (1.6 feet) long and 2.5 cm (1 inch) thick.";

            case 36: return "When children are little, each birthday it is traditional to pick up the child, turn them over and bump their head gently on their birthday cake. The child’s head is bumped once for each year they have lived. It is believed that partaking in this tradition brings good luck and good fortune to the child.";

            case 37: return "In 2007, a dog named Rocco discovered a truffle in Tuscany that weighed 3.3 pounds. It was sold at an auction for $333,000 (USD), a world record for a truffle.";

            case 38: return "Japan sells more adults diapers than baby diapers. The country’s population is on the decline and people over the age of 46 outnumber younger Japanese.";

            case 39: return "Every year the river Vilnia is dyed bright emerald green for St. Patrick’s Day. The idea originated in Chicago in the sixties, when Irish plumbers discovered an entirely harmless way of dyeing water.";

            case 40: return "Luxembourg is one of the safest countries in the world. According to a UN survey, you have less chance of being shot in Luxembourg than in any other country in the world. There are around 1,300 police and just two jails in Luxembourg.";

            case 41: return "Ranavalona (popularly known as the mad queen). She thwarted European efforts to gain sway over Madagascar during her 33-year rule, but also focused her energies on brutally eradicating Christians, neighbouring kingdoms and political rivals.";

            case 42: return "Almost half of Mali’s population lives below the international poverty line. Mali is one of the 25 poorest countries in the world. The average annual salary of a Malian is $1,500 (U.S. dollars) annually.";

            case 43: return "Mauritius is a volcanic island that first rose above the waves eight million years ago. In 2017, Mauritius was named one of only four countries in the world which had no involvement in ongoing international or domestic conflict and no tensions with neighbouring countries.";

            case 44: return "Almost 30% of the population of Monaco was a millionaire in 2014, similar to Zurich or Geneva. Monaco does not have its own major defense force. The country’s defense; however, is France’ responsibility.";

            case 45: return "The Dutch were the first in the world to legalise gay marriage: same-sex marriage has been legal in the Netherlands since 2001.";

            case 46: return "While English is the official language, there are over 500 indigenous languages in Nigeria.Seven percent of the total languages spoken in the world are spoken in Nigeria.";

            case 47: return "While English is the official language, there are over 500 indigenous languages in Nigeria.Seven percent of the total languages spoken in the world are spoken in Nigeria.";

            case 48: return "Pakistan is the world’s first Islamic country to attain nuclear power. Pakistan has the world’s largest ambulance network. Pakistan’s Edhi Foundation, which is also listed in the Guinness Book of World Records, operates the network.";

            case 49: return "Sana’a is the capital and largest city Yemen. It is one of the oldest continuously inhabited cities in the world. At an elevation of 2300 metres(7, 500 ft), it is also one of the highest capital cities in the world. Yemen’s territory includes more than 200 islands.";

            case 50: return "Peruvians celebrate New Year’s by gifting one another yellow underpants on New Year’s Eve to bring good luck in the coming year. It is tradition to wear them inside out (underneath clothing) until midnight, then flip them around at the stroke of midnight.";

            case 51: return "In 1853, Ignacy Lukasiewicz introduced the first modern street lamp from in Europe. His lamp inventions were, however, first used in Lviv in Ukraine. There is still a street in Warsaw that uses the same street lamps until today.";

            case 52: return "Qatar’s per capita GDP is $127,600, according to the IMF. That’s some way ahead of Luxembourg, in second place, with $104,003. Having the world's third-largest natural-gas reserves and oil reserves, despite being geographically smaller than Yorkshire, certainly helps.";

            case 53: return "Romania is the fifth booziest country in the world, behind four more Eastern European states: Belarus, Russia, Moldova and Lithuania. As the map below shows, the average Romanian consumes 14.4 litres of pure alcohol each year, compared to 11.6 litres in Britain.";

            case 54: return "There is a restaurant in Moscow staffed entirely by twins. The Twin Stars diner employs identically-dressed siblings and takes inspiration from 1964 Soviet film Kingdom of Crooked Mirrors.";

            case 55: return "The history of Sierra Leone began when the land became inhabited by indigenous African peoples at least 2,500 years ago. The dense tropical rainforest partially isolated the region from other West African cultures, and it became a refuge for peoples escaping violence and jihads.";

            case 56: return "Somalia is located on the outer edge of the Somali Peninsula, which is also known as “the Horn of Africa.";

            case 57: return "Homosexuality is not legal in the Sudan and is a capital offense.";

            case 58: return "Swedes study and work hard but they also take their rest and relaxation seriously. So the fika – a coffee break that normally consists of coffee/tea, cookies/sweet buns, but can also include soft drinks, fruit and sandwiches – is a social institution and an important part of the national culture.";

            case 59: return "You’re lucky that you know Bangkok, Thailand as “Bangkok.” Its real name is : Krungthepmahanakhon Amonrattanakosin Mahintharayutthaya Mahadilokphop Noppharatratchathaniburirom Udomratchaniwetmahasathan Amonphimanawatansathit Sakkathattiyawitsanukamprasit.";

            case 60: return "Saint Nicholas was born far from the North Pole, in Patara. And he’s not the only saint with connections to Turkey — the Virgin Mary’s resting place could be near Ephesus, while Saint Paul was from Tarsus in the south.";

            case 61: return "Bulgaria is the only country in Europe that hasn’t changed its name since it was first established. This happened in 681 AD. Bulgarians shake their heads to mean yes and nod for no.";

            case 62: return "Palau is a beautiful tropical paradise, and one of the true unspoiled destinations on the planet. Geographically, the terrain varies from the high, mountainous main island of Babeldaob to low coral islands usually fringed by large barrier reefs.";


        }
        return "The Sahara Desert covers 90% of the land and with some the hottest temperatures ever recorded, it is not suitable for occupation. As a result of this, only 12% of the country has human communities";
    }
}