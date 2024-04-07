using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : BaseScene<MenuSceneData>
{
    [field: SerializeField] private TextController NewGameTextController { get; set; }
    [field: SerializeField] private Button NewGame { get; set; }
    [field: SerializeField] private TMP_InputField NewGameSeed { get; set; }
    [field: SerializeField] private TextController ContinueTextController { get; set; }
    [field: SerializeField] private Button Continue { get; set; }
    [field: SerializeField] private Button ChangeGameLanguage { get; set; }

    private void Start()
    {
        NewGame.onClick.AddListener(OpenNewMatch);
        ChangeGameLanguage.onClick.AddListener(ChangeLanguage);

        if (Application.PlayerManager.HasLoadedPlayer())
        {
            Continue.onClick.AddListener(LoadPreviousMatch);
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
    
    private void LoadPreviousMatch()
    {
        Application.PlayerManager.LoadSavedPlayer();
        Application.SceneManager.OpenGameplayScene();
    }
    
    private void OpenNewMatch()
    {
        var seed = NewGameSeed.text.Length == 10 ? NewGameSeed.text : ""; 
        Application.PlayerManager.CreateNewPlayer(seed);
        Application.SceneManager.OpenGameplayScene();
    }
}