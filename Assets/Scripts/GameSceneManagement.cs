using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManagement : MonoBehaviour
{
    private static GameSceneManagement m_Instance;

    private AdsManager m_AdManager; 
    //propf --> double tab
    public static GameSceneManagement INSTANCE
    {
        get { return m_Instance; }
        set { m_Instance = value; }
    }

    private void Awake()
    {
        if(m_Instance != null && m_Instance != this) //If there is another instance of this object in the game it kills itself
        {
            Destroy(gameObject);
        }
        else //If not im the instance
        {
            m_Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {

        m_AdManager = GetComponent<AdsManager>();
    }

    public void ChangeScene(string name)
    {
        StartCoroutine(LoadSceneCoroutine(name));
    }

    private static void LoadScene(string sceneName)
    {
        m_Instance.StartCoroutine(m_Instance.LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        //we Start loading the loader scene
        AsyncOperation asyncLoadLoader = SceneManager.LoadSceneAsync("Loader", LoadSceneMode.Single);

        while (!asyncLoadLoader.isDone)
        {
            yield return null;
        }

        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        asyncLoadScene.allowSceneActivation = false;

        while (asyncLoadScene.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        asyncLoadScene.allowSceneActivation = true;

        yield return null;

        AsyncOperation asyncUnLoadScene = SceneManager.UnloadSceneAsync("Loader");

        if(sceneName == "MainMenu")
        {
            //m_AdManager
            Time.timeScale = 0;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
