using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class opciones : MonoBehaviour
{
    public GameObject eOptions;
    public GameObject eReanudar;
    public GameObject eMenu;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void BottonOptions()
    {
        eOptions.SetActive(true);
        eReanudar.SetActive(false);
        eMenu.SetActive(false);
    }
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
