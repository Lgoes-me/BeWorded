using UnityEngine;
using UnityEngine.UI;

public class MenuScene : BaseScene<MenuSceneData>
{
    [field: SerializeField] private TextController NewGameTextController { get; set; }
    [field: SerializeField] private Button NewGame { get; set; }
    [field: SerializeField] private TextController ContinueTextController { get; set; }
    [field: SerializeField] private Button Continue { get; set; }
    [field: SerializeField] private Button ChangeGameLanguage { get; set; }

    private void Start()
    {
        NewGame.onClick.AddListener(() => Application.SceneManager.OpenGameplayScene(new Player()));
        ChangeGameLanguage.onClick.AddListener(ChangeLanguage);

        var savedPlayers = Application.SaveManager.LoadFilesList<PlayerModel, Player>();
        
        if (savedPlayers.Count > 0)
        {
            Continue.onClick.AddListener(() => Application.SceneManager.OpenGameplayScene(savedPlayers[0]));
        }
        else
        {
            Continue.gameObject.SetActive(false);
        }
        
        NewGameTextController.Init("new_game", Application.TextManager);
        ContinueTextController.Init("continue", Application.TextManager);

        /*if(Application.OnboardManager.CanShow("Welcome"))
        {
            await Application.AlertManager.ShowTooltip("Welcome");
        }*/
    }

    private async void ChangeLanguage()
    {
        var gameConfig = Application.ConfigManager.GameConfig;
        
        var language = await Application.AlertManager.ShowLanguageSelectionAlertController();
        gameConfig.SetLanguage(language);

        Application.SaveManager.SaveData(gameConfig);
        Application.Reset();
    }
}