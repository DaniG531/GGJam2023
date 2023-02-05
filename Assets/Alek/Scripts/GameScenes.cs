using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenes : MonoBehaviour
{
    Scene scene;
    private void Update()
    {
        scene = SceneManager.GetActiveScene();

        if(PlayerManager.m_NutrientCount > 2 & scene.name == "Game Scene 1")
        {
            PlayerManager.m_NutrientCount = 0;
            SceneManager.LoadScene("Game Scene 2");
        }

        if (PlayerManager.m_NutrientCount > 2 & scene.name == "Game Scene 2")
        {
            PlayerManager.m_NutrientCount = 0;
            SceneManager.LoadScene("Game Scene 3");
        }

        if (PlayerManager.m_NutrientCount > 2 & scene.name == "Game Scene 3")
        {
            PlayerManager.m_NutrientCount = 0;
            SceneManager.LoadScene("creditos");
        }

        if(PlayerManager.m_GameOver == true)
        {
            PlayerManager.m_GameOver = false;
            SceneManager.LoadScene("menu inicial");
        }
    }
}
