using System.Collections.Generic;
using System.Linq;

public class Player : ISavable<PlayerModel>, ILoadable<PlayerModel>
{
    public string Id { get; set; }
    public int Level { get; private set; }
    public int Money { get; private set; }

    public PowerUp Swaps { get; private set; }
    public PowerUp Bombs { get; private set; }
    public PowerUp Shuffles { get; private set; }
    
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

        Jokers = new List<BaseJoker>() { };
        QuantidadeJokers = 5;
    }

    public void OnLetterPrizeCredited(ref int basePrize, ref int baseMultiplier, Letter letter)
    {
        foreach (var joker in Jokers)
        {
            if(joker is not BaseOnLetterPrizeCreditedJoker letterPrizeCreditedJoker)
                continue;

            letterPrizeCreditedJoker.OnLetterPrizeCredited(ref basePrize, ref baseMultiplier, letter);
        }
    }
    
    public void OnWordCredited(ref int basePrize, ref int baseMultiplier, string word)
    {
        foreach (var joker in Jokers)
        {
            if(joker is not BaseOnWordCreditedJoker wordCreditedJoker)
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
        var jokers = Jokers.Select(j => j.SaveData()).ToList();
        return new PlayerModel(Level, Money, Swaps, Bombs, Shuffles, jokers, QuantidadeJokers);
    }
}