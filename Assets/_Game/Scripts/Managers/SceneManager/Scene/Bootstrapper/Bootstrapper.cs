using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [field: SerializeField] private Application Application { get; set; }
    
    private void Awake()
    {
        Boot();
    }

    private async void Boot()
    {
        Application.Init();
        Application.SaveManager.Init();
        
        Application.ConfigManager.Init();
        await Application.ConfigManager.GetOrSetLanguage();

        Application.ContentManager.Init();
        Application.OnboardManager.Init();
        Application.SceneManager.GoToMainMenu();
    }
}
