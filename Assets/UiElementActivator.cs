using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiElementActivator : MonoBehaviour
{


    public GameObject[] uiElements;
    public Button radarButton;

    public void ActivateAndDisableElements()
    {
            for (int i = 0; i < uiElements.Length; i++)
            {
                uiElements[i].SetActive(!uiElements[i].activeSelf);
            }
    }

public void DisableButton()
    {
        if(radarButton != null)
        radarButton.interactable = false;
    }
    public void EnableButton()
    {
        if (radarButton != null)
            radarButton.interactable = true;
    }
    public void ActivateElements()
    {
        foreach (var item in uiElements)
        {
            item.SetActive(true);
        }
    }
    public void DisableElements()
    {
        foreach (var item in uiElements)
        {
            item.SetActive(false);
        }
    }
}
