using System.Collections.Generic;

public class GameConfig : ISavable<GameConfigModel>, ILoadable<GameConfigModel>
{
    public string Id { get; set; }
    public int Height { get; }
    public int Width { get; } 
    public int MinimumWordSize { get; } 
    public LanguageType Language { get; private set; }
    
    public List<(char, int)> WeightedLetters => new()
    {
        ('A', 8), ('B', 3), ('C', 3), ('D', 3), ('E', 7), ('F', 3), ('G', 3), ('H', 1), ('I', 6), ('J', 2), ('K', 0),
        ('L', 3), ('M', 3), ('N', 2), ('O', 8), ('P', 3), ('Q', 1), ('R', 3), ('S', 3), ('T', 3), ('U', 5), ('V', 2),
        ('W', 0), ('X', 1), ('Y', 0), ('Z', 1)
    };

    public List<(IPrize, int)> WeightedPrizes => new()
    {
        (new ScorePrize(5), 32),
        (new ScorePrize(10), 16),
        (new ScorePrize(25), 8),
        (new ScorePrize(50), 4),
        (new ScorePrize(100), 1),
        (new PowerUpPrize(PowerUpType.Troca), 2),
        (new PowerUpPrize(PowerUpType.Bomba), 2),
        (new PowerUpPrize(PowerUpType.Misturar), 1)
    };
    
    public List<int> Scores => new()
    {
        300,
        500,
        1000,
        //
        1250,
        1600,
        2400,
        //
        2800,
        3500,
        7000,
        //
        8500,
        9500,
        16000,
        //
        20000,
        30000,
        150000
    };

    public GameConfig()
    {
        Id = "GameConfig.json";
        Height = 8;
        Width = 5;
        MinimumWordSize = 3;
        Language = LanguageType.Unknown;
    }

    public void SetLanguage(LanguageType language)
    {
        Language = language;
    }

    public void LoadData(GameConfigModel data)
    {
        Language = data.Language;
    }
    
    public GameConfigModel SaveData()
    {
        return new GameConfigModel(Language);
    }
}

public class GameConfigModel
{
    public LanguageType Language { get; private set; }

    public GameConfigModel(LanguageType language)
    {
        Language = language;
    }
}

public enum LanguageType
{
    Unknown,
    Pt,
    En
}