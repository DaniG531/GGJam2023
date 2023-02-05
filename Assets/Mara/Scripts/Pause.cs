using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool mIsPaused = false;
    public GameObject mUI;

    public void Pausar()
    {
        if (mUI != null) mUI.SetActive(true);
        Time.timeScale = 0.0f;
        mIsPaused = true;
    }
    public void Reanudar()
    {
        if (mUI != null) mUI.SetActive(false);
        Time.timeScale = 1.0f;
        mIsPaused = false;
    }
    public void Menu()
    {
        SceneManager.LoadScene("menu inicial");
        Time.timeScale = 1.0f;
        mIsPaused = false;
    }

  public void Test()
  {
    Debug.Log("Pressed");
  }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mIsPaused)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }
}
