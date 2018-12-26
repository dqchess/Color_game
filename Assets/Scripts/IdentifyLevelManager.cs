using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IdentifyLevelManager : MonoBehaviour {


    public GameObject flag_img;
    private string correct_btn_text;
    private int score = 0;
    private bool attempted = false;
    private int current_level = 0;
    public GameObject popup;
    public Button btn_redo;
    public Button btn_home;


    // Use this for initialization
    void Start () {
        current_level = 1;
        LoadLevelForIdentifyFlag(current_level);
        setupButtonListeners();
        score = 0;
        setScore(score);
        
    }
    void setupButtonListeners() {
        for (int i = 1; i <= 4; ++i)
        {

            Button btn = GameObject.Find("option" + i).GetComponent<Button>();
            btn.onClick.AddListener(delegate { CheckButtonClicked(btn); });

        }
        Button btn_next = GameObject.Find("Next").GetComponent<Button>();
        btn_next.onClick.AddListener(delegate { CheckButtonClicked(btn_next); });


        btn_redo.onClick.AddListener(delegate { CheckButtonClicked(btn_redo); });
        btn_home.onClick.AddListener(delegate { CheckButtonClicked(btn_home); });
    }

    void enableButtons(bool enable)
    {
        for (int i = 1; i <= 4; ++i)
        {

            Button btn = GameObject.Find("option" + i).GetComponent<Button>();
            btn.interactable = enable;

        }
     
    }


    // Update is called once per frame
    void Update () {
      
    }

    void setScore(int score)
    {
        GameObject.Find("ScoreText").GetComponent<Text>().text = "" + score;
    }

    void LoadLevelForIdentifyFlag(int level_no)
    {
        attempted = false;
        GameObject.Find("Next").GetComponent<Button>().interactable = false;
        enableButtons(true);
        string original = "Flags/" + "Resized/" + GetCountryForLevel(level_no) + "_original";
        Sprite original_flag = Resources.Load<Sprite>(original);
        flag_img.GetComponent<Image>().sprite = original_flag;
        setButtonText(level_no);
    }

    void setButtonText(int level)
    {
        int random_num_for_correct_button = Random.Range(1, 4);
        correct_btn_text = "option" + random_num_for_correct_button;
        
        Button btn_correct = GameObject.Find(correct_btn_text).GetComponent<Button>();
        btn_correct.GetComponent<Image>().color = Color.white;

        string correct_txt = "text" + random_num_for_correct_button;
        GameObject.Find(correct_txt).GetComponent<Text>().text = GetCountryForLevel(level);
        for(int i = 1; i <=4; ++i)
        {
            
            if (i == random_num_for_correct_button)
                continue;
            GameObject.Find("text" + i).GetComponent<Text>().text = GetCountryForLevel((level + (i * 5)) % 62);
            Button btn = GameObject.Find("option" + i).GetComponent<Button>();
            btn.GetComponent<Image>().color = Color.white;

        }
    }

    void CheckButtonClicked(Button btn)
    {
        
        string btn_name = btn.name;
        if(btn_name == "redo_btn")
        {
            current_level = 1;
            popup.SetActive(false);
            LoadLevelForIdentifyFlag(current_level);
            score = 0;
            setScore(score);
            return;
        }
        if (btn_name == "home_btn")
        {
         
            SceneManager.LoadScene("Menu");
        }
        if (btn_name == "Next")
        {
            if(current_level == 62)
            {
                popup.SetActive(true);

            }
            LoadLevelForIdentifyFlag(++current_level);
            return;
        }
        Debug.Log("Button clicked " + btn_name);

        if (attempted == false)
        {
            //Output this to console when the Button2 is clicked
            if (btn_name == correct_btn_text)
            {
                GameObject.Find(correct_btn_text).GetComponent<Image>().color = Color.green;

                setScore(++score);

            }
            else
            {
                GameObject.Find(btn_name).GetComponent<Image>().color = Color.red;
                GameObject.Find(correct_btn_text).GetComponent<Image>().color = Color.green;

            }
            attempted = true;
            GameObject.Find("Next").GetComponent<Button>().interactable = true;
            enableButtons(false);
        }
        
    }


    public string GetCountryForLevel(int level)
    {
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
}
