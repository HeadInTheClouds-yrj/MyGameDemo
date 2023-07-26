using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSaveUI : UIBase
{
    public void LoadDirTextChange()
    {
        List<string> files = DataFileNameManager.Instance.GetAllFileNames();
        int i = 0;
        foreach (string file in files)
        {
            ReplaceText("SaveDataButton(" + i + ")_N", file);
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
