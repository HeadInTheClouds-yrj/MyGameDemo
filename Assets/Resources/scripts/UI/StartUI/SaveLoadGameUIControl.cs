using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadGameUIControl : MonoBehaviour
{
    [SerializeField] GameObject saveInputBox;
    [SerializeField] GameObject savePanel;
    public void LoadButtonControl()
    {
        DataPersistenceManager.instance.ChangeDataSourceName(GetComponentInChildren<TMP_Text>().text);
        StartCoroutine(LoadSenceControl.Instance.OnLoadSenceEnd(1,DataPersistenceManager.instance.LoadGame));
    }
    public void SaveButtonControl()
    {
        string fileName = GetComponentInChildren<TMP_Text>().text;
        Debug.Log(fileName);
        if (fileName != "")
        {
            DataPersistenceManager.instance.ChangeDataSourceName(fileName);
            DataPersistenceManager.instance.SaveGame();
        }
        else
        {
            saveInputBox.SetActive(true);
            savePanel.SetActive(false);
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
