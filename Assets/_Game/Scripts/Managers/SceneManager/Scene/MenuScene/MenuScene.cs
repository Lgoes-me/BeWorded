using UnityEngine;
using UnityEngine.UI;

public class MenuScene : BaseScene<MenuSceneData>
{
    [field: SerializeField] private Button NewGame { get; set; }
    [field: SerializeField] private Button Continue { get; set; }
    [field: SerializeField] private Button ChangeGameLanguage { get; set; }
    [field: SerializeField] private GameplaySceneData GameplaySceneData { get; set; }

    private void Start()
    {
        NewGame.onClick.AddListener(() => OpenNewGame(new Player()));
        ChangeGameLanguage.onClick.AddListener(ChangeLanguage);

        var savedPlayers = Application.SaveManager.LoadFilesList<PlayerModel, Player>();
        
        if (savedPlayers.Count > 0)
        {
            var player = savedPlayers[0];
            Continue.onClick.AddListener(() => OpenNewGame(player));
        }
        else
        {
            Continue.gameObject.SetActive(false);
        }
    }

    private void OpenNewGame(Player player)
    {
        GameplaySceneData.Init(player);
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