using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameMasterBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject gameArea;
    private GameObject spawnedGameArea;

    private Day day = new Day(10, 10, 50, -50);

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
        inputHandler();
    }

    private void inputHandler()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(spawnedGameArea != null) return;
            startDay();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if(spawnedGameArea == null) return;
            Destroy(spawnedGameArea);
            spawnedGameArea = null;
        }
    }

    private void startDay()
    {
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
