using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadGameUIControl : MonoBehaviour
{
    [SerializeField] GameObject saveInputBox;
    [SerializeField] GameObject savePanel;
    public void StartUILoadButtonControl()
    {
        DataPersistenceManager.instance.ChangeDataSourceName(GetComponentInChildren<TMP_Text>().text);
        if (DataPersistenceManager.instance.GetGameData().datas[0].scenceIndex == 0 || DataPersistenceManager.instance.GetGameData().datas[0].scenceIndex == 1)
        {
            StartCoroutine(DataPersistenceManager.instance.LoadGameData());
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        }
        else
        {
            StartCoroutine(DataPersistenceManager.instance.LoadGameData());
            SceneManager.LoadSceneAsync(DataPersistenceManager.instance.GetGameData().datas[0].scenceIndex, LoadSceneMode.Single);
        }
    }
    public void LoadButtonControl()
    {
        DataPersistenceManager.instance.ChangeDataSourceName(GetComponentInChildren<TMP_Text>().text);
        if (DataPersistenceManager.instance.GetGameData().datas[0].scenceIndex == 0 || DataPersistenceManager.instance.GetGameData().datas[0].scenceIndex == 1)
        {
            StartCoroutine(DataPersistenceManager.instance.LoadGameData());
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        }
        else
        {
            StartCoroutine(DataPersistenceManager.instance.LoadGameData());
            SceneManager.LoadSceneAsync(DataPersistenceManager.instance.GetGameData().datas[0].scenceIndex, LoadSceneMode.Single);
        }
    }
    public void SaveButtonControl()
    {
        if (GetComponentInChildren<TMP_Text>().text != "")
        {
            DataPersistenceManager.instance.ChangeDataSourceName(GetComponentInChildren<TMP_Text>().text);
            DataPersistenceManager.instance.SaveGame();
        }
        else
        {
            saveInputBox.SetActive(true);
            savePanel.SetActive(false);
        }
    }
    public void RemoveGameData()
    {
        DataPersistenceManager.instance.RemoveData(GetComponentInChildren<TMP_Text>().text);
    }
}
