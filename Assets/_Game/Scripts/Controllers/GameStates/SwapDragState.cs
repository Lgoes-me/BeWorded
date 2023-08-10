using UnityEngine;
using UnityEngine.EventSystems;

public class SwapDragState : GameState
{
    private GameplayController Game { get; set; }
    private LetterController SelectedLetterController { get; set; }
    
    public SwapDragState(GameplayController game)
    {
        Game = game;
    }

    public override void OnDrag(PointerEventData pointerEventData, LetterController newSelected)
    {
        if (SelectedLetterController != null)
        {
            SelectedLetterController.Content.position = pointerEventData.position;
            return;  
        }
        
        newSelected.OnSelect();
        SelectedLetterController = newSelected;
    }

    public override void OnDragEnd(PointerEventData pointerEventData, LetterController newSelected)
    {
        SelectedLetterController.Content.anchoredPosition = Vector3.zero;

        SelectedLetterController.ResetLetter();
        newSelected.ResetLetter();

        Game.LettersGrid.SwapCells(SelectedLetterController, newSelected);
        SelectedLetterController = null;

        Game.StateMachine.ChangeState(new GameplayState(Game));
    }

    public override void OnStateExit()
    {
        if (SelectedLetterController != null)
        {
            SelectedLetterController.ResetLetter();
        }
    }
}