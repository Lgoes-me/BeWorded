using UnityEngine;
using UnityEngine.EventSystems;

public class GameAreaController : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler,
    IPointerExitHandler
{
    private int Height { get; set; }
    private int Width { get; set; }
    private GameplayController Game { get; set; }

    public void Init(GameplayController game, int height, int width)
    {
        Game = game;
        Height = height;
        Width = width;
        
        GetComponent<RectTransform>().sizeDelta = new Vector2(70 * Width, 70 * Height);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var newSelected = GetPointerCell(eventData);
        Game.StateMachine.State.OnClick(newSelected);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var newSelected = GetPointerCell(eventData);
        Game.StateMachine.State.OnDrag(newSelected);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var newSelected = GetPointerCell(eventData);
        Game.StateMachine.State.OnCancel(newSelected);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var newSelected = GetPointerCell(eventData);
        Game.StateMachine.State.OnCancel(newSelected);
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

        return Game.LettersGrid.Get(yPosition, xPosition);
    }
}