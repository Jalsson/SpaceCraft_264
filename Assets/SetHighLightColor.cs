using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHighLightColor : MonoBehaviour {

    public Color highLightColor;
    public Color normalColor;

    public Image targetImage;

    public void ChangeHighLightColor()
    {
        targetImage.color = highLightColor;
    }
    public void NormalColor()
    {
        targetImage.color = normalColor;
    }
    
}
