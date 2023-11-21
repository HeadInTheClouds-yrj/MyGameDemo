using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public event Action OpenUI;
    public event Action CloseUI;
    private Dictionary<string,Dictionary<string,GameObject>> destoredGO;
    Dictionary<string, Dictionary<string, GameObject>> allUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            allUI = new Dictionary<string, Dictionary<string, GameObject>>();
            destoredGO = new Dictionary<string,Dictionary<string,GameObject>>();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += ClearDestoredGameObject;
    }
    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= ClearDestoredGameObject;
    }
    public void RegisterUI(string  panelName,string uIName,GameObject uIObject)
    {
        if (!allUI.ContainsKey(panelName))
        {
            allUI[panelName] = new Dictionary<string, GameObject>();
        }
        if (!allUI[panelName].ContainsKey(uIName))
        {
            allUI[panelName].Add(uIName, uIObject);
        }
    }
    public GameObject GetUI (string panelName,string uIName)
    {
        if (allUI.ContainsKey(panelName))
        {
            return allUI[panelName][uIName];         
        }
        return null;
    }
    private void ClearDestoredGameObject(Scene arg0)
    {
        if (destoredGO == null || allUI == null)
        {
            return;
        }
        destoredGO.Clear();
        foreach (var item in allUI.Keys)
        {
            foreach (var item1 in allUI[item].Keys)
            {
                if (allUI[item][item1].IsDestroyed())
                {
                    if (!destoredGO.ContainsKey(item))
                    {
                        destoredGO[item] = new Dictionary<string, GameObject>();
                    }
                    destoredGO[item].Add(item1, allUI[item][item1]);
                }
            }
        }
        foreach (var item in destoredGO.Keys)
        {
            foreach (var item1 in destoredGO[item].Keys)
            {
                allUI[item].Remove(item1);
            }
        }
    }
    public void InvokeOpenUI()
    {
        if (OpenUI != null)
        {
            OpenUI.Invoke();
        }
    }

    public void InvokeCloseUI()
    {
        if (CloseUI != null)
        {
            CloseUI.Invoke();
        }
    }
}
