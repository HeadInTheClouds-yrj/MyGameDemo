using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiscipleBedroom : MonoBehaviour
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
        EventManager.Instance.gameStateEvent.ChangeGameState(State.UI);
        SceneManager.LoadSceneAsync(4,LoadSceneMode.Single);
    }
    public void LeftZongMen()
    {
        EventManager.Instance.gameStateEvent.ChangeGameState(State.BATTLE);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }

    public void LoadGame(GameData gameData)
    {
    }

    public void SaveGame(GameData gameData)
    {
        
    }
}