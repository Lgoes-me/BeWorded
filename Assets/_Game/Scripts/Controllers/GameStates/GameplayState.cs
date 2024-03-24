using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameplayState : GameState
{
    private GameplayScene Game { get; set; }
    private List<LetterController> SelectedLetterControllers { get; set; }
    
    public GameplayState(GameplayScene game)
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
        CheckResponse();
    }

    public override void OnStateExit()
    {
        Game.Response.SetText(string.Empty);
        Game.ResponseScore.SetText(string.Empty);
    }

    private void CheckResponse()
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
            return;
        
        Game.Response.SetText(string.Empty);
        Game.ResponseScore.SetText(string.Empty);
        SelectedLetterControllers.Clear();
    }
}