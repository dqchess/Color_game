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
    private Sprite[] flags_original;
    private Sprite[] flags_editable;


    public void LoadFlags()
    {

        flags_original = new Sprite[max_level];
        flags_editable = new Sprite[max_level];

        for (int i = 0; i < max_level; ++i)
        {
            string original = "Flags/" + GetCountryForLevel(i) + "/" + GetCountryForLevel(i) + "_original";
            flags_original[i] = Resources.Load<Sprite>(original);

            string bw = "Flags/" + GetCountryForLevel(i) + "/" + GetCountryForLevel(i) + "_bw";
            flags_editable[i] = Resources.Load<Sprite>(bw);

        }
    }
    public int GetCurrentLevel()
    {
        return current_level;
    }
	void Start () {
        LoadFlags();
        current_level = starting_level;
        Load_level_From_Memory(current_level);
	}
    void Load_level_From_Memory(int level)
    {

        original_gb.GetComponent<Image>().sprite = flags_original[level];
        GameObject.Find("EditableMap").GetComponent<Image>().sprite = flags_editable[level];
        GameObject.Find("Color_Object").GetComponent<InitMap>().Initiate();



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

    public void ResetFlag(){
        Load_level(current_level);
        GameObject.Find("Color_Object").GetComponent<InitMap>().Init();
    }

    public void Load_next_level(){

        GameObject.Find("Color_Object").GetComponent <InitMap>().Reset();
        current_level++;

        if (current_level > max_level){
            current_level = 1;
        }

        //Load_level(current_level);
        Load_level_From_Memory(current_level);
        GameObject.Find("Color_Object").GetComponent<InitMap>().Init();
    }


    public string  GetCountryForLevel(int level){
        switch(level){
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
            case 12: return "Bulgaria";
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
            case 61: return "Ukrain";
            case 62: return "Palau";


        }
        return "Algeria";
    }



    // Update is called once per frame
    void Update () {
		
	}
}
