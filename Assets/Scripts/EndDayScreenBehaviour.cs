using UnityEngine;
using UnityEngine.UI;

public class EndDayScreenBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject quotaScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Button continueButton;

    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        continueButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            quotaScreen.SetActive(true);
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endDay(int earnedCash, int quota)
    {
        quotaScreen.SetActive(false);
        gameObject.SetActive(true);
        if (earnedCash < quota)
        {
            print("loseScreen");
            loseScreen.gameObject.SetActive(true);
        }
        else
        {
            print("winScreen");
            winScreen.gameObject.SetActive(true);
        }
    }
}
