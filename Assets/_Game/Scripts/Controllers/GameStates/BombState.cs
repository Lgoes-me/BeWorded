using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BombState : GameState
{
    private GameplayScene Game { get; set; }

    public BombState(GameplayScene game)
    {
        Game = game;
    }

    public override async void OnClick(PointerEventData pointerEventData, LetterController newSelected)
    {
        newSelected.OnError();
        await Game.ClearSelection(new List<LetterController> {newSelected});
        Game.ChangeState(new GameplayState(Game));
    }

    public override void OnStateExit()
    {
    }
}