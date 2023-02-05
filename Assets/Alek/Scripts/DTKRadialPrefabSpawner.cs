using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTKRadialPrefabSpawner : DTKPrefabSpawner
{
    public float m_radius;
    public int m_count = 0;
    public bool m_flatSwitch = true;
    int m_X;
    int m_Y;
    int m_Z;

    private float m_time = 0.0f;
    public float m_spawnTime = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        if (m_spawnOnStart)
        {
            for (int i = 0; i < m_count; i++)
            {
                SpawnPrefab();
            }
        }
    }

    private void FixedUpdate()
    {
        m_time += Time.fixedDeltaTime;
        if (m_time >= m_spawnTime)
        {
            m_time -= m_spawnTime;
            SpawnPrefab();
        }
    }

    public override void SpawnPrefab()
    {
        int randomIdx = Random.Range(0, m_prefabList.Count);
        if (m_prefabList[randomIdx] == null)
        {
            return;
        }

        Vector3 offset = Random.insideUnitSphere * (m_radius - 1.5f);
        if (m_flatSwitch)
        {
            offset.z = 0.0f;
        }

        if (offset.y > -1.0f)
        {
            offset.y += 1.0f;
            offset.y *= -1.0f;
        }

        float dist = Mathf.Sqrt(offset.x * offset.x + offset.y * offset.y) + 1.5f;
        float angle = Mathf.Atan2(offset.y, offset.x);
        offset.x = Mathf.Cos(angle) * dist;
        offset.y = Mathf.Sin(angle) * dist;

        GameObject spawnedObject = Instantiate(m_prefabList[randomIdx], transform.position + new Vector3(offset.x, offset.y, offset.z), transform.rotation);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "PrefabIcon", true);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
}
