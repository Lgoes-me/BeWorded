using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour, IDeselectHandler
{
    [field: SerializeField] private RectTransform Content { get; set; }
    [field: SerializeField] private TextMeshProUGUI Text { get; set; }
    [field: SerializeField] private Button ConfirmButton { get; set; }

    private List<HighlightController> Highlights { get; set; }
    private TaskCompletionSource<bool> CompletionSource { get; set; }

    private Coroutine Coroutine { get; set; }
    private WaitForSeconds WaitForSeconds { get; set; }

    public Task Show(string text, List<RectTransform> highlights, bool needsConfirmation, float duration)
    {
        Text.SetText(text);

        if (needsConfirmation)
        {
            ConfirmButton.gameObject.SetActive(true);
            ConfirmButton.onClick.AddListener(Close);
        }
        else
        {
            SetSelected();
            ConfirmButton.gameObject.SetActive(false);
            WaitForSeconds = new WaitForSeconds(duration);
            Coroutine = StartCoroutine(CloseCoroutine());
        }

        DoHighlight(highlights);

        CompletionSource = new TaskCompletionSource<bool>();
        return CompletionSource.Task;
    }

    private void DoHighlight(List<RectTransform> highlights)
    {
        Highlights = new List<HighlightController>();

        foreach (var highlight in highlights)
        {
            var newHighlight = highlight.AddComponent<HighlightController>();
            Highlights.Add(newHighlight);

            newHighlight.Highlight();
        }

        if (highlights.Count == 0)
            return;

        var verticalPosition = highlights[0].position.y + Content.sizeDelta.y;

        if (!(highlights[0].position.y >= Content.sizeDelta.y))
        {
            verticalPosition += highlights[0].sizeDelta.y;
        }
        else
        {
            verticalPosition -= highlights[0].sizeDelta.y;
        }
        
        Content.position = new Vector2(highlights[0].position.x, verticalPosition);
    }


    private IEnumerator CloseCoroutine()
    {
        yield return WaitForSeconds;
        Close();
    }

    private void Close()
    {
        foreach (var highlight in Highlights)
        {
            highlight.UnHighlight();
        }

        if (Coroutine != null)
        {
            StopCoroutine(Coroutine);
        }

        CompletionSource.SetResult(true);
        Destroy(this.gameObject);
    }

    private void SetSelected()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (eventData is not PointerEventData pointerEventData)
            return;

        var selectedGameObject = pointerEventData.pointerCurrentRaycast.gameObject;

        if (selectedGameObject.transform.IsChildOf(transform))
            return;

        Close();
    }
}