using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour
{
    [field: SerializeField] private Button PowerUpButton { get; set; } 
    [field: SerializeField] private TextMeshProUGUI UsesText { get; set; } 
       
    private int Uses { get; set; }

    public void Init(int uses, Action powerUpAction)
    {
        Uses = uses;

        PowerUpButton.onClick.AddListener(() =>
        {
            if(Uses <= 0)
                return;
            
            Uses--;
            powerUpAction();
            UpdateButton();
        });

        UpdateButton();
    }

    private void UpdateButton()
    {
        PowerUpButton.interactable = Uses > 0;
        UsesText.SetText(Uses.ToString());
    }
}