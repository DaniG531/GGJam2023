using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveToFile : MonoBehaviour
{
    public int mLevel = 0;
    void Start()
    {
        if (SaveSystem.IsGameSaved() && SaveSystem.GetGameLevel() < mLevel || !SaveSystem.IsGameSaved())
        {
          SaveSystem.SaveGameLevel(mLevel);
        }
    }
}
