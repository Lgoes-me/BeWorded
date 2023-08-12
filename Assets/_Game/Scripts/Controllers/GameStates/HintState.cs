using System.Linq;

public class HintState : GameState
{
    private GameplayController Game { get; set; }

    public HintState(GameplayController game)
    {
        Game = game;
    }

    public override void OnStateEnter()
    {
        var letters = Game.LettersGrid.Select(c => c.Data.Letter.Character).ToList();
        var hint = Game.ContentManager.GetHint(letters);
        Game.Response.SetText(hint);
        
        Game.ChangeState(new GameplayState(Game));
    }
}