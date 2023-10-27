using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartUIControl : UIBase
{
    private AudioSource audioSource;
    public void SetColorA()
    {
        Color color1 = transform.GetComponent<Image>().color;
        try
        {
            color1.a = 0.5f;
            transform.GetComponent<Image>().color = color1;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

    }
    public void SetColorAOpaque()
    {
        Color color1 = transform.GetComponent<Image>().color;
        try
        {
            color1.a = 1f;
            transform.GetComponent<Image>().color = color1;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

    }
    public void FilesNameInit()
    {
        DataFileNameManager.Instance.Init();
    }
    private void OnEnable()
    {
        audioSource = AudioManager.instance.LoopPlayClip("WeiWoTianXia");
    }
    private void OnDestroy()
    {
        AudioManager.instance.StopLoopPlay("WeiWoTianXia", audioSource);
    }
    // Start is called before the first frame update
    void Start()
    {
        addlicen("LoadGame_N",SetColorA);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
