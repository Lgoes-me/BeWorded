using System;
using System.Collections.Generic;
using System.Linq;

public class Player : ISavable<PlayerModel>, ILoadable<PlayerModel>
{
    public string Id { get; set; }
    public int Level { get; private set; }
    public int Shops { get; private set; }
    public int Money { get; set; }
    public int QuantidadeJokers { get; private set; }
    public string BaseSeed { get; private set; }

    public PowerUp Swaps { get; private set; }
    public PowerUp Bombs { get; private set; }
    public PowerUp Shuffles { get; private set; }

    public List<Joker> Jokers { get; private set; }
    public Dictionary<JokerIdentifier, int> JokersExtraParams { get; private set; }
    private JokerFactory JokerFactory { get; }

    public Player()
    {
        Level = -1;
        Shops = -1;

        Id = "Player.json";

        Jokers = new List<Joker>();
        JokersExtraParams = new Dictionary<JokerIdentifier, int>();
        JokerFactory = new JokerFactory(this);
    }

    public Player(int money, int swaps, int bombs, int shuffles, int quantidadeJokers, string seed) : this()
    {
        Money = money;

        Swaps = new PowerUp(PowerUpType.Troca, swaps);
        Bombs = new PowerUp(PowerUpType.Bomba, bombs);
        Shuffles = new PowerUp(PowerUpType.Misturar, shuffles);

        QuantidadeJokers = quantidadeJokers;

        BaseSeed = string.IsNullOrWhiteSpace(seed) ? Extensions.RandomString(10) : seed;
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

    public void OpenShop()
    {
        Shops++;
    }

    public void GivePrize(int prize)
    {
        Money += prize;
    }

    public void LoadData(PlayerModel data)
    {
        Level = data.Level;
        Shops = data.Shops;
        Money = data.Money;
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

        JokersExtraParams = data.JokersExtraParams;
        QuantidadeJokers = data.QuantidadeJokers;
        BaseSeed = data.BaseSeed;
    }

    public PlayerModel SaveData()
    {
        var jokers = Jokers.Select(j => j.SaveData()).ToList();
        return new PlayerModel(Level, Shops, Money, Swaps, Bombs, Shuffles, jokers, JokersExtraParams, QuantidadeJokers,
            BaseSeed);
    }
}