using System.Threading.Tasks;
using UnityEngine;

public class AlertManager : BaseManager
{
    [field: SerializeField] private LanguageSelectionAlertController LanguageSelectionAlertController { get; set; }
    [field: SerializeField] private LevelVictoryAlertController LevelVictoryAlertController { get; set; }
    [field: SerializeField] private GameOverAlertController GameOverAlertController { get; set; }
    [field: SerializeField] private TooltipController TooltipController { get; set; }

    [field: SerializeField] private GameObject InputBlocker { get; set; }

    public async Task<LanguageType> ShowLanguageSelectionAlertController()
    {
        InputBlocker.gameObject.SetActive(true);
        var result = await Instantiate(LanguageSelectionAlertController).Show();
        InputBlocker.gameObject.SetActive(false);

        return result;
    }

    public async Task<bool> ShowLevelVictoryAlertController()
    {
        InputBlocker.gameObject.SetActive(true);
        var result = await Instantiate(LevelVictoryAlertController).Show();
        InputBlocker.gameObject.SetActive(false);

        return result;
    }

    public async Task<bool> ShowGameOverAlertController(bool didWin)
    {
        InputBlocker.gameObject.SetActive(true);
        var result = await Instantiate(GameOverAlertController).Show(didWin);
        InputBlocker.gameObject.SetActive(false);

        return result;
    }

    public async Task ShowTooltip(
        string text, 
        Transform pointTo = null, 
        bool needsConfirmation = false,
        float delay = 4f)
    {
        InputBlocker.gameObject.SetActive(true);
        await Instantiate(TooltipController).Show(text, pointTo, needsConfirmation, delay);
        InputBlocker.gameObject.SetActive(false);
    }
}