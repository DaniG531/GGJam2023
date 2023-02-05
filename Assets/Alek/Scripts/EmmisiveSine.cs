using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmisiveSine : MonoBehaviour
{
    public Color Color1;
    public Color Color2;
    public Material m_Emmisive;
    float m_Lerp = 0;
    public float m_LerpSpeed = 1;
    bool m_GoingUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(m_GoingUp)
        {
            m_Lerp += Time.deltaTime/m_LerpSpeed;
            if(m_Lerp > 1)
            {
                m_GoingUp = false;
            }
        }
        else if(!m_GoingUp)
        {
            m_Lerp -= Time.deltaTime/m_LerpSpeed;
            if(m_Lerp < 0)
            {
                m_GoingUp = true;
            }
        }

        var ColorLerp = Color.Lerp(Color1, Color2, m_Lerp);

        m_Emmisive.SetVector("_EmissionColor", ColorLerp);
    }
}
