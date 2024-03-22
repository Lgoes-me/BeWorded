using UnityEngine;
using UnityEngine.UI;

public class MenuScene : BaseScene<MenuSceneData>
{
    [field: SerializeField] private Button NewGame { get; set; }
    [field: SerializeField] private GameplaySceneData GameplaySceneData { get; set; }

    private void Start()
    {
        NewGame.onClick.AddListener(OpenNewGame);
    }

    private void OpenNewGame()
    {
        GameplaySceneData.Init(new Player());
        Application.SceneManager.ChangeMainScene(GameplaySceneData);
    }
}