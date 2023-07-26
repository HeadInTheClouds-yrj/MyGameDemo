using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

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
    public void OnEndEditAddLicener(UnityAction<string> action)
    {
        TMP_InputField inputField = transform.GetComponent<TMP_InputField>();
        if (inputField != null)
        {
            inputField.onEndEdit.AddListener(action);
        }
    }
    internal void ReplaceText(string text)
    {
        TMP_Text tMP_Text = transform.GetComponent<TMP_Text>();
        if (tMP_Text != null)
        {
            tMP_Text.text = text;
        }
    }
    internal void OnButtonReplaceText(string text)
    {
        TMP_Text tMP_Text = transform.GetComponentInChildren<TMP_Text>();
        if (tMP_Text != null)
        {
            tMP_Text.text = text;
        }
    }

    internal string GetText()
    {
        TMP_Text tMP_Text = transform.GetComponent<TMP_Text>();
        if (tMP_Text != null)
        {
            return tMP_Text.text;
        }
        return null;
    }
}
