using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
   
    public AudioSource sfxAudioSource;
    public AudioClip[] footStepDirt;


    private void Awake()
    {
        _instance = this;
    }

    public void PlayFootStepAtDirt()
    {
        sfxAudioSource.clip = footStepDirt[Random.Range(0, footStepDirt.Length)];
        sfxAudioSource.Play();
    }
    public void PlayShootSound(AudioClip clip)
    {
        sfxAudioSource.clip = clip;
        sfxAudioSource.Play();
    }
}
