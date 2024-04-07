using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [field: SerializeField] private Application Application { get; set; }
    [field: SerializeField] private LoadingController LoadingController { get; set; }
    [field: SerializeField] private TransitionController TransitionController { get; set; }
    
    private async void Awake()
    {
        Application.Init();
        Application.SaveManager.Init();
        LoadingController.Show(0.1f);
        
        Application.ConfigManager.Init();
        await Application.ConfigManager.GetOrSetLanguage();

        LoadingController.Show(0.5f);
        Application.ContentManager.Init();
        Application.OnboardManager.Init();
        Application.TextManager.Init();
        Application.PlayerManager.Init();
        
        LoadingController.Show(1f);
        LoadingController.Hide();

        await TransitionController.Show();
        
        if(Application.OnboardManager.CanShow("Welcome"))
        {
            Application.PlayerManager.CreateNewPlayer();
            Application.SceneManager.OpenGameplayScene(); 
        }
        else
        {
            Application.SceneManager.GoToMainMenu(); 
        }
    }
}
