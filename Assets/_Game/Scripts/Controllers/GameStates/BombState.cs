public class BombState : GameState
{
    private GameEventsManager GameEventsManager { get; set; }
    private GameplayScene Game { get; set; }
    private PowerUp Bombs { get; set; }

    public BombState(GameEventsManager gameEventsManager, GameplayScene game, PowerUp bombs)
    {
        GameEventsManager = gameEventsManager;
        Game = game;
        Bombs = bombs;
    }

    public override async void OnClick(LetterController letterController)
    {
        Bombs.Use();
        Game.BombButton.UpdateButton();
        GameEventsManager.OnPowerUpUsed.Invoke(Bombs, letterController);
        
        letterController.OnBomb();

        await Game.ClearSelection(letterController);
        Game.ChangeState(new GameplayState(GameEventsManager, Game));
    }

    public override void OnPowerUpClicked(PowerUp powerUp)
    {
        switch (powerUp.Type)
        {
            case PowerUpType.Troca:
                Game.ChangeState(new SwapDragState(GameEventsManager, Game, powerUp));
                break;
            case PowerUpType.Bomba:
                Game.ChangeState(new GameplayState(GameEventsManager, Game));
                break;
            case PowerUpType.Misturar:
                Game.ChangeState(new ShuffleState(GameEventsManager, Game, powerUp));
                break;
        }
    }
}