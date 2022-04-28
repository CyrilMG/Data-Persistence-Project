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
        public List<HighScore> highScores;
    }

    [System.Serializable]
    public class HighScore
    {
        public string bestScorePlayerName;
        public int bestScore;
    }

    public void SaveData(int newScore)
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

        if(rank != -1)
        {
            HighScore highScore = new HighScore();
            highScore.bestScore = newScore;
            highScore.bestScorePlayerName = playerName;

            bestScores.Insert(rank, highScore);

            if (bestScores.Count > 10)
                bestScores.RemoveAt(10);

            PersistantData data = new PersistantData();
            data.highScores = bestScores;


            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

            LoadData();
        }

    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            bestScores = JsonUtility.FromJson<PersistantData>(json).highScores;

        }
    }

}
