using UnityEngine;
using UnityEngine.UI;

public class MenuScene : BaseScene<MenuSceneData>
{
    [field: SerializeField] private Button NewGame { get; set; }
    [field: SerializeField] private Button ChangeGameLanguage { get; set; }
    [field: SerializeField] private GameplaySceneData GameplaySceneData { get; set; }

    private void Start()
    {
        NewGame.onClick.AddListener(OpenNewGame);
        ChangeGameLanguage.onClick.AddListener(ChangeLanguage);
    }

    private void OpenNewGame()
    {
        GameplaySceneData.Init(new Player());
        Application.SceneManager.ChangeMainScene(GameplaySceneData);
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