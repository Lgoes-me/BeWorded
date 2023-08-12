using UnityEngine;
using UnityEngine.EventSystems;

public class GameAreaController : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    private int Height { get; set; }
    private int Width { get; set; }
    private GameplayController Game { get; set; }
    private RectTransform RectTransform { get; set; }
    
    public void Init(GameplayController game, int height, int width)
    {
        Game = game;
        Height = height;
        Width = width;
        RectTransform = GetComponent<RectTransform>();
            
        RectTransform.sizeDelta = new Vector2(70 * Width, 70 * Height);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var selected = GetPointerCell(eventData);
        Game.State.OnClick(eventData, selected);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var selected = GetPointerCell(eventData);
        Game.State.OnDrag(eventData, selected);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var selected = GetPointerCell(eventData);
        Game.State.OnDragEnd(eventData, selected);
    }

    private LetterController GetPointerCell(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var clickPosition))

            return null;

        var xPosition = Mathf.FloorToInt(Width * clickPosition.x / RectTransform.sizeDelta.x);
        var yPosition = Mathf.FloorToInt(Height * -1 * clickPosition.y / RectTransform.sizeDelta.y);

        if (xPosition < 0 || xPosition >= Width || yPosition < 0 || yPosition >= Height)
            return null;

        return Game.LettersGrid.Get(yPosition, xPosition);
    }
}