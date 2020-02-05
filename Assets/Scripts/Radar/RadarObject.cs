using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarObject : MonoBehaviour {

    public bool inRadarDisplayArea = false;
    Color32 normalColor;
    Color32 notificationColor = new Color32(255, 0, 0,255);
    Color32 markerColor = new Color32(0, 255, 0, 255);
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;
        
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void HighLightColor()
    {
        spriteRenderer.color = notificationColor;
    }
    public void NormalColor()
    {
        spriteRenderer.color = normalColor;
    }
    public void MarkerColor()
    {
        spriteRenderer.color = markerColor;
    }
}
