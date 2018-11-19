using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {


    public int starting_level;
    private int current_level;
    public int max_level;
    public GameObject original_gb;
	// Use this for initialization
    public int GetCurrentLevel()
    {
        return current_level;
    }
	void Start () {
        current_level = starting_level;
        Load_level(current_level);
	}

    void Load_level(int level){

        //Replace sprites
        //original image
        string original = "Flags/"+GetCountryForLevel(current_level) +"/"+ GetCountryForLevel(current_level) + "_original";
        Sprite original_flag = Resources.Load<Sprite>(original);
        original_gb.GetComponent<Image>().sprite = original_flag;

        string bw = "Flags/" + GetCountryForLevel(current_level) + "/" + GetCountryForLevel(current_level) + "_bw";
        Sprite bw_flag = Resources.Load<Sprite>(bw);
        GameObject.Find("EditableMap").GetComponent<Image>().sprite = bw_flag;
        GameObject.Find("Color_Object").GetComponent<InitMap>().Initiate();



    }

    public void Load_next_level(){

        GameObject.Find("Color_Object").GetComponent <InitMap>().Reset();
        current_level++;

        if (current_level > max_level){
            current_level = 1;
        }

        Load_level(current_level);
        GameObject.Find("Color_Object").GetComponent<InitMap>().Init();
    }


    public string  GetCountryForLevel(int level){
        switch(level){
           case 1: return "algeria";
           case 2: return "austria";
            case 3: return "armenia";
            case 4: return "azerbaijan";
            case 5: return "bahamas";
            case 6: return "bahrain";
            case 7: return "bangladesh";
            case 8: return "belgium";
            case 9: return "benin";
            case 10: return "bolivia";
            case 11: return "botswana";
            case 12: return "bulgaria_flag";
            case 13: return "burkina_faso";
            case 14: return "cameroon";
            case 15: return "chad";
            case 16: return "chile";
            case 17: return "colombia";
            case 18: return "congo_republic";
            case 19: return "costa_rica";
            case 20: return "cote_d_ivoire";
            case 21: return "czech_republic";
            case 22: return "denmark";
            case 23: return "estonia";
            case 24: return "finland";
            case 25: return "france";
            case 26: return "gabon";
            case 27: return "gambia";
            case 28: return "georgia";
            case 29: return "germany";
            case 30: return "ghana";
            case 31: return "guinea";
            case 32: return "guinea_bissau";
            case 33: return "hungary";
            case 34: return "iceland";
            case 35: return "indonesia";
            case 36: return "ireland";
            case 37: return "italya";
            case 38: return "japan";
            case 39: return "lithuania";
            case 40: return "luxembourg";
            case 41: return "madagascar";
            case 42: return "mali";
            case 43: return "mauritius";
            case 44: return "monaco";
            case 45: return "netherlands";
            case 46: return "nigera";
            case 47: return "nigeria";
            case 48: return "pakistan";
            case 49: return "yemen";
            case 50: return "peru";
            case 51: return "poland";
            case 52: return "qatar";
            case 53: return "romania";
            case 54: return "russia";
            case 55: return "sierra_leone";
            case 56: return "somalia";
            case 57: return "sudan";
            case 58: return "sweden";
            case 59: return "thailand";
            case 60: return "turkey";
            case 61: return "ukrain";
            case 62: return "palau";


        }
        return "algeria";
    }



    // Update is called once per frame
    void Update () {
		
	}
}
