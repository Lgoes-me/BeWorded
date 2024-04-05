using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelectionAlertController : AlertController<LanguageType>
{
    [field: SerializeField] private Button Portuguese { get; set; }
    [field: SerializeField] private Button English { get; set; }

    public Task<LanguageType> Show()
    {
        Portuguese.onClick.AddListener(() => Close(LanguageType.Pt));
        English.onClick.AddListener(() => Close(LanguageType.En));
        
        return InternalShow();
    }
}