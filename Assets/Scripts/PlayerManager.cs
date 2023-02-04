using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int m_Life;
    float m_Cooldown = 0;
    public float m_MaxCooldown;

    Rigidbody m_Rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == ("Enemy"))
        {
            m_Life -= 1;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        m_Cooldown += Time.deltaTime;

        if (collision.collider.tag == ("Enemy") & m_Cooldown >= m_MaxCooldown)
        {
            m_Rigidbody.velocity = Vector3.zero;
            m_Life -= 1;
            m_Cooldown = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_Cooldown = 0;
    }
}
