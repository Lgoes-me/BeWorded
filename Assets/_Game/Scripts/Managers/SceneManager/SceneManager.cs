using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : BaseManager
{
    [field: SerializeField] private GameplaySceneData GameplaySceneData { get; set; }
    [field: SerializeField] private ShopSceneData ShopSceneData { get; set; }
    [field: SerializeField] private MenuSceneData MenuSceneData { get; set; }
    
    private BaseScene MainScene { get; set; }
    
    public void GoToShop(Player player)
    {
        ShopSceneData.Init(player);
        ChangeMainScene(ShopSceneData);
    }

    public void GoToMainMenu()
    {
        ChangeMainScene(MenuSceneData);
    }

    public void OpenGameplayScene(Player player)
    {
        GameplaySceneData.Init(player);
        ChangeMainScene(GameplaySceneData);
    }

    private void ChangeMainScene(BaseSceneData data)
    {
        data.Init(Application);
        UnitySceneManager.LoadScene(data.SceneName, LoadSceneMode.Single);
    }
    
    public void SetScene(BaseScene scene)
    {
        MainScene = scene;
    }
}
