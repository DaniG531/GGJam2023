using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menuinicio : MonoBehaviour
{
  public GameObject eContinuar;
  public GameObject eOptions;
  public GameObject eCapitulos;

  public Button mContinueButton;
  public TMP_Text mContinueText;

  public Button mLvl1Button;
  public TMP_Text mLvl1BText;
  public Button mLvl2Button;
  public TMP_Text mLvl2BText;
  public Button mLvl3Button;
  public TMP_Text mLvl3BText;

  private void Start()
  {
    if (SaveSystem.IsGameSaved())
    {
      mContinueButton.interactable = true;
      mContinueText.color = Color.white;

      int lvl = SaveSystem.GetGameLevel();
      if (lvl >= 1)
      {
        mLvl1Button.interactable = true;
        mLvl1BText.color = Color.white;
      }
      if (lvl >= 2)
      {
        mLvl2Button.interactable = true;
        mLvl2BText.color = Color.white;
      }
      if (lvl >= 3)
      {
        mLvl3Button.interactable = true;
        mLvl3BText.color = Color.white;
      }
    }
  }

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

  public void Continuar()
  {
    SceneManager.LoadScene("Lvl_" + SaveSystem.GetGameLevel().ToString());
  }

  public void NuevoJuego()
  {
    SaveSystem.SaveGameLevel(1);
    SceneManager.LoadScene("IntroCinematic");
  }
}