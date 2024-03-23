using System.Threading.Tasks;
using UnityEngine;

public class AlertManager : BaseManager
{
    [field: SerializeField] private LanguageSelectionAlertController LanguageSelectionAlertController { get; set; }
    [field: SerializeField] private LevelVictoryAlertController LevelVictoryAlertController { get; set; }
    
    public Task<LanguageType> ShowLanguageSelectionAlertController()
    {
        return Instantiate(LanguageSelectionAlertController).Show();
    }
    
    public Task<bool> ShowLevelVictoryAlertController()
    {
        return Instantiate(LevelVictoryAlertController).Show();
    }
}