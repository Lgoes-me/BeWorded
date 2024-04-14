using System.Collections.Generic;
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

public class MultipleLetterValidator : WordValidator
{
    private int Count { get; set; }

    public MultipleLetterValidator(int count)
    {
        Count = count;
    }

    public override bool Validate(string word)
    {
        var count = word.GroupBy(x => x).Select(y=>y).Count(z => z.Count()>=Count);
        return count > 0;
    }
}