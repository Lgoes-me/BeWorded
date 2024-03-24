public class GameOverState : GameState
{
    private GameplayScene Game { get; set; }
    private bool DidWin { get; set; }
    private Player Player { get; set; }
    private SaveManager SaveManager { get; set; }
    private AlertManager ApplicationAlertManager { get; set; }

    public GameOverState(GameplayScene game, bool didWin, Player player, SaveManager saveManager, AlertManager applicationAlertManager)
    {
        Game = game;
        DidWin = didWin;
        Player = player;
        SaveManager = saveManager;
        ApplicationAlertManager = applicationAlertManager;
    }

    public override async void OnStateEnter()
    {
        SaveManager.DeleteData(Player);
        await ApplicationAlertManager.ShowGameOverAlertController(DidWin);
        Game.GoToMainMenu();
    }
}