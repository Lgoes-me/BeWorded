
using System.Linq;

public class ShuffleState : GameState
{
    private GameEventsManager GameEventsManager { get; set; }
    private GameplayScene Game { get; set; }
    private PowerUp Shuffle { get; set; }

    public ShuffleState(GameEventsManager gameEventsManager, GameplayScene game, PowerUp shuffle)
    {
        GameEventsManager = gameEventsManager;
        Game = game;
        Shuffle = shuffle;
    }

    public override void OnStateEnter()
    {
        Shuffle.Use();
        Game.ShuffleButton.UpdateButton();

        var letterControllers = Game.LettersGrid.ToList();
        GameEventsManager.OnPowerUpUsed.Invoke(Shuffle, letterControllers);
        foreach (var letterController in letterControllers)
        {
            letterController.ResetData();
        }
        Game.ClearResponseText();
        Game.ChangeState(new GameplayState(GameEventsManager, Game));
    }
}