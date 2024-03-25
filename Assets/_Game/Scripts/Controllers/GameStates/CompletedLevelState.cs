public class CompletedLevelState : GameState
{
    private GameplayScene Game { get; set; }
    private Player Player { get; set; }
    private Level Level { get; set; }
    private AlertManager ApplicationAlertManager { get; set; }
        
    public CompletedLevelState(GameplayScene game, Player player, Level level, AlertManager applicationAlertManager)
    {
        Game = game;
        Player = player;
        Level = level;
        ApplicationAlertManager = applicationAlertManager;
    }

    public override async void OnStateEnter()
    {
        await ApplicationAlertManager.ShowLevelVictoryAlertController();
        Game.GoToShop();
    }
}