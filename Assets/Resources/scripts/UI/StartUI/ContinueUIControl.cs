using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ContinueUIControl : UIBase
{
    //Find in C:\Users\Administrator\AppData\LocalLow\DefaultCompany\MyGameDemo All Files;
    [SerializeField] private int LoadButtonCount = 10;
    public void LoadDirTextChange()
    {
        List<string> files = DataFileNameManager.Instance.GetAllFileNames();
        for (int i = 0; i < LoadButtonCount; i++)
        {
            if (i >= files.Count)
            {
                ReplaceText("LoadDataButton(" + i + ")_N", "");
            }
            else
            {
                ReplaceText("LoadDataButton(" + i + ")_N", files[i]);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
