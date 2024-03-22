using System.Threading.Tasks;

public class ConfigManager : BaseManager
{
    public GameConfig GameConfig { get; private set; }

    public void Init()
    {
        GameConfig = new GameConfig();
    }

    public LevelConfig GetNextLevelConfig(Player player)
    {
        player.PlayLevel();
        var score = GameConfig.Scores[player.Level];

        return new LevelConfig(score);
    }

    public async Task GetOrSetLanguage()
    {
        if (GameConfig.Language is not LanguageType.Unknown)
            return;

        var language = await Application.AlertManager.ShowLanguageSelectionAlertController();
        GameConfig.SetLanguage(language);
    }
}