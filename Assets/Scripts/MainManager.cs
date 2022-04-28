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

    public List<HighScore> bestScores = new List<HighScore>();

    public Settings settings;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetInitialSettings();

        LoadData();
    }

    [System.Serializable]
    class PersistantData
    {
        public List<HighScore> highScores;
        public Settings settings;
    }

    [System.Serializable]
    public class HighScore
    {
        public string bestScorePlayerName;
        public int bestScore;
    }

    [System.Serializable]
    public class Settings
    {
        public int lineCount;
    }

    public void AddNewScore(int newScore)
    {

        int rank = -1;
        if (bestScores.Count == 0)
        {
            rank = 0;
        }
        else
        {
            for (int i = 0; i < bestScores.Count; i++)
            {
                if (newScore > bestScores[i].bestScore)
                {
                    rank = i;
                    break;
                }
            }
        }

        if (rank == -1 && bestScores.Count < 10)
            rank = bestScores.Count;

        if (rank != -1)
        {
            HighScore highScore = new HighScore();
            highScore.bestScore = newScore;
            highScore.bestScorePlayerName = playerName;

            bestScores.Insert(rank, highScore);

            if (bestScores.Count > 10)
                bestScores.RemoveAt(10);

            SaveData();
        }
    }

    public void SaveData()
    {
        PersistantData data = new PersistantData();
        data.highScores = bestScores;
        data.settings = settings;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PersistantData data = JsonUtility.FromJson<PersistantData>(json);

            bestScores = data.highScores;
            settings = data.settings;

        }
    }

    Settings SetInitialSettings()
    {
        Settings settings = new Settings();
        settings.lineCount = 6;
        return settings;
    }
}
