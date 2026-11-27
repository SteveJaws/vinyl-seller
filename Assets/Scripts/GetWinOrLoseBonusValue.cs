using Unity.IntegerTime;
using UnityEngine;

public class GetWinOrLoseBonusValue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool didUserWin = AutoloadUnity.gameMaster.GetComponent<GameMasterBehaviour>().didUserWin();

        TMPro.TextMeshProUGUI text = GetComponent<TMPro.TextMeshProUGUI>();
        DITTE GAAT NU NOG FOUT ER MOET EEN DAY IN DIT SCRIPT KOMEN ALLEEN DEGEN DIE NIET GERESET NAAR NUL IS
        if (didUserWin)
        {
            print(day.winBonus);
            text.text = day.winBonus.ToString();
        }
        else
        {
            print(day.loseAmount);
            text.text = day.loseAmount.ToString();
        }
    }
}
