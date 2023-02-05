using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AUDIOSOURCES
{
  SFX,
  Music,
  Ambient,
  UI
}

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  [Header("Audio Source References")]
  public AudioSource m_musicAudioSource;
  public AudioSource m_ambientAudioSource;
  public AudioSource m_uiAudioSource;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(instance);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void PlaySound(AudioSource _audioSource, AudioClip _clip, float _volume = 1.0f)
  {
    _audioSource.PlayOneShot(_clip, _volume);
  }

  public void PlaySoundRandomPitch(AudioSource _audioSource, AudioClip _clip, float _volume = 1.0f, float _pitchMin = 0.5f, float _pitchMax = 1.5f)
  {
    _audioSource.pitch = Random.Range(_pitchMin, _pitchMax);
    _audioSource.PlayOneShot(_clip, _volume);
  }

  //play or change current music
  public void PlayMusic(AudioClip clip, bool loop = false, float _volume = 1.0f)
  {
    m_musicAudioSource.Stop();
    m_musicAudioSource.volume = _volume;
    m_musicAudioSource.clip = clip;
    m_musicAudioSource.time = 0.0f;
    m_musicAudioSource.loop = loop;
    m_musicAudioSource.Play();
  }

  //play or change current ambient clip
  public void PlayAmbient(AudioClip clip, bool loop = false, float _volume = 1.0f)
  {
    m_ambientAudioSource.Stop();
    m_ambientAudioSource.volume = _volume;
    m_ambientAudioSource.clip = clip;
    m_musicAudioSource.loop = loop;
    m_ambientAudioSource.Play();
  }
}
