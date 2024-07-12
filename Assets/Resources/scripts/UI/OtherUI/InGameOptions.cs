using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameOptions : UIBase
{
    public void FileNameInit()
    {
        DataFileNameManager.Instance.Init();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void OptionManuQuit()
    {
        Application.Quit();
    }
}
