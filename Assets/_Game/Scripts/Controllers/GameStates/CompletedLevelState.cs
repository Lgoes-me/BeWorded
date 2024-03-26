public class CompletedLevelState : GameState
{
    private Player Player { get; set; }
    private Level Level { get; set; }
    private AlertManager AlertManager { get; set; }
    private SceneManager SceneManager { get; set; }
        
    public CompletedLevelState(
        Player player, 
        Level level, 
        Application application)
    {
        Player = player;
        Level = level;
        AlertManager = application.AlertManager;
        SceneManager = application.SceneManager;
    }

    public override async void OnStateEnter()
    {
        Player.GivePrize(Level);
        await AlertManager.ShowLevelVictoryAlertController();
        SceneManager.GoToShop(Player);
    }
}