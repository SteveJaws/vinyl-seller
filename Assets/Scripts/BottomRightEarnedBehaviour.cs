using UnityEngine;

public class BottomRightEarnedBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TMPro.TextMeshProUGUI text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = AutoloadUnity.gameMaster.GetComponent<GameMasterBehaviour>().getEarnedCash().ToString() + " / " + AutoloadUnity.gameMaster.GetComponent<GameMasterBehaviour>().getDay().quota.ToString();
    }
}
