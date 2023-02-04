using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject m_Target;

    public float m_Speed;

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_Target.transform.position - transform.position), m_Speed * Time.deltaTime);
        transform.position += transform.forward * m_Speed * Time.deltaTime;
    }
}
