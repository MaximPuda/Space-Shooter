using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(SaveData data)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/game.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/game.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/game.save";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file is deleted!");
        }
    }
}
