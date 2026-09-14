using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public TextMeshProUGUI m_HighScore;
    public void PlayButton()
    {
        GameSceneManagement.INSTANCE.ChangeScene("Game");
    } 
    public void ExitButton()
    {
        Debug.Log("ExitButton");
        Application.Quit();
    }

    private void Start()
    {
        m_HighScore.text = "HighScore: " + SavingSystem.INSTANCE.LoadPlayerPrefs("HighScore");
    }

}
