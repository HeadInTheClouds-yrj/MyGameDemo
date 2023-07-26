using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderKeyForUI : UIBase
{
    [SerializeField] GameObject crollView;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!crollView.activeSelf)
            {
                crollView.SetActive(true);
                UIManager.instance.InvokeOpenUI();
            }
            else
            {
                crollView.SetActive(false);
                UIManager.instance.InvokeCloseUI();
            }
        }
    }
}
