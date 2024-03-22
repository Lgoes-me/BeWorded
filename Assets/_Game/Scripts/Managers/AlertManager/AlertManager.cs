using System.Threading.Tasks;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    [field: SerializeField] private LanguageSelectionAlertController LanguageSelectionAlertController { get; set; }
    
    public Task<LanguageType> ShowLanguageSelectionAlertController()
    {
        return Instantiate(LanguageSelectionAlertController).Show();
    }
}