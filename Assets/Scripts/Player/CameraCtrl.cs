using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

    public float cameraZoom = 2f;
    public float smoothness = 10f;
    public float MaxZoom;

    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            StartCoroutine("MouseZoomIn");
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            StartCoroutine("MouseZoomOut");
        }


    }
    IEnumerator MouseZoomIn()
    {
        while (Camera.main.orthographicSize > cameraZoom)
        {
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - Mathf.Lerp(0.2f,1f, Time.deltaTime / smoothness), 2f);

            yield return null;
        }
        if (cameraZoom > 2)
            cameraZoom -= 1;
        yield return null;

    }

    IEnumerator MouseZoomOut()
    {
        while(Camera.main.orthographicSize < cameraZoom)
        {
           
        Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize + Mathf.Lerp(0.2f, 1f, Time.deltaTime / smoothness), MaxZoom);

            yield return null;
        }
        if (cameraZoom < MaxZoom)
            cameraZoom += 1;
        yield return null;

    }
}
