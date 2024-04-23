using UnityEngine;
using UnityEngine.EventSystems;

public class GameAreaController : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    private IGameAreaControllerListener Listener { get; set; }
    private RectTransform RectTransform { get; set; }
    private Grid<LetterController> LettersGrid { get; set; }
    
    public void Init(IGameAreaControllerListener listener,Grid<LetterController> lettersGrid)
    {
        Listener = listener;
        LettersGrid = lettersGrid;
        RectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var selected = GetPointerCell(eventData);
        Listener.OnPointerClick(selected);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var selected = GetPointerCell(eventData);
        Listener.OnDrag(selected);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var selected = GetPointerCell(eventData);
        Listener.OnEndDrag(selected);
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

        var width = LettersGrid.Width;
        var height = LettersGrid.Height;
        
        var xPosition = Mathf.FloorToInt(width * -1 * click.x / (localCorners[1].x + localCorners[0].x));
        var yPosition = Mathf.FloorToInt(height * -1 * clickPosition.y / (localCorners[2].y - localCorners[0].y) + (float) height / 2);

        if (xPosition < 0 || xPosition >= width || yPosition < 0 || yPosition >= height)
            return null;

        return LettersGrid.Get(yPosition, xPosition);
    }
}

public interface IGameAreaControllerListener
{
    void OnPointerClick(LetterController letterController);
    void OnDrag(LetterController letterController);
    void OnEndDrag(LetterController letterController);
}