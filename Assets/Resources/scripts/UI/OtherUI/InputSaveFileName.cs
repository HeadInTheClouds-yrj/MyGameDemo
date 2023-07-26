using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSaveFileName : UIBase
{
    public void ChangeSLFileName(string value)
    {
        DataPersistenceManager.instance.ChangeDataSourceName(value);
    }
    public void SaveData()
    {
        DataPersistenceManager.instance.SaveGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        OnEndEditAddLicener("NewGameDataName_N", ChangeSLFileName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
