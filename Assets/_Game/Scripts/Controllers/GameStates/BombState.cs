using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BombState : GameState
{
    private GameplayScene Game { get; set; }

    public BombState(GameplayScene game)
    {
        Game = game;
    }

    public override void OnClick(PointerEventData pointerEventData, LetterController newSelected)
    {
        newSelected.OnError();
        Game.ClearSelection(new List<LetterController> {newSelected}, false);
        Game.ChangeState(new GameplayState(Game));
    }

    public override void OnStateExit()
    {
    }
}