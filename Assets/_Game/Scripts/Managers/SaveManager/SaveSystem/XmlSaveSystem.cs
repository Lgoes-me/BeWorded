using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class XmlSaveSystem<T> : SaveSystem<T> where T : class
{
    public override void SaveFile(string path, T state)
    {
        using (var stream = File.Open(path, FileMode.Create))
        using (var streamWriter = new StreamWriter(stream))
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(streamWriter, state);
        }
    }

    public override T LoadFile(string path)
    {
        if (!File.Exists(path))
            return null;

        using (var stream = File.Open(path, FileMode.Open))
        using (var streamReader = new StreamReader(stream))
        using (var xmlStream = new XmlTextReader(streamReader))
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(xmlStream);
        }
    }
}