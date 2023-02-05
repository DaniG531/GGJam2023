using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidesCinematic : MonoBehaviour
{
  public List<GameObject> mSlides;
  int mIndex = 0;

  public string mGoToScene;

  void Start()
  {
    mSlides[0].SetActive(true);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.anyKeyDown)
    {
      mSlides[mIndex++].SetActive(false);

      if (mIndex == mSlides.Count)
      {
        SceneManager.LoadScene(mGoToScene);
        return;
      }

      mSlides[mIndex].SetActive(true);
    }
  }
}
