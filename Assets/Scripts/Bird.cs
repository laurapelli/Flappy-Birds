using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float m_JumpForce;
    [SerializeField] private float m_RotateDegree;
    [SerializeField] private enum BIRD_STATES { Waiting, Flying, Dead};
    [SerializeField] private BIRD_STATES m_CurrentBirdState = BIRD_STATES.Waiting;

    private Rigidbody2D rig;

    public GameManager m_GameManager;

    private bool m_GameOverAlready;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.isKinematic = true;
        m_CurrentBirdState = BIRD_STATES.Waiting;
    }
    
    void Update()
    {
        ChangingStates();
    }

private void ChangingStates()
{
        switch (m_CurrentBirdState)
        {
            case BIRD_STATES.Waiting:
                WaitingFunction();
                break;
            case BIRD_STATES.Flying:
                FlyingFunction();
                break;
            case BIRD_STATES.Dead:
                DeadFunction();
                break;
            default:
                break;
        }
    }

    private void WaitingFunction()
    {
        if(InputPressed())
        {
            m_CurrentBirdState = BIRD_STATES.Flying;
            rig.isKinematic = false;
            m_GameManager.m_GameWorking = true;
            Jump();
        }
    }
    private void FlyingFunction()
    {
        if(InputPressed())
        {
            Jump();
        }
        transform.eulerAngles = new Vector3(0, 0, rig.velocity.y * m_RotateDegree); //Rotating while seeing up/down
    }
    private void DeadFunction()
    {
        if(!m_GameOverAlready)
        {
            m_GameOverAlready = true;
            m_GameManager.GameOver();
        }
    }

    private bool InputPressed()
    {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0;
    }

    private void Jump()
    {
        rig.velocity = Vector2.up * m_JumpForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_GameManager.AddPoingToScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_CurrentBirdState = BIRD_STATES.Dead;
        m_GameManager.m_GameWorking = false;
    }
}
