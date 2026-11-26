using System;
using Unity.VisualScripting;
using UnityEngine.UI;

[System.Serializable]
public class Quota{
    private Day day {get; set;}
    private Button button {get; set;}
    private string buttonText {get; set;}
    public Quota(Day day, Button button, string buttonText)
    {
        this.day = day;
        this.button = button;
        this.buttonText = buttonText;
    }
}