using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLoadUIControl : UIBase
{
    [SerializeField] private int LoadButtonCount = 10;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform iconPanel_N;
    [SerializeField] private Transform openOptionsManu_N;
    public void LoadDirTextChange()
    {
        List<string> files = DataFileNameManager.Instance.GetAllFileNames();
        for (int i = 0; i < LoadButtonCount; i++)
        {
            if (i>=files.Count)
            {
                ReplaceText("LoadDataButton(" + i + ")_N", "");
            }
            else
            {
                ReplaceText("LoadDataButton(" + i + ")_N", files[i]);
            }
        }
    }
    public void CloseLoadPanel()
    {
        iconPanel_N.gameObject.SetActive(true);
        openOptionsManu_N.gameObject.SetActive(true);
        canvas.sortingOrder = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
