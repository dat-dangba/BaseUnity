using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveGame
{
    private static readonly string FILE_SAVE = "SaveGame.bin";

    public static string LoadData()
    {
        string dataPath = GetPath();
        if (!File.Exists(dataPath))
        {
            return "";
        }

        string dataEncode = File.ReadAllText(dataPath);
        return Decode(dataEncode);
    }

    public static void SaveData(string data)
    {
        File.WriteAllText(GetPath(), Encode(data));
        Debug.LogWarning($"----------SAVE GAME----------\n{GetPath()}");
    }

    public static void ClearData()
    {
        string dataPath = GetPath();
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
        }
    }

    public static string GetPath()
    {
        return Application.persistentDataPath + "/" + FILE_SAVE;
    }

    private static string Encode(string data)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
    }

    private static string Decode(string dataEncode)
    {
        byte[] decodedBytes = Convert.FromBase64String(dataEncode);
        return Encoding.UTF8.GetString(decodedBytes);
    }
}