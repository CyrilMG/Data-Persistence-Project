using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainManager;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScoreUIHandler : MonoBehaviour
{
    [SerializeField] GameObject highScoreContainer;
    [SerializeField] GameObject highScoreTextPrefab;

    void Start()
    {
        if(MainManager.Instance != null)
        {
            int rank = 1;
            foreach(HighScore score in MainManager.Instance.bestScores)
            {
                if(score != null)
                {
                    GameObject scoreText = Instantiate(highScoreTextPrefab, highScoreContainer.transform);
                    scoreText.GetComponent<TextMeshProUGUI>().text = rank + " - " + score.bestScorePlayerName + " : " + score.bestScore;
                    rank++;
                }
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
