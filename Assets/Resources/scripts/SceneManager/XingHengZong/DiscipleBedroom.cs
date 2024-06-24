using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiscipleBedroom : MonoBehaviour,IDataPersistence
{
    private void Awake()
    {
    }
    private void Start()
    {
        //UIManager.instance.InvokeCloseUI();
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
    private void ScenceInputControl()
    {
    }
    public void ToDiZiRoom()
    {
        SceneManager.LoadSceneAsync(4,LoadSceneMode.Single);
    }
    public void LeftZongMen()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }

    public void LoadGame(GameData gameData)
    {
        PlayerManager.instance.playerData.scenceIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void SaveGame(GameData gameData)
    {
        
    }
}