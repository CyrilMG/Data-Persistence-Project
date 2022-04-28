using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using static MainManager;

public class SettingsUIHandler : MonoBehaviour
{
    public TMP_InputField lineCountInputField;

    void Start()
    {
        if(MainManager.Instance != null && MainManager.Instance.settings != null)
        {
            lineCountInputField.text = MainManager.Instance.settings.lineCount.ToString();
        }
    }

    public void BackToMenu()
    {
        Settings settings = new Settings();
        settings.lineCount = int.Parse(lineCountInputField.text);

        MainManager.Instance.settings = settings;
        MainManager.Instance.SaveData();

        SceneManager.LoadScene("Menu");
    }
}
