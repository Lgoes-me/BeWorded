public class GameStateMachine
{
    public GameState State { get; private set; }
    
    public GameStateMachine(GameplayController game)
    {
        State = new GameplayState(game);
    }

    public void ChangeState(GameState state)
    {
        State.OnStateExit();
        State = state;
        State.OnStateEnter();
    }
}