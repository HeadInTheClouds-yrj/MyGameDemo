using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadGameUIControl : MonoBehaviour
{
    [SerializeField] Transform loadPanel;
    [SerializeField] GameObject saveInputBox;
    [SerializeField] GameObject savePanel;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform iconPanel_N;
    [SerializeField] private Transform openOptionsManu_N;
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
        loadPanel.GetComponent<SceneLoad>().SceneLoadHandle(GetComponentInChildren<TMP_Text>().text);
        
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
    public void CloseLoadPanel()
    {
        iconPanel_N.gameObject.SetActive(true);
        openOptionsManu_N.gameObject.SetActive(true);
        canvas.sortingOrder = 3;
    }
}
