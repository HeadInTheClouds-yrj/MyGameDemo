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
    public void LoopPlay(AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void StopPlay(AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Stop();
    }
    public void StopLoopPlay(AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Stop();
        audioSource.loop = false;
    }
}
