﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InitMap : MonoBehaviour {

    public GameObject original_map;
    public GameObject editable_map;
    public int min_pixels_per_color;
    public int min_matching_percentage;

    public Sprite Initial_State;
    private Texture2D Initial_Texture;

    // Use this for initialization
    public void Initiate () {
        Init();
        Reset();
        

    }

    void SetColorButtons()
    {

        GameObject color_btn = GameObject.Find("Color1");
        GameObject parent = GameObject.Find("Colors");
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Transform btn_transform = color_btn.transform;
        Texture2D t1 = TextureExtension.textureFromSprite(original_map.GetComponent<Image>().sprite);
        Color[] colors_comparable = TextureExtension.GetMainColorsFromTexture2(t1,min_pixels_per_color);

        Debug.Log("Colors found in original image:" + colors_comparable.Length);
        
        for (int i = 0; i < colors_comparable.Length; ++i)
        {
   

            //string button_name = "Color" + (i+1).ToString();
            //GameObject btn = GameObject.Find(button_name);
            GameObject colorbtn = Instantiate(color_btn);
            colorbtn.GetComponent<Image>().color = colors_comparable[i];// colors_comparable[i].ToColor32();
            Debug.Log(colors_comparable[i]);
            colorbtn.transform.SetParent(parent.transform);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowHideHint()
    {

        original_map.SetActive(true);
        editable_map.SetActive(false);
        StartCoroutine("FlashHint");
    }

    public void Init(){
        original_map.SetActive(false);
        editable_map.SetActive(true);
        LevelManager levelmgr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        GameObject.Find("CountryName").GetComponent<Text>().text = levelmgr.GetCountryForLevel(levelmgr.GetCurrentLevel());
        SetColorButtons();

        Initial_Texture = new Texture2D((int)editable_map.GetComponent<Image>().sprite.rect.width, (int)editable_map.GetComponent<Image>().sprite.rect.height,
            TextureExtension.textureFromSprite(editable_map.GetComponent<Image>().sprite).format, false);
        Graphics.CopyTexture(TextureExtension.textureFromSprite(editable_map.GetComponent<Image>().sprite), Initial_Texture);
        Initial_Texture.Apply();

    }
    public void Reset()
    {

        editable_map.GetComponent<Image>().sprite = Sprite.Create(Initial_Texture, new Rect(0.0f, 0.0f, Initial_Texture.width, Initial_Texture.height), new Vector2(0.0f, 0.0f));
        Initial_Texture.Apply();
    }

    public  bool AreImagesMatching(Texture2D current_texture)
    {
        Texture2D t1 = TextureExtension.textureFromSprite(original_map.GetComponent<Image>().sprite);
        //Texture2D t2 = TextureExtension.textureFromSprite(editable_map.GetComponent<Image>().sprite);

        return (TextureExtension.AreTexturesSameByColor(t1, current_texture, min_matching_percentage));
        
    }

    IEnumerator FlashHint()
    {
        //AreImagesMatching();
        int current_time = 0;
        while (current_time < 3 )
        {
            current_time++;
            yield return new WaitForSeconds(1f);
        }
        original_map.SetActive(false);
        editable_map.SetActive(true);


    }
}