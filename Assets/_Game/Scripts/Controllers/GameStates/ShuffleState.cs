
public class ShuffleState : GameState
{
    private GameplayController Game { get; set; }

    public ShuffleState(GameplayController game)
    {
        Game = game;
    }

    public override void OnStateEnter()
    {
        foreach (var cell in Game.LettersGrid)
        {
            cell.Data.ResetData();
        }
        
        Game.Response.SetText(string.Empty);
        Game.StateMachine.ChangeState(new GameplayState(Game));
    }
}