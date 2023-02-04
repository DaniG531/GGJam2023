using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownEnemy : MonoBehaviour
{
    public float m_Speed;
    Transform m_Target;

    private void Start()
    {
        m_Target = GameObject.Find("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if(m_Target != null)
        {
            transform.LookAt(m_Target.position,Vector3.back);
            transform.position = Vector2.MoveTowards(transform.position, m_Target.position, m_Speed * Time.deltaTime);
        }
    }
}
