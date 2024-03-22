using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [field: SerializeField] private Application Application { get; set; }
    [field: SerializeField] private GameplaySceneData GameplaySceneData { get; set; }
    
    private GameConfig GameConfig { get; set; }
    
    private void Awake()
    {
        Boot();
    }

    private async void Boot()
    {
        GameConfig = new GameConfig();

        if (GameConfig.Language is LanguageType.Unknown)
        {
            var language = await Application.AlertManager.ShowLanguageSelectionAlertController();
            GameConfig.SetLanguage(language);
        }
        
        Application.ContentManager.Init(GameConfig);
        
        GameplaySceneData.Init(Application, GameConfig);
        Application.SceneManager.ChangeMainScene(GameplaySceneData);
    }
}
