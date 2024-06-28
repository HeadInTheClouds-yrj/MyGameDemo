using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputSaveFileName : UIBase
{
    [SerializeField] private TMP_InputField m_Text;
    public void SaveData()
    {
        DataPersistenceManager.instance.ChangeDataSourceName(m_Text.text);
        DataPersistenceManager.instance.SaveGame();
    }
}
