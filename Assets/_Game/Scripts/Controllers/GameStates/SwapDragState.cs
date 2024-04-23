using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapDragState : GameState
{
    private GameEventsManager GameEventsManager { get; set; }
    private GameplayScene Game { get; set; }
    private PowerUp Swaps { get; set; }

    private LetterController MainLetterController { get; set; }
    private LetterController OtherLetterController { get; set; }

    public SwapDragState(GameEventsManager gameEventsManager, GameplayScene game, PowerUp swaps)
    {
        GameEventsManager = gameEventsManager;
        Game = game;
        Swaps = swaps;
    }

    public override void OnDrag(LetterController newSelected)
    {
        if (newSelected == null)
            return;

        if (MainLetterController != null)
        {
            MainLetterController.Content.position = Input.mousePosition;

            if (OtherLetterController != null)
            {
                OtherLetterController.ResetLetter();
                OtherLetterController = newSelected;
                OtherLetterController.OnSelect(false);
                return;
            }
            else if (newSelected != MainLetterController)
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

    public override void OnDragEnd(LetterController newSelected)
    {
        if (newSelected == null)
            return;

        Swaps.Use();
        Game.SwapButton.UpdateButton();

        MainLetterController.Content.anchoredPosition = Vector3.zero;

        MainLetterController.ResetLetter();
        newSelected.ResetLetter();

        GameEventsManager.OnPowerUpUsed.Invoke(Swaps, new List<LetterController>() {MainLetterController, newSelected});

        Game.LettersGrid.SwapCells(MainLetterController, newSelected);
        MainLetterController = null;

        Game.ChangeState(new GameplayState(GameEventsManager, Game));
    }

    public override void OnStateExit()
    {
        if (MainLetterController != null)
        {
            MainLetterController.ResetLetter();
        }
    }

    public override void OnPowerUpClicked(PowerUp powerUp)
    {
        switch (powerUp.Type)
        {
            case PowerUpType.Troca:
                Game.ChangeState(new GameplayState(GameEventsManager, Game));
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