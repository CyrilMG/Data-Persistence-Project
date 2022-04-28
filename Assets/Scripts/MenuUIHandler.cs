using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField playerNameText;

    public void LoadLevel()
    {
        MainManager.Instance.playerName = playerNameText.text;

        SceneManager.LoadScene("main");
    }

    public void LoadHighScore()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
