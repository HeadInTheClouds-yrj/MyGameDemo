using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartUIControl : UIBase
{
    private AudioSource audioSource;
    [SerializeField] private GameObject bg1;
    [SerializeField] private GameObject bg2;
    public void SetColorA()
    {
        Color color1 = transform.GetComponent<Image>().color;
        try
        {
            color1.a = 0.5f;
            transform.GetComponent<Image>().color = color1;
            bg1.SetActive(false);
            bg2.SetActive(false);
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
            bg1.SetActive(true);
            bg2.SetActive(true);
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
    private void OnDisable()
    {
        
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
