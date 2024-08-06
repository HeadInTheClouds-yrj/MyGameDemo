using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public event Action OnGetLeftMouseDown;
    public void GetLeftMouseDown()
    {
        if (OnGetLeftMouseDown !=null && Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            OnGetLeftMouseDown();
        }
    }
    public event Action OnGetLeftMouseUp;
    public void GetLeftMouseUp()
    {
        if (OnGetLeftMouseUp != null && Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            OnGetLeftMouseUp();
        }
    }
    public event Action OnGetLeftMouse;
    public void GetLeftMouse()
    {
        if (OnGetLeftMouse != null && Input.GetMouseButton((int)MouseButton.Left))
        {
            OnGetLeftMouse();
        }
    }
    public event Action OnGetRightMouseDown;
    public void GetRightMouseDown()
    {
        if (OnGetRightMouseDown != null && Input.GetMouseButtonDown((int)MouseButton.Right))
        {
            OnGetRightMouseDown();
        }
    }
    public event Action OnGetRightMouseUp;
    public void GetRightMouseUp()
    {
        if (OnGetRightMouseUp != null && Input.GetMouseButtonUp((int)MouseButton.Right))
        {
            OnGetRightMouseUp();
        }
    }
    public event Action OnGetRightMouse;
    public void GetRightMouse()
    {
        if (OnGetRightMouse != null && Input.GetMouseButton((int)MouseButton.Right))
        {
            OnGetRightMouse();
        }
    }
    public event Action OnGetMiddleMouseDown;
    public void GetMiddleMouseDown()
    {
        if (OnGetMiddleMouseDown != null && Input.GetMouseButtonDown((int)MouseButton.Middle))
        {
            OnGetMiddleMouseDown();
        }
    }
    public event Action OnGetMiddleMouseUp;
    public void GetMiddleMouseUp()
    {
        if (OnGetMiddleMouseUp != null && Input.GetMouseButtonUp((int)MouseButton.Middle))
        {
            OnGetMiddleMouseUp();
        }
    }
    public event Action OnGetMiddleMouse;
    public void GetMiddleMouse()
    {
        if (OnGetMiddleMouse != null && Input.GetMouseButton((int)MouseButton.Middle))
        {
            OnGetMiddleMouse();
        }
    }

}
