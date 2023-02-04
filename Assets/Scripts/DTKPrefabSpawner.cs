using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTKPrefabSpawner : MonoBehaviour
{
    public List<GameObject> m_prefabList;
    public bool m_spawnOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if (m_spawnOnStart)
        {
            SpawnPrefab();
        }
    }

    public virtual void SpawnPrefab()
    {
        int randomIdx = Random.Range(0, m_prefabList.Count);
        if (m_prefabList[randomIdx] == null)
        {
            return;
        }
        GameObject spawnedObject = Instantiate(m_prefabList[randomIdx], transform.position, transform.rotation);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "PrefabIcon", true);
    }
}
