using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public event Action OpenUI;
    public event Action CloseUI;
    Dictionary<string, Dictionary<string, GameObject>> allUI;

    private void Awake()
    {
        instance = this;
        allUI = new Dictionary<string, Dictionary<string, GameObject>>();

    }
    private void OnEnable()
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.playerData.scenceIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }
    public void RegisterUI(string  panelName,string uIName,GameObject uIObject)
    {
        if (!allUI.ContainsKey(panelName))
        {
            allUI[panelName] = new Dictionary<string, GameObject>();
        }
        allUI[panelName].Add(uIName, uIObject);
    }
    public GameObject GetUI (string panelName,string uIName)
    {
        if (allUI.ContainsKey(panelName))
        {
            return allUI[panelName][uIName];         
        }
        return null;
    }
    public void InvokeOpenUI()
    {
        OpenUI.Invoke();
    }

    public void InvokeCloseUI()
    {
        CloseUI.Invoke();
    }
}
