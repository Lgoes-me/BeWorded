using UnityEngine;

public class HighlightController : MonoBehaviour
{
    private Canvas HighlightCanvas { get; set; }
    private bool MustDelete { get; set; }
    private bool OverrideSorting { get; set; }
    private int SortingOrder { get; set; }
    
    public void Highlight()
    {
        if (TryGetComponent<Canvas>(out var canvas))
        {
            HighlightCanvas = canvas;
        }
        else
        {
            MustDelete = true;
            HighlightCanvas = gameObject.AddComponent<Canvas>();
        }

        OverrideSorting = HighlightCanvas.overrideSorting;
        SortingOrder = HighlightCanvas.sortingOrder;
        
        HighlightCanvas.overrideSorting = true;
        HighlightCanvas.sortingOrder = 9;
    }

    public void UnHighlight()
    {
        HighlightCanvas.overrideSorting = OverrideSorting;
        HighlightCanvas.sortingOrder = SortingOrder;
        
        if (MustDelete)
        {
            Destroy(this);
        }
    }
}