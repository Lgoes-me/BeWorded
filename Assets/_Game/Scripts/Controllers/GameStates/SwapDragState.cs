using UnityEngine;
using UnityEngine.EventSystems;

public class SwapDragState : GameState
{
    private GameplayController Game { get; set; }
    
    private LetterController MainLetterController { get; set; }
    private LetterController OtherLetterController { get; set; }
    
    public SwapDragState(GameplayController game)
    {
        Game = game;
    }

    public override void OnDrag(PointerEventData pointerEventData, LetterController newSelected)
    {
        if (MainLetterController != null)
        {
            MainLetterController.Content.position = pointerEventData.position;
            
            if (OtherLetterController != null)
            {
                OtherLetterController.ResetLetter();
                OtherLetterController = newSelected;
                OtherLetterController.OnSelect(false);
                return;  
            }
            else if(newSelected != MainLetterController)
            {
                OtherLetterController = newSelected;
                OtherLetterController.OnSelect(false);
                return;
            }
            return;  
        }
        
        newSelected.OnSelect();
        MainLetterController = newSelected;
    }

    public override void OnDragEnd(PointerEventData pointerEventData, LetterController newSelected)
    {
        MainLetterController.Content.anchoredPosition = Vector3.zero;

        MainLetterController.ResetLetter();
        newSelected.ResetLetter();

        Game.LettersGrid.SwapCells(MainLetterController, newSelected);
        MainLetterController = null;

        Game.StateMachine.ChangeState(new GameplayState(Game));
    }

    public override void OnStateExit()
    {
        if (MainLetterController != null)
        {
            MainLetterController.ResetLetter();
        }
    }
}