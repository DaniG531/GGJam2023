using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int m_Life;
    float m_Cooldown = 0;
    public float m_MaxCooldown;

    float m_Timer;
    public float m_MaxTimer;

    Rigidbody m_Rigidbody;

    public static int m_NutrientCount;
    public static bool m_GameOver;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_MaxTimer)
        {
            m_Timer = 0;
            m_Life -= 1;
        }

        if(m_Life <= 0 | EnemyNutrient.m_NoNutrients == true)
        {
            m_GameOver = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            m_Life -= 1;
        }

        if (collision.collider.tag == ("Nutrient"))
        {
            Destroy(collision.gameObject);

            m_Life += 2;
            if(m_Life > 10)
            {
                m_Life = 10;
            }

            m_NutrientCount += 1;

            gameObject.GetComponent<RootMovement>().AddLength(5);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        m_Cooldown += Time.deltaTime;

        if (other.GetComponent<Collider>().tag == ("Enemy") & m_Cooldown >= m_MaxCooldown)
        {
            m_Rigidbody.velocity = Vector3.zero;
            m_Life -= 1;
            m_Cooldown = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_Cooldown = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            m_Life -= 1;
        }
    }
}
