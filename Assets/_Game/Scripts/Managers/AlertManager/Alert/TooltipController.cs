using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI Text { get; set; }
    [field: SerializeField] private Button ConfirmButton { get; set; }

    private TaskCompletionSource<bool> CompletionSource { get; set; }

    public Task Show(string text, Transform pointTo, bool needsConfirmation, float duration)
    {
        Text.SetText(text);

        if (needsConfirmation)
        {
            ConfirmButton.gameObject.SetActive(true);
            ConfirmButton.onClick.AddListener(Close);
        }
        else
        {
            ConfirmButton.gameObject.SetActive(false);
            Invoke(nameof(Close), duration);
        }

        if (pointTo != null)
        {
        }

        CompletionSource = new TaskCompletionSource<bool>();
        return CompletionSource.Task;
    }

    private void Close()
    {
        CompletionSource.SetResult(true);
        Destroy(this.gameObject);
    }
}