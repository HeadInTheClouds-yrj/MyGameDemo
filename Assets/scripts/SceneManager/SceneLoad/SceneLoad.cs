using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void SceneLoadHandle(string fielName)
    {
        DataPersistenceManager.instance.ChangeDataSourceName(fielName);
        DataPersistenceManager.instance.SetIsSceneChanged();
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
        
}
