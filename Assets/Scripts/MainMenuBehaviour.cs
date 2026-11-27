using System;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject endDayScreen;
    void Awake()
    {
        AutoloadUnity.mainMenu = gameObject;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endDay(int earnedCash, int quota)
    {
        endDayScreen.GetComponent<EndDayScreenBehaviour>().endDay(earnedCash, quota);
    }
}
