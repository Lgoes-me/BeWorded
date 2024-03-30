using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    [field: SerializeField] private RectTransform Content { get; set; }
    [field: SerializeField] private TextMeshProUGUI Text { get; set; }
    [field: SerializeField] private Button ConfirmButton { get; set; }
    [field: SerializeField] private RectTransform UpArrow { get; set; }
    [field: SerializeField] private RectTransform DownArrow { get; set; }

    private Canvas PointTo { get; set; }
    private TaskCompletionSource<bool> CompletionSource { get; set; }

    public Task Show(string text, RectTransform pointTo, bool needsConfirmation, float duration)
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
            PointTo = pointTo.AddComponent<Canvas>();
            PointTo.overrideSorting = true;
            PointTo.sortingOrder = 9;
            
            if (!(pointTo.position.y <= Content.sizeDelta.y))
            {
                UpArrow.gameObject.SetActive(true);
                UpArrow.position = new Vector2(pointTo.position.x, DownArrow.position.y);
                Content.position = new Vector2(Content.position.x, pointTo.position.y + Content.sizeDelta.y / 2 - 50);
            }
            else
            {
                DownArrow.gameObject.SetActive(true);
                DownArrow.position = new Vector2(pointTo.position.x, DownArrow.position.y);
                Content.position = new Vector2(Content.position.x, pointTo.position.y + Content.sizeDelta.y / 2 + 50);
            }
        }

        CompletionSource = new TaskCompletionSource<bool>();
        return CompletionSource.Task;
    }

    private void Close()
    {
        if (PointTo != null)
        {
            Destroy(PointTo);
        }
        
        CompletionSource.SetResult(true);
        Destroy(this.gameObject);
    }
}