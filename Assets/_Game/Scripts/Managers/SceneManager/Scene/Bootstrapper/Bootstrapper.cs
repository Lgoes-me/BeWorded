using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [field: SerializeField] private Application Application { get; set; }
    [field: SerializeField] private GameplaySceneData GameplaySceneData { get; set; }

    private Player Player { get; set; }
    
    private void Awake()
    {
        Boot();
    }

    private async void Boot()
    {
        Player = new Player();
        
        Application.Init();
        
        Application.ConfigManager.Init();
        await Application.ConfigManager.GetOrSetLanguage();

        Application.ContentManager.Init();
        
        GameplaySceneData.Init(Application, Player);
        Application.SceneManager.ChangeMainScene(GameplaySceneData);
    }
}
