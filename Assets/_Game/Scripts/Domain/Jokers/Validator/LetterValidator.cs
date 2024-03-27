using System.Collections.Generic;

public abstract class LetterValidator
{
    public abstract bool Validate(Letter letter);
}

public class ContainsCharacterLetterValidator : LetterValidator
{
    private List<char> Characters { get; set; }

    public ContainsCharacterLetterValidator(List<char> characters)
    {
        Characters = characters;
    }

    public override bool Validate(Letter letter)
    {
        return Characters.Contains(letter.Character);
    }
}

public class IsPrizeLetterValidator : LetterValidator
{
    private IPrize Prize { get; set; }

    public IsPrizeLetterValidator(IPrize prize)
    {
        Prize = prize;
    }

    public override bool Validate(Letter letter)
    {
        return Prize == letter.Prize;
    }
}