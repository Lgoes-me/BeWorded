using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameAreaController : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler,
    IPointerExitHandler
{
    [field: SerializeField] private int Height { get; set; }
    [field: SerializeField] private int Width { get; set; }
    [field: SerializeField] private LetterController LetterControllerPrefab { get; set; }
    [field: SerializeField] private ContentManager ContentManager { get; set; }

    [field: SerializeField] private TextMeshProUGUI Response { get; set; }

    private Grid<LetterController> Letters { get; set; }
    private LetterController SelectedLetterController { get; set; }
    private List<LetterController> SelectedLetterControllers { get; set; }

    private bool CanInput { get; set; }

    private void Awake()
    {
        Letters = new Grid<LetterController>(Height, Width, CreateLetterController);
        SelectedLetterControllers = new List<LetterController>();

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(70 * Width, 70 * Height);

        CanInput = true;
    }

    private LetterController CreateLetterController()
    {
        return Instantiate(LetterControllerPrefab, transform).Init(ContentManager.GetRandomLetter);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanInput)
            return;

        var newSelected = GetPointerCell(eventData);

        if (newSelected == null) return;

        newSelected.OnPointerClick();

        if (SelectedLetterController != null)
        {
            if (Letters.AreNeighbours(SelectedLetterController, newSelected))
            {
                SelectedLetterController.ResetLetter();
                newSelected.ResetLetter();

                Letters.SwapCells(SelectedLetterController, newSelected);

                SelectedLetterController = null;
                return;
            }

            SelectedLetterController.OnError();
        }

        SelectedLetterController = newSelected;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CanInput)
            return;

        var newSelected = GetPointerCell(eventData);

        if (newSelected == null) return;

        if (SelectedLetterControllers.Count == 0)
        {
            newSelected.OnDrag();
            SelectedLetterControllers.Add(newSelected);
            return;
        }

        if (SelectedLetterControllers.Contains(newSelected)) return;

        var lastLetter = SelectedLetterControllers[^1];

        if (Letters.AreNeighbours(lastLetter, newSelected))
        {
            newSelected.OnDrag();
            SelectedLetterControllers.Add(newSelected);
            Response.SetText(string.Join("", SelectedLetterControllers.Select(s => s.Letter)));
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CheckResponse();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CheckResponse();
    }

    private void CheckResponse()
    {
        if (SelectedLetterControllers.Count < 3)
        {
            CancelSelection();
            return;
        }
        
        var response = string.Join("", SelectedLetterControllers.Select(s => s.Letter));

        if (ContentManager.IsValidWord(response))
        {
            StartCoroutine(ClearSelection());
        }
        else
        {
            CancelSelection();
        }
    }

    private IEnumerator ClearSelection()
    {
        CanInput = false;

        Letters.ClearCells(SelectedLetterControllers);
        SelectedLetterControllers.Clear();
        Response.SetText(string.Empty);

        yield return Letters.SortEmpty();
        yield return Letters.FillNewData();

        CanInput = true;
    }

    private void CancelSelection()
    {
        foreach (var letter in SelectedLetterControllers)
        {
            letter.OnError();
        }

        SelectedLetterControllers.Clear();
        Response.SetText(string.Empty);
    }

    private LetterController GetPointerCell(PointerEventData eventData)
    {
        var rectTransform = GetComponent<RectTransform>();
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var clickPosition))

            return null;

        var xPosition = Mathf.FloorToInt(Width * clickPosition.x / rectTransform.sizeDelta.x);
        var yPosition = Mathf.FloorToInt(Height * -1 * clickPosition.y / rectTransform.sizeDelta.y);

        return Letters.Get(yPosition, xPosition);
    }
}