using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBColor : MonoBehaviour {

    public Image colorPreviewImage;
    public Slider[] RGBSliders;
    public Image[] fillAreaColors;
    public Image currentPreviewImage;



    public void ChangeColorSlider()
    {
        colorPreviewImage.color = new Color(RGBSliders[0].value , RGBSliders[1].value , RGBSliders[2].value );
        currentPreviewImage.color = new Color(RGBSliders[0].value, RGBSliders[1].value, RGBSliders[2].value);
    }

    public void ChangeSliderColor()
    {
        fillAreaColors[0].color = new Color(RGBSliders[0].value, 0, 0);
        fillAreaColors[1].color = new Color(0, RGBSliders[1].value, 0);
        fillAreaColors[2].color = new Color(0, 0, RGBSliders[2].value);
    }

    public void ChangeSliderValues()
    {
        colorPreviewImage.color = currentPreviewImage.color;

        var redValue = colorPreviewImage.color.r;
        var greenValue = colorPreviewImage.color.g;
        var blueValue = colorPreviewImage.color.b;

        RGBSliders[0].value = redValue;
        RGBSliders[1].value = greenValue;
        RGBSliders[2].value = blueValue;
    }
}
