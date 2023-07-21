using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InputNewGameFileNameControl : UIBase
{
    UnityAction<string> OnNewGameFileName;
    public void test(string txt)
    {
        Debug.Log(txt);
    }
    public void ReplaceSLFileName()
    {
        DataPersistenceManager.instance.dataFileName = GetUI("InputNewGameFileName", "Text_N").GetComponent<TMP_Text>().text;
    }
    // Start is called before the first frame update
    void Start()
    {
        OnNewGameFileName = test;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
