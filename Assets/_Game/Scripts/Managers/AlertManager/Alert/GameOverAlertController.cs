using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverAlertController : AlertController<bool>
{
    [field: SerializeField] private TextMeshProUGUI EndText { get; set; }
    [field: SerializeField] private Button ContinueButton { get; set; }

    public Task<bool> Show(bool didWin)
    {
        EndText.SetText(didWin ? "Voce Ganhou" : "Tente Novamente");
        ContinueButton.onClick.AddListener(() => Close(true));
        return InternalShow();
    }
}