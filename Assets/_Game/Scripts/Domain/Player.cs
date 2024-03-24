public class Player : ISavable<PlayerModel>, ILoadable<PlayerModel>
{
    public string Id { get; set; }
    public int Level { get; private set; }
    public PowerUp Swaps { get; private set; }
    public PowerUp Bombs { get; private set; }
    public PowerUp Shuffles { get; private set; }

    public Player()
    {
        Id = "Player.bin";
        Level = -1;
        Swaps = new PowerUp(PowerUpType.Troca, 8);
        Bombs = new PowerUp(PowerUpType.Bomba, 4);
        Shuffles = new PowerUp(PowerUpType.Misturar, 1);
    }

    public void PlayLevel()
    {
        Level++;
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
        return new PlayerModel(Level, Swaps, Bombs, Shuffles);
    }
}