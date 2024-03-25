
public class ShuffleState : GameState
{
    private GameplayScene Game { get; set; }

    public ShuffleState(GameplayScene game)
    {
        Game = game;
    }

    public override void OnStateEnter()
    {
        foreach (var letterController in Game.LettersGrid)
        {
            letterController.ResetData();
        }
        Game.ClearResponseText();
        Game.ChangeState(new GameplayState(Game));
    }
}