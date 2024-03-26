using System.Collections.Generic;

public class Player : ISavable<PlayerModel>, ILoadable<PlayerModel>
{
    public string Id { get; set; }
    public int Level { get; private set; }
    public int Money { get; private set; }

    public PowerUp Swaps { get; private set; }
    public PowerUp Bombs { get; private set; }
    public PowerUp Shuffles { get; private set; }
    
    public List<IJoker> Jokers { get; private set; }

    public Player()
    {
        Id = "Player.bin";
        Level = -1;
        Money = 4;
        
        Swaps = new PowerUp(PowerUpType.Troca, 8);
        Bombs = new PowerUp(PowerUpType.Bomba, 4);
        Shuffles = new PowerUp(PowerUpType.Misturar, 1);

        Jokers = new List<IJoker>()
        {
            new ExtraPrizeCreditedPerCharacterJoker(new List<char> {'A'}, 50),
            new ExtraMultiplierPerCharacterCreditedJoker(new List<char> {'M'}, 10),
        };
    }

    public void OnLetterPrizeCredited(ref int basePrize, ref int baseMultiplier, Letter letter)
    {
        foreach (var joker in Jokers)
        {
            if(joker is not IOnLetterPrizeCreditedJoker letterPrizeCreditedJoker)
                continue;

            letterPrizeCreditedJoker.OnLetterPrizeCredited(ref basePrize, ref baseMultiplier, letter);
        }
    }
    
    public void OnWordCredited(ref int basePrize, ref int baseMultiplier, string word)
    {
        foreach (var joker in Jokers)
        {
            if(joker is not IOnWordCreditedJoker wordCreditedJoker)
                continue;

            wordCreditedJoker.OnWordCredited(ref basePrize, ref baseMultiplier, word);
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
    }

    public PlayerModel SaveData()
    {
        return new PlayerModel(Level, Money, Swaps, Bombs, Shuffles);
    }
}