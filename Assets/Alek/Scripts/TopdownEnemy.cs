using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownEnemy : MonoBehaviour
{
    public float m_Speed;
    Transform m_Target;
    float m_Timer = 0;
    float m_MaxTimer = 3f;
    Vector3 m_DistanceToPlayer;
    GameObject m_Player;
    public bool m_Idle = false;

    private void Start()
    {
        m_Target = GameObject.Find("Player").GetComponent<Transform>();
        m_Player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if(m_Idle == false)
        {
            transform.LookAt(m_Target.position,Vector3.back);
            transform.position = Vector2.MoveTowards(transform.position, m_Target.position, m_Speed * Time.deltaTime);
        }

        if(m_Idle == true)
        {
            m_Timer += Time.deltaTime;
            m_DistanceToPlayer = m_Target.position - transform.position;

            if(m_Timer > m_MaxTimer & m_DistanceToPlayer.magnitude < 3)
            {
                m_Idle = false;
                m_Timer = 0.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_Idle = true;

        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}
