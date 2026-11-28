using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject gameArea;
    private GameObject spawnedGameArea;

    private Day day = new Day(0, 0, 0, 0);

    private int earnedCash = 0;

    [SerializeField] private GameObject loseValue;
    [SerializeField] private GameObject winBonusValue;

    [SerializeField] private Button shopButton;
    [SerializeField] private GameObject goalText;

    private GameObject clockHand;
 
    void Awake()
    {
        AutoloadUnity.gameMaster = gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shopButton.gameObject.SetActive(false);
        goalText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startDay(Day dayParam)
    {
        day = dayParam;
        earnedCash = 0;
        spawnedGameArea = Instantiate(gameArea);
        spawnedGameArea.transform.position = transform.position;
        StartCoroutine(Timer(day.time));
        clockHand = spawnedGameArea.GetComponent<GameAreaBehaviour>().getClockHand();
        shopButton.gameObject.SetActive(true);
        goalText.gameObject.SetActive(true);
    }

    public Day getDay()
    {
        return day;
    }

    public bool didUserWin()
    {
         if(earnedCash < day.quota)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void endDay()
    {
        shopButton.gameObject.SetActive(false);
        goalText.gameObject.SetActive(false);
        if(earnedCash < day.quota)
        {
            lose();
        }
        else
        {
            win();
        }
        Destroy(spawnedGameArea);
        spawnedGameArea = null;
        AutoloadUnity.mainMenu.GetComponent<MainMenuBehaviour>().endDay(earnedCash, day.quota);
        AutoloadUnity.mainMenu.SetActive(true);
        day = new Day(0, 0, 0, 0);
    }

    public void updateCash(int amount)
    {
        earnedCash += amount;
    }

    public void setCash(int amount)
    {
        earnedCash = amount;
    }

    public int getEarnedCash()
    {
        return earnedCash;
    }

    private void lose()
    {
        AutoloadUnity.cash += day.loseAmount;
        if(AutoloadUnity.cash < 0) AutoloadUnity.cash = 0;
        TMPro.TextMeshProUGUI text = loseValue.GetComponent<TMPro.TextMeshProUGUI>();
        text.text = day.loseAmount.ToString();
    }

    private void win()
    {
        AutoloadUnity.cash += day.winBonus;
        TMPro.TextMeshProUGUI text = winBonusValue.GetComponent<TMPro.TextMeshProUGUI>();
        text.text = day.winBonus.ToString();
    }

    IEnumerator Timer(int time)
    {
        int timePassed = 0;
        bool isNotTime = true;
        while (isNotTime)
        {
            print(time - timePassed);
            yield return new WaitForSeconds(1f);  
            float angle = ((float)timePassed + 1) / time * 360f;
            clockHand.transform.localRotation = Quaternion.Euler(0, 0, -angle);
            if(timePassed >= time)
            {
                isNotTime = false;
                endDay();
            }
            timePassed++; 
        }
    }
}
