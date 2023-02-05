using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressGame : MonoBehaviour
{
    public PlayerManager mPlayer;
    public GameScenes mGameScene;

    public Slider mHealthBar;
    public Slider mProgressBar;


    private void Update()
    {
        mHealthBar.minValue = 0.0f;
        mHealthBar.maxValue = 10.0f;
        if (mGameScene != null)
        {
          mProgressBar.minValue = 0.0f;
          mProgressBar.maxValue = mGameScene.mNutrientWinCount;
        }

        if (mPlayer != null)
        {
            mHealthBar.value = mPlayer.m_Life;
            mProgressBar.value = PlayerManager.m_NutrientCount;
        }
    }
}
