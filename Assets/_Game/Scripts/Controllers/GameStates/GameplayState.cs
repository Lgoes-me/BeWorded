using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameplayState : GameState
{
    private GameEventsManager GameEventsManager { get; set; }
    private GameplayScene Game { get; set; }
    private List<LetterController> SelectedLetterControllers { get; set; }
    
    public GameplayState(GameEventsManager gameEventsManager, GameplayScene game)
    {
        GameEventsManager = gameEventsManager;
        SelectedLetterControllers = new List<LetterController>();
        Game = game;
    }

    public override void OnDrag(LetterController newSelected)
    {
        if (SelectedLetterControllers.Count == 0)
        {
            newSelected.OnDrag();
            SelectedLetterControllers.Add(newSelected);
            return;
        }

        if (SelectedLetterControllers.Contains(newSelected))
            return;
        
        var lastLetter = SelectedLetterControllers[^1];

        if (Game.LettersGrid.AreNeighbours(lastLetter, newSelected))
        {
            newSelected.OnDrag();
            SelectedLetterControllers.Add(newSelected);

            Game.SetResponse(SelectedLetterControllers);
        }
    }
    public override void OnDragEnd(LetterController letterController)
    {
        CheckResponse();
    }

    public override void OnStateExit()
    {
        Game.ClearResponseText();
    }

    private async void CheckResponse()
    {
        var rightResponse = Game.CheckResponse(SelectedLetterControllers);
        
        foreach (var letter in SelectedLetterControllers)
        {
            if (rightResponse)
                letter.OnCorrect();
            else
                letter.OnError();
        }

        if (rightResponse)
        {
            Game.GetPrizes(SelectedLetterControllers);
            await Game.ClearSelection(SelectedLetterControllers);
            Game.CheckIfGameEnded();
        }
        else
        {
            Game.ClearResponseText();
            SelectedLetterControllers.Clear();
        }
    }
    
    public override void OnPowerUpClicked(PowerUp powerUp)
    {
        switch (powerUp.Type)
        {
            case PowerUpType.Troca:
                Game.ChangeState(new SwapDragState(GameEventsManager, Game, powerUp));
                break;
            case PowerUpType.Bomba:
                Game.ChangeState(new BombState(GameEventsManager, Game, powerUp));
                break;
            case PowerUpType.Misturar:
                Game.ChangeState(new ShuffleState(GameEventsManager, Game, powerUp));
                break;
        }
    }
}