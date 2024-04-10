using System.Linq;

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

public class StartsWithValidator : WordValidator
{
    private char Character { get; set; }

    public StartsWithValidator(char character)
    {
        Character = character;
    }
    
    public override bool Validate(string word)
    {
        return word.FirstOrDefault() == Character;
    }
}

public class EndsWithValidator : WordValidator
{
    private char Character { get; set; }

    public EndsWithValidator(char character)
    {
        Character = character;
    }
    
    public override bool Validate(string word)
    {
        return word.LastOrDefault() == Character;
    }
}