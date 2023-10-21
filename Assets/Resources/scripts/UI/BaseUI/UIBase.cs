using Microsoft.Cci;
using System;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static TMPro.TMP_InputField;

public class UIBase : MonoBehaviour
{
    Transform[] allPanelUI;
    private void Awake()
    {
        allPanelUI = transform.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < allPanelUI.Length; i++)
        {
            if (allPanelUI[i] != null)
            {
                if (allPanelUI[i].name.EndsWith("_N"))
                {
                    allPanelUI[i].gameObject.AddComponent<UIBehaviour>();
                }
            }
        }

    }
    //private void addUIBehaviourComponent()
    //{
    //    allPanelUI = transform.GetComponentsInChildren<Transform>();
    //    for (int i = 0; i < allPanelUI.Length; i++)
    //    {
    //        if (allPanelUI[i] != null)
    //        {
    //            if (allPanelUI[i].name.EndsWith("_I"))
    //            {
    //                allPanelUI[i].gameObject.AddComponent<UIBehaviour>();
    //            }
    //        }
    //    }
    //}
    public GameObject GetUI(string panelName, string uIName)
    {
        return UIManager.instance.GetUI(panelName, uIName);
    }
    public UIBehaviour GetUIBehaviour( string uIName)
    {
        return UIManager.instance.GetUI(transform.name, uIName).GetComponent<UIBehaviour>();
    }
    public void addlicen(string uIName,UnityAction action)
    {
        UIBehaviour uIBehaviour = GetUIBehaviour(uIName);
        if (uIBehaviour != null)
        {
            uIBehaviour.AddLicener(action);
        }
    }
    public void OnEndEditAddLicener(string uIName,UnityAction<string> action)
    {
        UIBehaviour uIBehaviour = GetUIBehaviour(uIName);
        if (uIBehaviour != null)
        {
            uIBehaviour.OnEndEditAddLicener(action);
        }
    }
    public void ReplaceText(string uIName, string text)
    {
        UIBehaviour uIBehaviour = GetUIBehaviour(uIName);
        if (uIBehaviour != null)
        {
            uIBehaviour.OnButtonReplaceText(text);
        }
    }
    public string GetText(string uIName)
    {
        UIBehaviour uIBehaviour = GetUIBehaviour(uIName);
        if (uIBehaviour != null)
        {
            return uIBehaviour.GetText();
        }
        return null;
    }
    public void OnButtonReplaceText(string uIName,string text)
    {
        UIBehaviour uIBehaviour = GetUIBehaviour(uIName);
        if (uIBehaviour != null)
        {
            uIBehaviour.OnButtonReplaceText(text);
        }
    }
}
