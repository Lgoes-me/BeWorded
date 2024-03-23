using UnityEngine;
using UnityEngine.UI;

public class ShopScene : BaseScene<ShopSceneData>
{
    [field: SerializeField] private Button Continue { get; set; }
    [field: SerializeField] private GameplaySceneData GameplaySceneData { get; set; }

    private void Start()
    {
        Continue.onClick.AddListener(ContinueGame);
    }

    private void ContinueGame()
    {
        GameplaySceneData.Init(SceneData.Player);
        Application.SceneManager.ChangeMainScene(GameplaySceneData);
    }
}