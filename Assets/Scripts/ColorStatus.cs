using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorStatus : MonoBehaviour {

    static public Color current_color = Color.white;
    static public float flood_fill_tolerance = 0.3f;
    // Use this for initialization
    void Start () {

        GameObject selected_color = GameObject.Find("selected_color");
        selected_color.GetComponent<Image>().color = ColorStatus.current_color;
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = ColorSprite.current_color;
            GetComponent<AudioSource>().Play();

        }

    }

    public void OnClicked(Button button)
    {
        //GameObject btn = GameObject.Find(button.name);
        var color = button.GetComponent<Image>().color;
        ColorStatus.current_color = color;
        GameObject selected_color = GameObject.Find("selected_color");
        selected_color.GetComponent<Image>().color = ColorStatus.current_color;
        GetComponent<AudioSource>().Play();
        print(button.name);
        Debug.Log("Object clicked " + ColorSprite.current_color);
    }

       public void OnNextScene(Button button)
    {
        string next_scene = (button.name);
        SceneManager.LoadScene(next_scene);
    }
}
