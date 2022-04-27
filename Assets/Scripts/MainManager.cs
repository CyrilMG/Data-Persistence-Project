using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public string playerName;

    public string bestScorePlayerName;
    public int bestScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    [System.Serializable]
    class PersistantData
    {
        public string bestScorePlayerName;
        public int bestScore;
    }

    public void SaveData(int newScore)
    {
        if (newScore > bestScore)
        {
            PersistantData data = new PersistantData();
            data.bestScorePlayerName = playerName;
            data.bestScore = newScore;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        LoadData();
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PersistantData data = JsonUtility.FromJson<PersistantData>(json);

            bestScorePlayerName = data.bestScorePlayerName;
            bestScore = data.bestScore;
        }
    }

}
