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
    public Texture2D tex;

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
            var sprite = Resources.Load<Sprite>("done");
            GetComponent<AudioSource>().Play();
            editable_map.GetComponent<Image>().sprite = sprite;
            Debug.Log("Yohooooo");
        }
        //editable_map.GetComponent<Image>().sprite = tex;
    }


}
