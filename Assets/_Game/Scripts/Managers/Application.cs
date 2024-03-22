using UnityEngine;

public class Application : MonoBehaviour
{
    [field: SerializeField] public ContentManager ContentManager { get; private set; }
    [field: SerializeField] public SceneManager SceneManager { get; private set; }
    [field: SerializeField] public AlertManager AlertManager { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}