using UnityEngine.SocialPlatforms.Impl;

public class Letter
{
    public char Character { get; }
    public IPrize Prize { get; }

    public Letter(char character, IPrize prize)
    {
        Character = character;
        Prize = prize;
    }

    public override string ToString()
    {
        return Character.ToString();
    }
}