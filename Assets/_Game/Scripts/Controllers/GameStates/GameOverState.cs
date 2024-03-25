public class GameOverState : GameState
{
    private bool DidWin { get; set; }
    private Player Player { get; set; }
    private SaveManager SaveManager { get; set; }
    private AlertManager AlertManager { get; set; }
    private SceneManager SceneManager { get; set; }

    public GameOverState(
        bool didWin, 
        Player player, 
        Application application)
    {
        DidWin = didWin;
        Player = player;
        SaveManager = application.SaveManager;
        AlertManager = application.AlertManager;
        SceneManager = application.SceneManager;
    }

    public override async void OnStateEnter()
    {
        SaveManager.DeleteData(Player);
        await AlertManager.ShowGameOverAlertController(DidWin);
        SceneManager.GoToMainMenu();
    }
}