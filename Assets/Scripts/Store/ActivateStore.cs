using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ActivateStore : MonoBehaviour {

    public TextMeshProUGUI interactionText;
    public GameObject[] storeInterface;
    

    private PlayerController playerController;
    private GameObject[] uiElements;
    bool playerInRadius;

    private void Start()
    {
        playerController = PlayerStats.instance.gameObject.GetComponent<PlayerController>();
        uiElements = playerController.uiElements;
        interactionText.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactionText.enabled = true;
            playerInRadius = true;
        }

    }

    private void Update()
    {
        if (playerInRadius)
        {

            if (Input.GetButtonDown("Interact"))
            {
                for (int i = 0; i < storeInterface.Length; i++)
                {
                    storeInterface[i].SetActive(!storeInterface[i].activeSelf);
                    interactionText.gameObject.SetActive(!interactionText.gameObject.activeSelf);

                }
            }
            
        }
       
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < storeInterface.Length; i++)
            {
                if (storeInterface[i] != null)
                {
                    storeInterface[i].SetActive(!storeInterface[i].activeSelf);
                    interactionText.gameObject.SetActive(!interactionText.gameObject.activeSelf);

                }

            }
            interactionText.gameObject.SetActive(true);
            interactionText.enabled = false;
            playerInRadius = false;
        }
    }
}
