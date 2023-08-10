using UnityEngine;
using UnityEngine.EventSystems;

public class GameAreaController : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler,
    IPointerExitHandler
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
        Game.StateMachine.State.OnClick(eventData, GetPointerCell(eventData));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Game.StateMachine.State.OnDrag(eventData, GetPointerCell(eventData));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Game.StateMachine.State.OnDragEnd(eventData, GetPointerCell(eventData));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Game.StateMachine.State.OnPointerExit(eventData, GetPointerCell(eventData));
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

        return Game.LettersGrid.Get(yPosition, xPosition);
    }
}