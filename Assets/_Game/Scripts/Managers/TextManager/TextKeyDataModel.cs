public class TextKeyDataModel
{
    public string textKey { get; set; }
    public string pt { get; set; }
    public string en { get; set; }

    public TextKeyData ToDomain()
    {
        return new TextKeyData(
            textKey,
            pt,
            en);
    }
}