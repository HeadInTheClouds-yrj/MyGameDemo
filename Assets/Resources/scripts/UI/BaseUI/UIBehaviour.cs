using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    private void Awake()
    {
        UIBase tempBase = transform.GetComponentInParent<UIBase>();
        UIManager.instance.RegisterUI(tempBase.name,transform.name,this.gameObject);
    }
    public void AddLicener(UnityAction action)
    {
        Button image = transform.GetComponent<Button>();
        if (image != null)
        {
            image.onClick.AddListener(action);
        }
    }
}
