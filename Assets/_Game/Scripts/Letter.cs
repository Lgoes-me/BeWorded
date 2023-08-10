public class Letter
{
    public char Character { get; }
    public int Points { get; }

    public Letter(char character, int points)
    {
        Character = character;
        Points = points;
    }

    public override string ToString()
    {
        return Character.ToString();
    }
}