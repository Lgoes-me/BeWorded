﻿using UnityEngine;
using UnityEngine.EventSystems;

public class SwapDragState : GameState
{
    private GameplayScene Game { get; set; }
    private PowerUp Swaps { get; set; }
    
    private LetterController MainLetterController { get; set; }
    private LetterController OtherLetterController { get; set; }
    
    public SwapDragState(GameplayScene game, PowerUp swaps)
    {
        Game = game;
        Swaps = swaps;
    }

    public override void OnDrag(PointerEventData pointerEventData, LetterController newSelected)
    {
        if(newSelected == null)
            return;
        
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
        if(newSelected == null)
            return;
        
        Swaps.Use();
        Game.SwapButton.UpdateButton();
        
        MainLetterController.Content.anchoredPosition = Vector3.zero;

        MainLetterController.ResetLetter();
        newSelected.ResetLetter();

        Game.LettersGrid.SwapCells(MainLetterController, newSelected);
        MainLetterController = null;

        Game.ChangeState(new GameplayState(Game));
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
                Game.ChangeState(new GameplayState(Game));
                break;
            case PowerUpType.Bomba:
                Game.ChangeState(new BombState(Game, powerUp));
                break;
            case PowerUpType.Misturar:
                Game.ChangeState(new ShuffleState(Game, powerUp));
                break;
        }
    }
}