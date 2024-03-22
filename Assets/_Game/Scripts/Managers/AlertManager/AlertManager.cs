using System.Threading.Tasks;
using UnityEngine;

public class AlertManager : BaseManager
{
    [field: SerializeField] private LanguageSelectionAlertController LanguageSelectionAlertController { get; set; }
    
    public Task<LanguageType> ShowLanguageSelectionAlertController()
    {
        return Instantiate(LanguageSelectionAlertController).Show();
    }
}