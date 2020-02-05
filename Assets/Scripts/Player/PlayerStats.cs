using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class PlayerStats : MonoBehaviour {

    #region instance and Wake func
    public static PlayerStats instance;
    private void Awake()
    {
        instance = this;
        health = maxHealth;

    }
    #endregion

    public delegate void ScoreChanger();
    public ScoreChanger scoreChanger;

    public delegate void HealthChanger();
    public HealthChanger healthChanger;
    public List<TextMeshProUGUI> playerStats;

    public int score { get; private set; }
    public Canvas mainCanvas;
    public TextMeshProUGUI infoText;
    public Slider healthSlider;
    public Slider fuellSlider;
    public Slider thrusterSlider; 
    public TextMeshProUGUI thrusterInfoText;

    public int maxHealth;
    public float health { get; private set; }
    public float thrusterPower;
    float fuel;
    public bool noFuel;

    public bool paused = false;
    bool cursorNotOn = false;

    bool dead;
    public bool damaged = false;

            int secondsPast;
        int minutesPast;

    private void Start()
    {
        fuel = 100;
        scoreChanger += UpdateStats;
        healthChanger += ChangeHealthvalue;
        
        scoreChanger();
        healthChanger();
        thrusterSlider.value = 0;
        StartCoroutine(PlayingTime());
        UpdateFuelAmount(0);
        UpdateThrottle();
    }

    public void ChangeHealth(int ChangeAmount)
    {
        damaged = true;
        health -= ChangeAmount;
        AddTextToLog.instance.AddTextToEventLog("Hull took " + ChangeAmount.ToString() + " points Damage");
        healthChanger();
        if (health <= 0 && !dead)
        {
            Die();
        }

    }

    public void ChangeScore(int addedScore)
    {
        score += addedScore;
        scoreChanger();
    }
    
    void UpdateStats()
    {
        playerStats[0].text = score.ToString();
        playerStats[1].text = GetComponent<GetShipComponents>().Ammo.ToString();
    }

    void ChangeHealthvalue()
    {
        healthSlider.value = health / 100;
        if(health <= 50 && health > 40)
        {
            AddTextToLog.instance.AddTextToEventLog("hull hitpoints under 50% ");
        }
        if (health < 30)
        {
            StartCoroutine(healthSlider.GetComponent<ColorChanger>().BlinkColor());
            AddTextToLog.instance.AddTextToEventLog("hull critically damaged");
        }
    }

    public void UpdateThrottle()
    {

        thrusterPower = thrusterSlider.value * 1.2f;

        var thrusterpowerFloat = Mathf.Round(thrusterPower * 100);
        thrusterInfoText.text = thrusterpowerFloat.ToString();
        playerStats[6].text = thrusterpowerFloat.ToString();
    }

    public void UpdateFuelAmount(float fuelConsumptionRate)
    {
        if (thrusterPower > 100)
        {
            fuel += -fuelConsumptionRate * 2 * thrusterPower * Time.deltaTime ;
            fuellSlider.value = fuel * 0.01f;
        }
        else
        {
            fuellSlider.value = fuel * 0.01f;
            fuel += -fuelConsumptionRate * thrusterPower * Time.deltaTime;
        }

        if (fuel == 50)
        {
            AddTextToLog.instance.AddTextToEventLog("fuel tanks are only 50% full");
        }
        if (fuel == 20)
            AddTextToLog.instance.AddTextToEventLog("fuel tanks dangerously low");

        if (fuel <0)
            noFuel = true;

        playerStats[5].text = Mathf.RoundToInt(fuel).ToString();
    }


    public virtual void Die()
    {
        
        StartCoroutine(GameModeManager.instance.UpdateInfoText("you died restarting level in 4 seconds"));
        dead = true;
        gameObject.SetActive(false);
        Invoke("ReloadScene", 4);
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    IEnumerator PlayingTime()
    {
        secondsPast++;

        if (secondsPast == 60)
            minutesPast++;

        playerStats[7].text = minutesPast.ToString() + ":" + secondsPast.ToString();
        yield return new WaitForSeconds(1);
        StartCoroutine( PlayingTime());
    }

    public void MouseShowToggle()
    {
        Cursor.lockState = cursorNotOn ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = cursorNotOn ? Cursor.visible = false : Cursor.visible = true;
        cursorNotOn = cursorNotOn ? cursorNotOn = false : cursorNotOn = true;
    }
    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorNotOn = true;
    }

    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorNotOn = false;
    }
}