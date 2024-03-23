using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using UnityEditor;

public class SaveManager : BaseManager
{
    private static string Pasta => UnityEngine.Application.persistentDataPath;
    private static string BaseSavePath => Path.Combine(Pasta, "Save");

    public void Init()
    {
        if (!Directory.Exists(BaseSavePath))
        {
            Directory.CreateDirectory(BaseSavePath);
        }
    }

    //Saves into Model T
    public void SaveData<T>(ISavable<T> savable) where T : class
    {
        var data = savable.SaveData();
        var fileName = savable.Id;

        var saveSystem = GetSaveSystem<T>(fileName);
        saveSystem.SaveFile(FilePath<T>(fileName), data);
    }

    //Loads data from model T into into Domain based on class Id
    public void LoadData<T>(ILoadable<T> loadable) where T : class
    {
        var data = LoadFile<T>(loadable.Id);

        if (data == null)
            return;

        loadable.LoadData(data);
    }

    //Loads data from model T into into Domain based on provided file name
    public void LoadData<T>(ILoadable<T> loadable, string fileName) where T : class
    {
        var data = LoadFile<T>(fileName);

        if (data == null)
            return;

        loadable.LoadData(data);
    }

    //Load file into T
    public T LoadFile<T>(string fileName) where T : class
    {
        var saveSystem = GetSaveSystem<T>(fileName);
        return saveSystem?.LoadFile(FilePath<T>(fileName));
    }

    //Loads list of file names of type T
    public List<string> LoadFilesNames<T>() where T : class
    {
        return Directory.GetFiles(FolderPath<T>()).ToList();
    }

    //Loads list of files of type T
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

    //Delete file of type T based on file name
    public void DeleteData<T>(string fileName) where T : class
    {
        File.Delete(FilePath<T>(fileName));
    }
    
    //Delete file of type T based on file Id
    public void DeleteData<T>(ILoadable<T> loadable) where T : class
    {
        File.Delete(FilePath<T>(loadable.Id));
    }

    //Open file of type T based on file name
    public void OpenData<T>(string fileName) where T : class
    {
        Process.Start(FilePath<T>(fileName));
    }
    
    //Open file of type T based on file Id
    public void OpenData<T>(ILoadable<T> loadable) where T : class
    {
        Process.Start(FilePath<T>(loadable.Id));
    }
    
    //Clear Directory
#if UNITY_EDITOR
    [MenuItem("Window/Clear Folder")]
#endif
    public static void Clear()
    {
        Directory.Delete(BaseSavePath, true);
        Directory.CreateDirectory(BaseSavePath);
    }

    private string FilePath<T>(string fileName) where T : class
    {
        return Path.Combine(FolderPath<T>(), $"{fileName}");
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

    private SaveSystem<T> GetSaveSystem<T>(string fileName) where T : class
    {
        var path = FilePath<T>(fileName);
        var extension = Path.GetExtension(path);

        return extension switch
        {
            ".xml" => new XmlSaveSystem<T>(),
            ".json" => new JsonSaveSystem<T>(),
            ".bin" => new BinarySaveSystem<T>(),
            _ => throw new NotImplementedException()
        };
    }
}