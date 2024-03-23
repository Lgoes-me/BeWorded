using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using Environment = System.Environment;

public class SaveManager : BaseManager
{
    private string Documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private string BaseSavePath => Path.Combine(Documentos, "BeWorded");

    public void Init()
    {
        if (!Directory.Exists(BaseSavePath))
        {
            Directory.CreateDirectory(BaseSavePath);
        }
    }

    public void SaveData<T>(ISavable<T> savable) where T : class
    {
        var data = savable.SaveData();
        var fileName = savable.Id;

        var saveSystem = GetSaveSystem<T>(fileName);
        saveSystem.SaveFile(FilePath<T>(fileName), data);
    }

    public void LoadData<T>(ILoadable<T> loadable) where T : class
    {
        var data = LoadFile<T>(loadable.Id);

        if (data == null)
            return;

        loadable.LoadData(data);
    }

    public void LoadData<T>(ILoadable<T> loadable, string fileName) where T : class
    {
        var data = LoadFile<T>(fileName);

        if (data == null)
            return;

        loadable.LoadData(data);
    }

    public T LoadFile<T>(string fileName) where T : class
    {
        var saveSystem = GetSaveSystem<T>(fileName);
        return saveSystem?.LoadFile(FilePath<T>(fileName));
    }

    public List<string> LoadFilesNames<T>() where T : class
    {
        return Directory.GetFiles(FolderPath<T>()).ToList();
    }

    public List<T2> LoadFilesList<T, T2>() 
        where T : class
        where T2 : ILoadable<T>, new()  
    {
        var filesList = new List<T2>();
        
        foreach (var fileName in LoadFilesNames<T>())
        {
            var data = LoadFile<T>(fileName);
            
            if (data == null)
                continue;

            var newT = new T2();
            newT.LoadData(data);

            filesList.Add(newT);
        }

        return filesList;
    }

    public void DeleteData<T>(string fileName) where T : class
    {
        File.Delete(FilePath<T>(fileName));
    }

    public void OpenData<T>(string fileName) where T : class
    {
        Process.Start(FilePath<T>(fileName));
    }

    private string FolderPath<T>() where T : class
    {
        var fileFolder = typeof(T).Name;
        var folderPath = Path.Combine(BaseSavePath, fileFolder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        return folderPath;
    }

    private string FilePath<T>(string fileName) where T : class
    {
        return Path.Combine(FolderPath<T>(), $"{fileName}");
    }

    private SaveSystem<T> GetSaveSystem<T>(string fileName) where T : class
    {
        var path = FilePath<T>(fileName);
        var extension = Path.GetExtension(path);

        return extension switch
        {
            ".xml" => new XmlSaveSystem<T>(),
            ".json" => new JsonSaveSystem<T>(),
            _ => null
        };
    }
}