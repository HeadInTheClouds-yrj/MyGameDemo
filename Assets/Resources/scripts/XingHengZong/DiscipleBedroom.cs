using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiscipleBedroom : MonoBehaviour
{
    [SerializeField] private GameObject option;
    private void Awake()
    {
    }
    private void Start()
    {
        //UIManager.instance.InvokeCloseUI();
    }
    private void OnEnable()
    {
        EventManager.Instance.inputEvent.OnGetKeyESC += ScenceInputControl;
    }
    private void OnDisable()
    {
        EventManager.Instance.inputEvent.OnGetKeyESC -= ScenceInputControl;
    }
    private void ScenceInputControl()
    {
        if (!option.activeSelf)
        {
            option.SetActive(true);
        }
        else
        {
            option.SetActive(false);
        }
    }
    public void ToDiZiRoom()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(3,LoadSceneMode.Single);
    }
    public void LeftZongMen()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}