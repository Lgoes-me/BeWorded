public class Seed
{
    public string BaseSeed { get; private set; }
    
    public int MatchSeed { get; private set; }
    public int ShopSeed { get; private set; }

    public Seed(string baseSeed)
    {
        BaseSeed = baseSeed;
    }

    public void GetNextMatchSeed(int level)
    {
        MatchSeed = BaseSeed.GetHashCode() + level;
    }

    public int GetNextLetterSeed()
    {
        MatchSeed++;
        return MatchSeed;
    }

    public int GetNextShopItemSeed(int shopNumber)
    {
        ShopSeed = BaseSeed.GetHashCode() + shopNumber;
        return ShopSeed;
    }
}