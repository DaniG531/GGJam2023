using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystemUnitTest : MonoBehaviour
{
  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.R))
    {
      Debug.Log(SaveSystem.IsGameSaved());
    }
    if (Input.GetKeyDown(KeyCode.T))
    {
      SaveSystem.SaveGameLevel(1);
      Debug.Log("Game Saved");
    }
    if (Input.GetKeyDown(KeyCode.Y))
    {
      Debug.Log(SaveSystem.GetGameLevel());
    }
  }
}
