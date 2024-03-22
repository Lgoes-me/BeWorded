using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [field: SerializeField] private Application Application { get; set; }
    [field: SerializeField] private MenuSceneData MenuSceneData { get; set; }
    
    private void Awake()
    {
        Boot();
    }

    private async void Boot()
    {
        Application.Init();
        
        Application.ConfigManager.Init();
        await Application.ConfigManager.GetOrSetLanguage();

        Application.ContentManager.Init();
        Application.SceneManager.ChangeMainScene(MenuSceneData);
    }
}
