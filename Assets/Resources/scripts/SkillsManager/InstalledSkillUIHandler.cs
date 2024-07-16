using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InstalledSkillUIHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text displayKeyCode;
    private SkillInfoSO info;
    private bool flag = false;
    private List<RectTransform> readOnly_skillInstallButtons;
    public void SetSkillInfoSO(SkillInfoSO info)
    {
        this.info = info;
    }
    public SkillInfoSO GetCurrentInfo()
    {
        return this.info;
    }
    public void SetSkillInputKeyCode()
    {
        flag = true;
        readOnly_skillInstallButtons = PropertyManuCtrl.instance.GetSkillRectTransforms();
        foreach (RectTransform item in readOnly_skillInstallButtons)
        {
            item.GetComponent<Button>().interactable = false;
        }
        StartCoroutine(RebindKey());
    }
    IEnumerator RebindKey()
    {
        while (flag)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    if (SkiilManager.Instance.ChangeSkillKeyBind(GetComponent<RectTransform>(), key))
                    {
                        SetDisplayKeyCode(key);
                    }
                    foreach(RectTransform item in readOnly_skillInstallButtons)
                    {
                        item.GetComponent<Button>().interactable = true;
                    }
                    flag = false;
                    GetComponent<Button>().interactable = true;
                }
            }
            yield return null;
        }
    }
    public void SetDisplayKeyCode(KeyCode key)
    {
        displayKeyCode.text = key.ToString();
    }
    public void SetDisplayKeyCode(string key)
    {
        displayKeyCode.text = key.ToString();
    }
}
