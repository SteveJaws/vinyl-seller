using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglerBehaviour : MonoBehaviour
{
   [SerializeField] private List<UiButtonToggler> UiButtonTogglerList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(UiButtonToggler buttonToggler in UiButtonTogglerList)
        {
            setup(buttonToggler);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setup(UiButtonToggler buttonToggler)
    {
        buttonToggler.toggler.GetComponent<Button>().onClick.AddListener(() => togglerBehaviour(buttonToggler.toToggle));
    }

    private void togglerBehaviour(GameObject toToggle)
    {
        print("is " + toToggle.gameObject.activeSelf);
        print("to " + !toToggle.gameObject.activeSelf);
        toToggle.gameObject.SetActive(!toToggle.gameObject.activeSelf);
    }
}
