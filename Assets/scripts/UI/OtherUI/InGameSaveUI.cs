using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSaveUI : UIBase
{
    private int LoadButtonCount = 10;
    public void LoadDirTextChange()
    {
        List<string> files = DataFileNameManager.Instance.GetAllFileNames();
        for (int i = 0; i < LoadButtonCount; i++)
        {
            if (i >= files.Count)
            {
                ReplaceText("SaveDataButton(" + i + ")_N", "");
            }
            else
            {
                ReplaceText("SaveDataButton(" + i + ")_N", files[i]);
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