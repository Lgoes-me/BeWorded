using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [field: SerializeField] private ContentManager ContentManager { get; set; }
    [field: SerializeField] private SceneManager SceneManager { get; set; }
    [field: SerializeField] private GameplaySceneData GameplaySceneData { get; set; }
    
    private GameConfig GameConfig { get; set; }
    
    private void Awake()
    {
        GameConfig = new GameConfig();
        
        ContentManager.Init(GameConfig);
        
        GameplaySceneData.Init(SceneManager, ContentManager, GameConfig);
        SceneManager.ChangeMainScene(GameplaySceneData);
    }
}
