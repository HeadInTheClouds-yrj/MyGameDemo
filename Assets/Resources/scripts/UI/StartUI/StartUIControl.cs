using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartUIControl : UIBase
{
    public void SetColorA()
    {
        Color color1 = transform.GetComponent<Image>().color;
        try
        {
            color1.a = 0.5f;
            transform.GetComponent<Image>().color = color1;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        addlicen("LoadGame_N",SetColorA);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
