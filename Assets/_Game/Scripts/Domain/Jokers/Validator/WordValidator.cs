public abstract class WordValidator
{
    public abstract bool Validate(string word);
}

public class WordPatternValidator : WordValidator
{
    private string SubString { get; set; }

    public WordPatternValidator(string subString)
    {
        SubString = subString;
    }
    
    public override bool Validate(string word)
    {
        return word.Contains(SubString);
    }
}

public class NoValidator : WordValidator
{
    public override bool Validate(string word)
    {
        return true;
    }
}