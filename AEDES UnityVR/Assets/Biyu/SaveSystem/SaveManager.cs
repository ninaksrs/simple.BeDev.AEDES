using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static void SavePlayerData(PlayerSavedData data)
    {
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "playerSaveData.json");

        try
        {
            File.WriteAllText(path, json);
            print("Saved");
        }
        catch (System.Exception e)
        {
            print("AAAAAAAAA");
        }
    }

    public static PlayerSavedData LoadPlayerData()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerSaveData.json");

        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                PlayerSavedData data = JsonUtility.FromJson<PlayerSavedData>(json);
                print("found in"+ path);
                return data;
            }
            catch (System.Exception e)
            {
                print(e.Message);
            }
        }
        else
        {
            print("No file in" + path);
        }

        return null;
    }
}