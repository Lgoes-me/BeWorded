public interface IOnWordCreditedJoker : IJoker
{
    void OnWordCredited(ref int basePrize, ref int baseMultiplier, string word);
}

public class WordWithPatternJoker : IOnWordCreditedJoker
{
    private string SubString { get; set; }
    private int ExtraPrize { get; set; }
    private int ExtraMultiplier { get; set; }
    
    public WordWithPatternJoker(string subString, int extraPrize, int extraMultiplier)
    {
        SubString = subString;
        ExtraPrize = extraPrize;
        ExtraMultiplier = extraMultiplier;
    }
    
    public void OnWordCredited(ref int basePrize, ref int baseMultiplier, string word)
    {
        if (!word.Contains(SubString))
            return;

        basePrize += ExtraPrize;
        baseMultiplier += ExtraMultiplier;
    }
}