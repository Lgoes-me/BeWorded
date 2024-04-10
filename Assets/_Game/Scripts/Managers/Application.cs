using UnityEngine;

public class Application : MonoBehaviour
{
    [field: SerializeField] public ContentManager ContentManager { get; private set; }
    [field: SerializeField] public SceneManager SceneManager { get; private set; }
    [field: SerializeField] public AlertManager AlertManager { get; private set; }
    [field: SerializeField] public ConfigManager ConfigManager { get; private set; }
    [field: SerializeField] public SaveManager SaveManager { get; private set; }
    [field: SerializeField] public TextManager TextManager { get; private set; }
    [field: SerializeField] public OnboardManager OnboardManager { get; private set; }
    [field: SerializeField] public PlayerManager PlayerManager { get; private set; }
    [field: SerializeField] public GameEventsManager GameEventsManager { get; private set; }
    
    public void Init()
    {
        DontDestroyOnLoad(gameObject);

        ContentManager.Setup(this);
        SceneManager.Setup(this);
        AlertManager.Setup(this);
        ConfigManager.Setup(this);
        SaveManager.Setup(this);
        TextManager.Setup(this);
        OnboardManager.Setup(this);
        PlayerManager.Setup(this);
        GameEventsManager.Setup(this);
    }


    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}