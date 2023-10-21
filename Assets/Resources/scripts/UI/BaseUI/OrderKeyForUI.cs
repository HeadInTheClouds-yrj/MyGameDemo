using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderKeyForUI : MonoBehaviour
{
    [SerializeField] private GameObject option;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        EventManager.Instance.inputEvent.OnGetKeyESC += GetKeyESC;
    }
    private void OnDisable()
    {
        EventManager.Instance.inputEvent.OnGetKeyESC += GetKeyESC;
    }

    public void GetKeyESC()
    {
        if (!option.activeSelf)
        {
            option.SetActive(true);
            UIManager.instance.InvokeOpenUI();
        }
        else
        {
            option.SetActive(false);
            UIManager.instance.InvokeCloseUI();
        }
    }
    public GameObject GetOptionObj()
    {
        return option;
    }
    public void ManuForSetUpButton()
    {
        UIManager.instance.InvokeOpenUI();
    }
    public void ManuForOption()
    {
        UIManager.instance.InvokeCloseUI();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
