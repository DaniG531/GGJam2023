using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
  public GameObject mLevels;
  public GameObject mOptions;

  public void Botton1()
  {
    if (mOptions.activeSelf)
    {
      mOptions.SetActive(false);
    }
    mLevels.SetActive(true);
  }
  public void Botton2()
  {
    if (mLevels.activeSelf)
    {
      mLevels.SetActive(false);
    }
    mOptions.SetActive(true);
  }
}
