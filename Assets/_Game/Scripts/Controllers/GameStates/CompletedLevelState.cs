public class CompletedLevelState : GameState
{
    private GameplayController Game { get; set; }
        
    public CompletedLevelState(GameplayController game)
    {
        Game = game;
    }

    public override void OnStateEnter()
    {
        Game.ResetButton.gameObject.SetActive(true);
    }
}