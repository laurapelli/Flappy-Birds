using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavingSystem : MonoBehaviour
{
    private static SavingSystem m_Instance;

    public int m_HighScore;
    public static SavingSystem INSTANCE
    {
        get { return m_Instance; }
        set { m_Instance = value; }
    }

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this) //If there is another instance of this object in the game it kills itself
        {
            Destroy(gameObject);
        }
        else //If not im the instance
        {
            m_Instance = this;
        }

        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            string highScore = LoadPlayerPrefs("HighScore");
            m_HighScore = Int32.Parse(highScore);
            Debug.Log(highScore);
        }
        else
        {
            SetPlayerPref("HighScore", "0");
            SavePlayerPrefs();
        }
    }
    public string LoadPlayerPrefs(string variable)
    {
        return PlayerPrefs.GetString(variable);
    }

    public void SetPlayerPref(string variable, string value)
    {
        PlayerPrefs.SetString(variable, value);
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.Save();
    }
}
