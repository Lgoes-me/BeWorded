using UnityEngine;

public class Application : MonoBehaviour
{
    [field: SerializeField] public ContentManager ContentManager { get; private set; }
    [field: SerializeField] public SceneManager SceneManager { get; private set; }
    [field: SerializeField] public AlertManager AlertManager { get; private set; }
    [field: SerializeField] public ConfigManager ConfigManager { get; private set; }
    [field: SerializeField] public SaveManager SaveManager { get; private set; }

    public void Init()
    {
        DontDestroyOnLoad(gameObject);

        ContentManager.Application = this;
        SceneManager.Application = this;
        AlertManager.Application = this;
        ConfigManager.Application = this;
        SaveManager.Application = this;
    }


    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}