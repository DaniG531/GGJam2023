using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public string mGoToScene;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(mGoToScene);
    }
}
