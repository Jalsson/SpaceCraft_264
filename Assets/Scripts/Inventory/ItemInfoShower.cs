using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ItemInfoShower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject InfoPanel;
    public TextMeshProUGUI infoPanelText;
    public String InfoText;
    bool cursorOnTarget;
    public float infoPanelDelay = 0.5f;

    private void Start()
    {
        InfoPanel.SetActive(false);
        infoPanelText = InfoPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ShowInfoPanel());
        cursorOnTarget = true;
    }


    IEnumerator ShowInfoPanel()
    {
        yield return new WaitForSecondsRealtime(infoPanelDelay);


            if (cursorOnTarget)
            {
                InfoPanel.SetActive(true);
            }

        infoPanelText.text = InfoText;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var cameraZoom = Mathf.RoundToInt(Camera.main.orthographicSize);


        switch (cameraZoom)
        {
            case 2:
                mousePosition.y = mousePosition.y < -5.5f ? mousePosition.y += 0.5f : mousePosition.y -= 0.5f;
                mousePosition.x = mousePosition.x < 10 ? mousePosition.x += 0.6f : mousePosition.x -= 0.6f;
                break;
            case 3:
                mousePosition.y = mousePosition.y < -5.5f ? mousePosition.y += 0.8f : mousePosition.y -= 0.8f;
                mousePosition.x = mousePosition.x < 9.5 ? mousePosition.x += 1f : mousePosition.x -= 1f;
                break;
            case 4:
                mousePosition.y = mousePosition.y < -5.5f ? mousePosition.y += 1f : mousePosition.y -= 1f;
                mousePosition.x = mousePosition.x < 9.5 ? mousePosition.x += 1.3f : mousePosition.x -= 1.3f;
                break;

            case 5:
                mousePosition.y = mousePosition.y < -5.5f ? mousePosition.y += 1.2f : mousePosition.y -= 1.2f;
                mousePosition.x = mousePosition.x < 9.5 ? mousePosition.x += 1.7f : mousePosition.x -= 1.7f;
                break;
            case 6:
                mousePosition.y = mousePosition.y < -5.5f ? mousePosition.y += 1.4f : mousePosition.y -= 1.4f;
                mousePosition.x = mousePosition.x < 9.5 ? mousePosition.x += 2f : mousePosition.x -= 2f;

                break;
            case 7:
                mousePosition.y = mousePosition.y < -5.5f ? mousePosition.y += 1.6f : mousePosition.y -= 1.6f;
                mousePosition.x = mousePosition.x < 9.5 ? mousePosition.x += 2.4f : mousePosition.x -= 2.4f;
                break;
            case 8:
                mousePosition.y = mousePosition.y < -5.5f ? mousePosition.y += 1.9f : mousePosition.y -= 1.9f;
                mousePosition.x = mousePosition.x < 9.5 ? mousePosition.x += 2.6f : mousePosition.x -= 2.6f;

                break;
            default:
                mousePosition.y = mousePosition.y < -5.5f ? mousePosition.y += 2.5f : mousePosition.y -= 2.5f;
                mousePosition.x = mousePosition.x < 9.5 ? mousePosition.x += 2.8f : mousePosition.x -= 2.8f;

                break;
        }

        mousePosition.z = 0;


        InfoPanel.transform.position = mousePosition;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorOnTarget = false;
        StartCoroutine(CloseInfoPanel());
        StopCoroutine("ShowInfoPanel");
    }

    IEnumerator CloseInfoPanel()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        InfoPanel.SetActive(false);
    }
}
