using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InputNewGameFileNameControl : UIBase
{
    public void ReplaceSLFileName(string value)
    {
        if (value== null || value == "")
        {
            value = "default";
        }
        DataPersistenceManager.instance.ChangeDataSourceName(value);
    }
    public void SaveData()
    {
        DataPersistenceManager.instance.SaveGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        OnEndEditAddLicener("NewGameDataName_N", ReplaceSLFileName);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
