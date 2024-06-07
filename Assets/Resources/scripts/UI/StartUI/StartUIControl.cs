using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUIControl : UIBase
{
    private AudioSource audioSource;
    public void FilesNameInit()
    {
        DataFileNameManager.Instance.Init();
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        
    }
    public void StopStartUIAudioLoop()
    {
        AudioManager.instance.StopLoopPlay("WeiWoTianXia", audioSource);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = AudioManager.instance.LoopPlayClip("WeiWoTianXia");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
