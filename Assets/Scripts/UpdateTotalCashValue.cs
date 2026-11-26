using UnityEngine;

public class UpdateTotalCashValue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TMPro.TextMeshProUGUI ding = GetComponent<TMPro.TextMeshProUGUI>();
        ding.text = AutoloadUnity.cash.ToString();
    }
}
