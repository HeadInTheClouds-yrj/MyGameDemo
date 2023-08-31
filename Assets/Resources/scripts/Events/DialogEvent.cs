using System;
public class DialogEvent
{
    public event Action OnShowDialog;
    public void ShowDailog()
    {
        OnShowDialog();
    }
    public event Action OnHideDialog;
    public void HideDailog() { OnHideDialog(); }
}