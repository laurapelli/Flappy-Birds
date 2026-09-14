using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int m_Score;
    public bool m_GameWorking = false;
    public TextMeshProUGUI m_ScoreText;
    public GameObject m_GameOverText;

    [SerializeField] private float m_MovementSpeed;

    //Ground elements
    [SerializeField] private Transform m_GroundSpawnPoint;
    [SerializeField] private Transform m_GroundDissapearPoint;

    [SerializeField] private Transform[] m_GroundsObjects;

    //Pipes elements
    [SerializeField] private Transform m_PipesSpawnPoint;
    [SerializeField] private Transform m_PipesDissapearPoint;

    [SerializeField] private Transform m_PipesHighestPoint;
    [SerializeField] private Transform m_PipesLowestPoint;

    [SerializeField] private Transform[] m_PipesObjects;

    private bool m_DeadCoroutineCalled;

    void Start()
    {
        m_Score = 0;
        m_ScoreText.text = "Score: " + m_Score.ToString();

        for (int i = 0; i < m_PipesObjects.Length; i++)
        {
            PlacePipesOnYAxis(m_PipesObjects[i]);
        }
    }

    void Update()
    {
        if(m_GameWorking)
        {
            MoveGrounds();
            MovePipes();
        }
    }

    private void MoveGrounds()
    {
        for (int i = 0; i < m_GroundsObjects.Length; i++)
        {
            m_GroundsObjects[i].Translate(Vector3.left * m_MovementSpeed * Time.deltaTime);

            if(m_GroundsObjects[i].position.x <= m_GroundDissapearPoint.position.x)
            {
                float offsetInX = m_GroundsObjects[i].position.x - m_GroundDissapearPoint.position.x;
                m_GroundsObjects[i].position = m_GroundSpawnPoint.position;
                m_GroundsObjects[i].position += new Vector3(offsetInX, 0f, 0f);
            }
        }
    }

    private void MovePipes()
    {
        for (int i = 0; i < m_PipesObjects.Length; i++)
        {
            m_PipesObjects[i].Translate(Vector3.left * m_MovementSpeed * Time.deltaTime);

            if (m_PipesObjects[i].position.x <= m_PipesDissapearPoint.position.x)
            {
                float offsetInX = m_PipesObjects[i].position.x - m_PipesDissapearPoint.position.x;
                m_PipesObjects[i].position = m_PipesSpawnPoint.position;
                m_PipesObjects[i].position += new Vector3(offsetInX, 0f, 0f);
                PlacePipesOnYAxis(m_PipesObjects[i]);
            }
        }
    }

    private void PlacePipesOnYAxis(Transform thisPipes)
    {
        float yPosition = Random.Range(m_PipesLowestPoint.position.y, m_PipesHighestPoint.position.y);

        Vector3 newPos = new Vector3(thisPipes.position.x, yPosition, thisPipes.position.z);

        thisPipes.position = newPos;
    }

    public void AddPoingToScore()
    {
        m_Score++;
        m_ScoreText.text = "Score: " + m_Score.ToString();
    }

    public void GameOver()
    {
        if(!m_DeadCoroutineCalled)
        {
            m_DeadCoroutineCalled = true;
            StartCoroutine(GameOverCoroutine());
        }
    }

    private IEnumerator GameOverCoroutine()
    {
        if(SavingSystem.INSTANCE.m_HighScore < m_Score)
        {
            SavingSystem.INSTANCE.SetPlayerPref("HighScore", m_Score.ToString());
            SavingSystem.INSTANCE.SavePlayerPrefs();
        }

        yield return new WaitForSeconds(2f);

        m_GameOverText.SetActive(true);

        yield return new WaitForSeconds(2f);

        GameSceneManagement.INSTANCE.ChangeScene("MainMenu");
    }
}
