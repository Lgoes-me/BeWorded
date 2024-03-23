public class CompletedLevelState : GameState
{
    private GameplayScene Game { get; set; }
    private AlertManager ApplicationAlertManager { get; set; }
        
    public CompletedLevelState(GameplayScene game, AlertManager applicationAlertManager)
    {
        Game = game;
        ApplicationAlertManager = applicationAlertManager;
    }

    public override async void OnStateEnter()
    {
        await ApplicationAlertManager.ShowLevelVictoryAlertController();
        Game.GoToShop();
    }
}