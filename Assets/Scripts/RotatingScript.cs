using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingScript : MonoBehaviour
{
    public float m_DegreesOnX;
    public float m_DegreesOnY;
    public float m_DegreesOnZ;

    void Update()
    {
        transform.Rotate(m_DegreesOnX, m_DegreesOnY, m_DegreesOnZ);
    }
}
