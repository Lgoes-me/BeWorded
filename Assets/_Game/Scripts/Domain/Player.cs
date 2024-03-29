using System;
using System.Collections.Generic;
using System.Linq;

public class Player : ISavable<PlayerModel>, ILoadable<PlayerModel>
{
    public string Id { get; set; }
    public int Level { get; private set; }
    public int Money { get; set; }

    public PowerUp Swaps { get; private set; }
    public PowerUp Bombs { get; private set; }
    public PowerUp Shuffles { get; private set; }

    private JokerFactory JokerFactory { get; }
    public List<BaseJoker> Jokers { get; private set; }
    public int QuantidadeJokers { get; private set; }

    public Player()
    {
        Id = "Player.json";
        Level = -1;
        Money = 4;

        Swaps = new PowerUp(PowerUpType.Troca, 8);
        Bombs = new PowerUp(PowerUpType.Bomba, 4);
        Shuffles = new PowerUp(PowerUpType.Misturar, 1);

        JokerFactory = new JokerFactory(this);
        
        Jokers = new List<BaseJoker>();
        QuantidadeJokers = 5;
    }

    public void OnLetterScored(ref int basePrize, ref int baseMultiplier, Letter letter)
    {
        foreach (var joker in Jokers)
        {
            if (joker is not OnLetterScoredJoker letterScoredJoker)
                continue;

            letterScoredJoker.OnLetterScored(ref basePrize, ref baseMultiplier, letter);
        }
    }

    public void OnWordScored(ref int basePrize, ref int baseMultiplier, string word)
    {
        foreach (var joker in Jokers)
        {
            if (joker is not OnWordScoredJoker wordScoredJoker)
                continue;

            wordScoredJoker.OnWordScored(ref basePrize, ref baseMultiplier, word);
        }
    }
    
    public void GainPowerUp(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.Troca:
                Swaps.Gain();
                break;
            case PowerUpType.Bomba:
                Bombs.Gain();
                break;
            case PowerUpType.Misturar:
                Shuffles.Gain();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void PlayLevel()
    {
        Level++;
    }

    public void GivePrize(int prize)
    {
        Money += prize;
    }

    public void LoadData(PlayerModel data)
    {
        Level = data.Level;
        Swaps = data.Swaps;
        Bombs = data.Bombs;
        Shuffles = data.Shuffles;

        foreach (var jokerModel in data.Jokers)
        {
            if (!Enum.TryParse(jokerModel.Identifier, out JokerIdentifier identifier)) 
                continue;
            
            var joker = JokerFactory.CreateJoker(identifier);
            Jokers.Add(joker);
        }

        QuantidadeJokers = data.QuantidadeJokers;
    }

    public PlayerModel SaveData()
    {
        var jokers = Jokers.Select(j => j.SaveData()).ToList();
        return new PlayerModel(Level, Money, Swaps, Bombs, Shuffles, jokers, QuantidadeJokers);
    }
}