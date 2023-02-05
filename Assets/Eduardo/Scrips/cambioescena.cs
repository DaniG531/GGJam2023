using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cambioescena : MonoBehaviour
{
    public int numeroEscena;

    public Canvas pause;


    public void cambioEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
       
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }

    private void Update()
    {
        if (numeroEscena == 2)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                
            {
                if (pause.enabled == true)
                        {
                            pause.enabled = false;
                }

                else
                {
                    pause.enabled = true;
                }
            }
        }
    }
}
