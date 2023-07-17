using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UIBase : MonoBehaviour
{
    Transform[] allPanelUI;
    private void Awake()
    {
        allPanelUI = transform.GetComponentsInChildren<Transform>();
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
}
