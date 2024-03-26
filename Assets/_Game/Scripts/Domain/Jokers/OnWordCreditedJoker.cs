public abstract class BaseOnWordCreditedJoker : BaseJoker
{
    public abstract void OnWordCredited(ref int basePrize, ref int baseMultiplier, string word);

    protected BaseOnWordCreditedJoker(string id) : base(id)
    {
        
    }
}

public class WordWithPatternJoker : BaseOnWordCreditedJoker
{
    private string SubString { get; set; }
    private int ExtraPrize { get; set; }
    private int ExtraMultiplier { get; set; }
    
    public WordWithPatternJoker(string id, string subString, int extraPrize, int extraMultiplier) : base(id)
    {
        SubString = subString;
        ExtraPrize = extraPrize;
        ExtraMultiplier = extraMultiplier;
    }
    
    public override void OnWordCredited(ref int basePrize, ref int baseMultiplier, string word)
    {
        if (!word.Contains(SubString))
            return;

        basePrize += ExtraPrize;
        baseMultiplier += ExtraMultiplier;
    }
}