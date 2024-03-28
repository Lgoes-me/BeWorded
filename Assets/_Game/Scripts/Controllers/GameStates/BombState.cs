using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BombState : GameState
{
    private GameplayScene Game { get; set; }
    private PowerUp Bombs { get; set; }

    public BombState(GameplayScene game, PowerUp bombs)
    {
        Game = game;
        Bombs = bombs;
    }

    public override async void OnClick(PointerEventData pointerEventData, LetterController newSelected)
    {
        Bombs.Use();
        Game.BombButton.UpdateButton();
        
        newSelected.OnError();
        await Game.ClearSelection(new List<LetterController> {newSelected});
        Game.ChangeState(new GameplayState(Game));
    }

    public override void OnPowerUpClicked(PowerUp powerUp)
    {
        switch (powerUp.Type)
        {
            case PowerUpType.Troca:
                Game.ChangeState(new SwapDragState(Game, powerUp));
                break;
            case PowerUpType.Bomba:
                Game.ChangeState(new GameplayState(Game));
                break;
            case PowerUpType.Misturar:
                Game.ChangeState(new ShuffleState(Game, powerUp));
                break;
        }
    }
}