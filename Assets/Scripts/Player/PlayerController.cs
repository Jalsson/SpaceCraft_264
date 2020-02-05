using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    PlayerStats playerStats;
     Rigidbody2D rigi2D;

    public float turnSpeed;
    public int playerimpactDamage;
    public int damageResistance;

    public GameObject[] uiElements;


    private void Start()
    {
        rigi2D = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        rigi2D.angularVelocity = 45;
        rigi2D.angularDrag = 0;
        rigi2D.angularDrag = 0.8f;
    }




    void FixedUpdate()
    {

        if (GameModeManager.instance.gameStarted)
        {
            if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
                rigi2D.angularVelocity -= turnSpeed;

            if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
                rigi2D.angularVelocity += turnSpeed;
        }


        var headingValue = Mathf.RoundToInt(transform.eulerAngles.z) - 360;
        headingValue = -headingValue;
        PlayerStats.instance.playerStats[3].text = headingValue.ToString();

    }

    private void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {
            if (GameModeManager.instance.gameStarted)
            {
                if (!GameModeManager.instance.menuOn)
                {
                    Time.timeScale = Time.timeScale == 1 ? Time.timeScale = 0 : Time.timeScale = 1;
                    if (Time.timeScale == 0)
                    {
                        playerStats.ShowMouse();
                        playerStats.paused = true;
                    }
                    else
                    {
                        playerStats.HideMouse();
                        playerStats.paused = false;
                    }
                }
            }
        }

        var angularvelocity = Mathf.RoundToInt(rigi2D.angularVelocity);
        angularvelocity = -angularvelocity;
        PlayerStats.instance.playerStats[4].text = angularvelocity.ToString();
        PlayerStats.instance.playerStats[2].text = rigi2D.velocity.magnitude.ToString();
    }

    public void ToggleInvenotory()
    {
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(!uiElements[i].activeSelf);
            GetComponent<GetShipComponents>().inventoryOn = uiElements[0].activeSelf;
            uiElements[2].SetActive(false);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        int shipRelativeVelocity = Mathf.RoundToInt(collision.relativeVelocity.magnitude);
        float shipVelocity = GetComponent<Rigidbody2D>().velocity.magnitude;
        if (collision.transform.tag == "BigAsteroid")
        {

            if (shipRelativeVelocity >= 4 && shipVelocity > 3)
            {
                playerStats.ChangeHealth(playerimpactDamage * shipRelativeVelocity / damageResistance);
            }
            

        }
        if (collision.transform.tag != "Collectable" && collision.transform.tag != "PlayerProjectile")
        {


            if (collision.gameObject.GetComponent<AsteroidDestroyer>() != null)
            {

                if (shipRelativeVelocity >= 5 && shipVelocity > 3)
                {
                    playerStats.ChangeHealth(playerimpactDamage * shipRelativeVelocity / damageResistance);
                }


            }
            if (collision.gameObject.GetComponent<AsteroidDestroyer>() == null)
            {

                if (shipRelativeVelocity >= 7 && shipVelocity > 3)
                {
                    playerStats.ChangeHealth(playerimpactDamage * shipRelativeVelocity / damageResistance);
                }


            }
        }
    }


    
}
