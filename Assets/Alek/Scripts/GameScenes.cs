using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenes : MonoBehaviour
{
    public int mNutrientWinCount = 5;
    public string mNextLevelScene = "";

    public PlayerManager mPlayer;

    Scene scene;
    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (mPlayer.m_NutrientCount >= mNutrientWinCount)
        {
            mPlayer.m_NutrientCount = 0;
            SceneManager.LoadScene(mNextLevelScene);
        }

        if(PlayerManager.m_GameOver == true)
        {
            PlayerManager.m_GameOver = false;
            SceneManager.LoadScene("menu inicial");
        }
    }
  //scene.name == "Game Scene 1"
  //                                  ? "Game Scene 2"
  //                                  : (scene.name == "Game Scene 2"
  //                                    ? "Game Scene 3"
  //                                    : (scene.name == "Game Scene 3"
  //                                      ? "creditos"
  //                                      : "")))
}
