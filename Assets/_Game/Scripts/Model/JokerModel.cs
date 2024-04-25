public class JokerModel
{
    public string Identifier { get; private set; }
    public int Index { get; private set; }

    public JokerModel(string identifier, int index)
    {
        Identifier = identifier;
        Index = index;
    }
}