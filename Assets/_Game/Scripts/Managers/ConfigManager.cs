using System;
using System.Threading.Tasks;

public class ConfigManager : BaseManager
{
    public GameConfig GameConfig { get; private set; }

    public void Init()
    {
        GameConfig = new GameConfig();
    }

    public Level GetNextLevelConfig(Player player)
    {
        player.PlayLevel();

        if (player.Level > GameConfig.Scores.Count) throw new InvalidOperationException();
        
        var score = GameConfig.Scores[player.Level];
        return new Level(score, player.Level == GameConfig.Scores.Count);
    }

    public async Task GetOrSetLanguage()
    {
        Application.SaveManager.LoadData(GameConfig);
        
        if (GameConfig.Language is not LanguageType.Unknown)
            return;

        var language = await Application.AlertManager.ShowLanguageSelectionAlertController();
        GameConfig.SetLanguage(language);
        
        Application.SaveManager.SaveData(GameConfig);
    }
}