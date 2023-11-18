using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public SourceManager sourceManager;
    public ClipManager clipManager;
    private void Awake()
    {
        instance= this;
        sourceManager = new SourceManager(gameObject);
        clipManager = new ClipManager();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public AudioSource PlayClip(string audioName)
    {
        AudioSource freeSource = sourceManager.GetFreeSource();
        ClipSingle clipSingle = clipManager.GetClipByName(audioName);
        clipSingle.Play(freeSource);
        return freeSource;
    }
    public AudioSource LoopPlayClip(string audioName)
    {
        AudioSource freeSource = sourceManager.GetFreeSource();
        ClipSingle clipSingle = clipManager.GetClipByName(audioName);
        clipSingle.LoopPlay(freeSource);
        return freeSource;
    }
    public void StopPlay(string audioName,AudioSource audioSource)
    {
        AudioSource freeSource = sourceManager.GetFreeSource();
        ClipSingle clipSingle = clipManager.GetClipByName(audioName);
        clipSingle.StopPlay(audioSource);
    }
    public void StopLoopPlay(string audioName, AudioSource audioSource)
    {
        ClipSingle clipSingle = clipManager.GetClipByName(audioName);
        clipSingle.StopLoopPlay(audioSource);
    }
    public void FreeSource()
    {
        sourceManager.CloseFreeSource();
    }
}
