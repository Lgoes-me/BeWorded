using UnityEngine.EventSystems;

public abstract class GameState
{
    public virtual void OnStateEnter()
    {
        
    }
    
    public virtual void OnClick(PointerEventData pointerEventData, LetterController letterController)
    {
        
    }
    
    public virtual void OnDrag(PointerEventData pointerEventData, LetterController letterController)
    {
        
    }
    
    public virtual void OnDragEnd(PointerEventData eventData, LetterController getPointerCell)
    {
        
    }
    
    public virtual void OnStateExit()
    {
        
    }

}