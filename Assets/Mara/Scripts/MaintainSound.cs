using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainSound : MonoBehaviour
{
  public bool mPlaying = false;

  // Update is called once per frame
  void Update()
  {
    if (mPlaying && !GetComponent<AudioSource>().isPlaying)
    {
      Destroy(gameObject);
    }
  }
}
