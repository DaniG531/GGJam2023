using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalizarcreds : MonoBehaviour
{
    public void cambioEscena()
    {
        SceneManager.LoadScene("menu inicial");
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))

        {
            cambioEscena();
        }

    }
}
