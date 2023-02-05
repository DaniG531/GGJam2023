using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTKSpawnerInBox : DTKPrefabSpawner
{
    public Vector3 m_size;
    public int m_count = 0;

    
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

    public override void SpawnPrefab()
    {
        int randomIdx = Random.Range(0, m_prefabList.Count);
        if (m_prefabList[randomIdx] == null)
        {
            return;
        }

        Vector3 offset = Vector3.zero;
        offset.x = Random.Range(-m_size.x * 0.5f, m_size.x * 0.5f);
        offset.y = Random.Range(-m_size.y * 0.5f, m_size.y * 0.5f);
        offset.z = Random.Range(-m_size.z * 0.5f, m_size.z * 0.5f);
        offset = transform.TransformDirection(offset);

        GameObject spawnedObject = Instantiate(m_prefabList[randomIdx], transform.position + offset, transform.rotation);
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "PrefabIcon", true);
        Gizmos.color = Color.blue;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, m_size);
    }
}
