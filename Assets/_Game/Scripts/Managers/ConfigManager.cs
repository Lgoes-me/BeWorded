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

        if (player.Level > GameConfig.Levels.Count) throw new InvalidOperationException();
        
        return GameConfig.Levels[player.Level];
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