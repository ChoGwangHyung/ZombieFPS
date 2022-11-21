using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour
{
    private static DataController dataController;
    public static DataController instance
    {
        get
        {
            if (dataController == null)
                dataController = FindObjectOfType<DataController>();
            return dataController;
        }
    }

    private string gameDataFileName = "saveData.json";

    private static GameData gameData;
    public GameData gameDataInstance
    {
        get
        {
            if (gameData == null)
            {
                LoadData();
                SaveData();
            }
            return gameData;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + gameDataFileName;

        if(File.Exists(path))
        {
            string fromJsonData = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(fromJsonData);
            gameData.LoadData();
        }
        else
        {
            gameData = new GameData();                        
        }
    }

    public void SaveData()
    {
        gameData.SaveData();

        string toJsonData = JsonUtility.ToJson(gameData);
        string path = Application.persistentDataPath + gameDataFileName;
        File.WriteAllText(path, toJsonData);        
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
