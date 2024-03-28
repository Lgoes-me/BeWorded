using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpController : MonoBehaviour
{
    [field: SerializeField] private Button PowerUpButton { get; set; }
    [field: SerializeField] private TextMeshProUGUI UsesText { get; set; }

    private PowerUp PowerUp { get; set; }

    public void Init(PowerUp powerUp, Action powerUpAction)
    {
        PowerUp = powerUp;

        PowerUpButton.onClick.AddListener(() =>
        {
            if (PowerUp.TryUse())
            {
                powerUpAction();
                UpdateButton();
            }
        });

        UpdateButton();
    }
    
    public void UpdateButton()
    {
        PowerUpButton.interactable = PowerUp.Uses > 0;
        UsesText.SetText(PowerUp.Uses.ToString());
    }
}