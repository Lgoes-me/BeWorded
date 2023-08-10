﻿using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameplayState : GameState
{
    private GameplayController Game { get; set; }
    private List<LetterController> SelectedLetterControllers { get; set; }
    

    public GameplayState(GameplayController game)
    {
        SelectedLetterControllers = new List<LetterController>();
        Game = game;
    }

    public override void OnDrag(PointerEventData pointerEventData, LetterController newSelected)
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
    public override void OnDragEnd(PointerEventData pointerEventData, LetterController letterController)
    {
        OnCancel();
    }

    private void OnCancel()
    {
        if (!Game.CheckResponse(SelectedLetterControllers))
        {
            foreach (var letter in SelectedLetterControllers)
            {
                letter.OnError();
            }
        }

        SelectedLetterControllers.Clear();
        Game.SetResponse(SelectedLetterControllers);
    }
}