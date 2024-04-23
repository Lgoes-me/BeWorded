public abstract class GameState
{
    public virtual void OnStateEnter()
    {
        
    }
    
    public virtual void OnClick(LetterController letterController)
    {
        
    }
    
    public virtual void OnDrag(LetterController letterController)
    {
        
    }
    
    public virtual void OnDragEnd(LetterController getPointerCell)
    {
        
    }
    
    public virtual void OnStateExit()
    {
        
    }

    public virtual void OnPowerUpClicked(PowerUp powerUp)
    {
        
    }
}