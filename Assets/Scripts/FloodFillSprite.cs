using ProtoTurtle.BitmapDrawing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloodFillSprite : MonoBehaviour, IPointerDownHandler
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public GameObject editable_map;
    public GameObject original_map;
    public GameObject particles;
    public Texture2D tex;
    public AudioSource audio1;
    public AudioSource audio2;

    // Use this for initialization
    void Start() {
        //Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        //Texture2D texture = TextureExtension.textureFromSprite(sprite);
        //texture.FloodFillArea(80, 80, Color.red);
        //texture.Apply();


    }
   
    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        tex = TextureExtension.textureFromSprite(editable_map.GetComponent<Image>().sprite);
        Rect r = editable_map.GetComponent<RectTransform>().rect;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(editable_map.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        int px = Mathf.Clamp(0, (int)(((localPoint.x - r.x) * tex.width) / r.width), tex.width);
        int py = Mathf.Clamp(0, (int)(((localPoint.y - r.y) * tex.height) / r.height), tex.height);
        Color32 col = tex.GetPixel(px, py);
        Color32 color32 = tex.GetPixel(px, py);
        audio2.Play();
        //Debug.Log("(LocalPoint X/Y:" + localPoint.x + ", " + localPoint.y + ")  ");
        //Debug.Log("(Rect X/Y:" + r.x + ", " + r.y + ")  ");
        //Debug.Log("(Rect W/H:" + r.width + ", " + r.height + ")  ");
        //Debug.Log("(Pixel X/Y:" + px + ", " + py + ")  ");
        //Debug.Log("(Texture W/H:" + tex.width + ", " + tex.height + ")  ");



        tex.FloodFillAreaWithTolerance(px, py, ColorStatus.current_color,ColorStatus.flood_fill_tolerance);
        //texture.DrawRectangle(new Rect(px, (tex.height - py), 20, 20), Color.red);
        tex.Apply();
        if (gameObject.GetComponent<InitMap>().AreImagesMatching(tex))
        {
            original_map.SetActive(true);
            //editable_map.SetActive(false); //TODO Right Now its a hack in the layer it is pasted

            //var sprite = Resources.Load<Sprite>("done");
            audio1.Play();
            //editable_map.GetComponent<Image>().sprite = sprite;
            //Debug.Log("Yohooooo");
            Button next_btn = GameObject.Find("Next_button").GetComponent<Button>();
            next_btn.interactable = true;

            LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            levelManager.boTimerActive = false; //Disabling Timer TODO - Better to create a delegate here 
            particles.SetActive(true);
        }
        //editable_map.GetComponent<Image>().sprite = tex;
    }


}
