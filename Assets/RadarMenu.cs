using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarMenu : MonoBehaviour {

    public Camera radarCamera;
    Rect fullScreenRect = new Rect(0, 0.03f, 1, 0.94f);
    Rect normalSizeRect;

        private void Start()
    {
        normalSizeRect = radarCamera.rect;
    }

    public void ActivateRadarMenu()
    {
        radarCamera.rect = fullScreenRect;
    }

    public void DisableRadarMenu()
    {
        radarCamera.rect = normalSizeRect;
    }


}
