using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menuinicio : MonoBehaviour
{
    public GameObject eContinuar;
    public GameObject eOptions;
    public GameObject eCapitulos;

    public void BottonOptions()
    {
        eOptions.SetActive(true);
        eContinuar.SetActive(false);
        eCapitulos.SetActive(false);
    }
    public void BottonLevels()
    {
        eOptions.SetActive(false);
        eContinuar.SetActive(false);
        eCapitulos.SetActive(true);
    }
    public void BottonEscenas()
    {
        eOptions.SetActive(false);
        eContinuar.SetActive(true);
        eCapitulos.SetActive(false);
    }
}