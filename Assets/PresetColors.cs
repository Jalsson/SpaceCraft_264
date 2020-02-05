using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetColors : MonoBehaviour {

    public Image colorPreview;
    public Image thisImage;
    public RGBColor rgbColor;

    public void InstanceThisImage()
    {
        rgbColor.currentPreviewImage = thisImage;
    }
}
