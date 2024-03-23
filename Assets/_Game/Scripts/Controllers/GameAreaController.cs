using UnityEngine;
using UnityEngine.EventSystems;

public class GameAreaController : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    private int Height { get; set; }
    private int Width { get; set; }
    private GameplayScene Game { get; set; }
    private RectTransform RectTransform { get; set; }
    
    public void Init(GameplayScene game, int height, int width)
    {
        Game = game;
        Height = height;
        Width = width;
        RectTransform = GetComponent<RectTransform>();
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
        {
            return null;
        }

        Vector3 click = new Vector3(clickPosition.x, clickPosition.y, 0f);
        
        Vector3[] localCorners = new Vector3[4];
        RectTransform.GetLocalCorners(localCorners);
        
        click -= localCorners[0];

        var xPosition = Mathf.FloorToInt(Width * -1 * click.x / (localCorners[1].x + localCorners[0].x));
        var yPosition = Mathf.FloorToInt(Height * -1 * clickPosition.y / (localCorners[2].y - localCorners[0].y) + (float) Height / 2);

        if (xPosition < 0 || xPosition >= Width || yPosition < 0 || yPosition >= Height)
            return null;

        return Game.LettersGrid.Get(yPosition, xPosition);
    }
}