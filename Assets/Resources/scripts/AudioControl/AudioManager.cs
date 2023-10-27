using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    SourceManager sourceManager;
    ClipManager clipManager;
    private void Awake()
    {
        instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        sourceManager = new SourceManager(gameObject);
        clipManager = new ClipManager();
        KeepPlayClip("WeiWoTianXia");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayClip(string audioName)
    {
        AudioSource freeSource = sourceManager.GetFreeSource();
        ClipSingle clipSingle = clipManager.GetClipByName(audioName);
        clipSingle.Play(freeSource);
    }
    public void KeepPlayClip(string audioName)
    {
        AudioSource freeSource = sourceManager.GetFreeSource();
        ClipSingle clipSingle = clipManager.GetClipByName(audioName);
        clipSingle.KeepPlay(freeSource);
    }
}
