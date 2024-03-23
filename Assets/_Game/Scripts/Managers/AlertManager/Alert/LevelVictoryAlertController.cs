using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelVictoryAlertController : AlertController<bool>
{
    [field: SerializeField] private Button ContinueButton { get; set; }

    public override Task<bool> Show()
    {
        ContinueButton.onClick.AddListener(() => Close(true));
        return base.Show();
    }
}