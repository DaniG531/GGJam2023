using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient : MonoBehaviour
{
    float m_Timer = 0;
    public float m_MaxTimer;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "NutrientEnemy")
        {
            m_Timer += Time.deltaTime;

            if(m_Timer >= m_MaxTimer)
            {
                Destroy(gameObject);
            }            
        }
    }

}
