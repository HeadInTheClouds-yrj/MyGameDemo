using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ContinueUIControl : UIBase
{
    //Find in C:\Users\Administrator\AppData\LocalLow\DefaultCompany\MyGameDemo All Files;

    public void LoadDirTextChange()
    {
        List<string> files = DataFileNameManager.Instance.GetAllFileNames();
        int i = 0;
        foreach (string file in files)
        {
            ReplaceText("LoadDataButton("+i+")_N", file);
            i++;
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
