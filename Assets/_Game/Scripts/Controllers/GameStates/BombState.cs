using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BombState : GameState
{
    private GameplayController Game { get; set; }

    public BombState(GameplayController game)
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