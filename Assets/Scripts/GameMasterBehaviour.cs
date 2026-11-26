using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameMasterBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject gameArea;
    private GameObject spawnedGameArea;

    private Day day = new Day(0, 0, 0, 0);

    private int earnedCash = 0;

    void Awake()
    {
        AutoloadUnity.gameMaster = gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }

    private void endDay()
    {
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
        day = new Day(0, 0, 0, 0);
        AutoloadUnity.mainMenu.SetActive(true);
    }

    public void updateCash(int amount)
    {
        earnedCash += amount;
    }

    public int getEarnedCash()
    {
        return earnedCash;
    }

    private void lose()
    {
        print("you lose");
        AutoloadUnity.cash += day.loseAmount;
        if(AutoloadUnity.cash < 0) AutoloadUnity.cash = 0;
    }

    private void win()
    {
        print("you win");
        AutoloadUnity.cash += day.winBonus;
    }

    IEnumerator Timer(int time)
    {
        int timePassed = 0;
        bool isNotTime = true;
        while (isNotTime)
        {
            print(time - timePassed);
            yield return new WaitForSeconds(1f);  
            if(timePassed >= time)
            {
                isNotTime = false;
                endDay();
            }
            timePassed++; 
        }
    }
}
