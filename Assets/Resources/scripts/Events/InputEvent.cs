using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvent
{
    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        if (onSubmitPressed != null&&Input.GetKeyDown(KeyCode.V))
        {
            onSubmitPressed();
        }
    }
    public event Action OnGetKeyESC;
    public void GetKeyESC()
    {
        if (OnGetKeyESC != null && Input.GetKeyDown(KeyCode.Escape))
        {
            OnGetKeyESC();
        }
    }
}
