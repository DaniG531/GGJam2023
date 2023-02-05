using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNutrient : MonoBehaviour
{
    public GameObject m_Target;

    public GameObject[] m_Targets;

    public float m_Speed;

    float m_Timer = 0;
    float m_MaxTimer = 3f;

    public static bool m_NoNutrients = false;

    // Update is called once per frame
    void Update()
    {
        m_Targets = GameObject.FindGameObjectsWithTag("Nutrient");
        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (m_Targets[i] != null)
            {
                m_Target = m_Targets[i];
                break;
            }
        }

        if (m_Targets.Length > 0)
        {
            
            transform.LookAt(m_Target.transform.position, Vector3.back);
            transform.position = Vector2.MoveTowards(transform.position, m_Target.transform.position, m_Speed * Time.deltaTime);
        }

        else
        {            

            m_Timer += Time.deltaTime;

            if(m_Timer > m_MaxTimer)
            {
                Destroy(gameObject);
            }

            m_NoNutrients = true;

        }

    }

}
