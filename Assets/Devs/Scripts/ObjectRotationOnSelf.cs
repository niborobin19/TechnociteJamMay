using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationOnSelf : MonoBehaviour
{
    public Vector3 m_rotation;
    public float m_speed;
    void Update()
    {
        transform.Rotate(m_rotation * m_speed * Time.deltaTime, Space.Self);
    }
}
