using System.Collections.Generic;
using UnityEngine;

public class QuotaScreenBehaviour : MonoBehaviour
{
    [SerializeField] private List<QuotaScreenButton> quotaScreenButtonList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setupButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setupButtons()
    {
        foreach(QuotaScreenButton button in quotaScreenButtonList)
        {
            button.button.onClick.AddListener(() =>
            {
                AutoloadUnity.gameMaster.GetComponent<GameMasterBehaviour>().startDay(button.day);
                AutoloadUnity.mainMenu.SetActive(false);
            });
        }
    }
}
