
public class ShuffleState : GameState
{
    private GameplayScene Game { get; set; }
    private PowerUp Shuffle { get; set; }

    public ShuffleState(GameplayScene game, PowerUp shuffle)
    {
        Game = game;
        Shuffle = shuffle;
    }

    public override void OnStateEnter()
    {
        Shuffle.Use();
        Game.ShuffleButton.UpdateButton();
        
        foreach (var letterController in Game.LettersGrid)
        {
            letterController.ResetData();
        }
        Game.ClearResponseText();
        Game.ChangeState(new GameplayState(Game));
    }
}