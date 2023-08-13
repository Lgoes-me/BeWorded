
public class ShuffleState : GameState
{
    private GameplayController Game { get; set; }

    public ShuffleState(GameplayController game)
    {
        Game = game;
    }

    public override void OnStateEnter()
    {
        foreach (var letterController in Game.LettersGrid)
        {
            letterController.ResetData();
        }
        
        Game.Response.SetText(string.Empty);
        Game.ChangeState(new GameplayState(Game));
    }
}