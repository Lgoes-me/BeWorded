public class CompletedLevelState : GameState
{
    private GameplayScene Game { get; set; }
        
    public CompletedLevelState(GameplayScene game)
    {
        Game = game;
    }

    public override void OnStateEnter()
    {
        Game.ResetGame();
    }
}