public class SwapState : GameState
{
    private GameplayController Game { get; set; }
    private LetterController SelectedLetterController { get; set; }
    
    public SwapState(GameplayController game)
    {
        Game = game;
    }

    public override void OnClick(LetterController newSelected)
    {
        newSelected.OnPointerClick();

        if (SelectedLetterController == null)
        {
            SelectedLetterController = newSelected;
            return;
        }

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