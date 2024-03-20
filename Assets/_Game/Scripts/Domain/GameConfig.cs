public class GameConfig
{
    public int Height { get; private set; }
    public int Width { get; private set; } 
    public int MinimumWordSize { get; private set; } 
    public Language GameLanguage { get; private set; } 
    
    public GameConfig()
    {
        Height = 8;
        Width = 5;
        MinimumWordSize = 4;
        GameLanguage = Language.pt;
    }

    public enum Language
    {
        pt,
        en
    }
}