using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    Dictionary<string, Dictionary<string, GameObject>> allUI;

    private void Awake()
    {
        instance = this;
        allUI = new Dictionary<string, Dictionary<string, GameObject>>();

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
}
