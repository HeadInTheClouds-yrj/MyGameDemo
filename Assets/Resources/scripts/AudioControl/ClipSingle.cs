using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipSingle
{
    public AudioClip clip;
    public ClipSingle(AudioClip audioClip)
    {
        clip= audioClip;
    }
    public void Play(AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void KeepPlay(AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.loop = true;
    }
}
