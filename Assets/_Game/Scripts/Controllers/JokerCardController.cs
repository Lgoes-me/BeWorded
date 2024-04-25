using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class JokerCardController : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    [field: SerializeField] private TextMeshProUGUI JokerName { get; set; }

    private JokerAreaController JokerAreaController { get; set; }
    
    public Joker Joker { get; private set; }
    private bool IsDragging { get; set; }
    
    public JokerCardController Init(Joker joker, JokerAreaController jokerAreaController)
    {
        JokerAreaController = jokerAreaController;
        
        Joker = joker;
        JokerName.SetText(Joker.Id);
        return this;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        IsDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        IsDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
        JokerAreaController.ResetArea();
    }
    
    private void Update()
    {
        if(!IsDragging)
            return;
        
        transform.position = Input.mousePosition;
    }
}