using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderKeyForUI : UIBase
{
    private GameObject crollView_N;
    // Start is called before the first frame update
    void Start()
    {
        crollView_N = GetUI(transform.name, "Scroll View_N");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!crollView_N.activeSelf)
            {
                crollView_N.SetActive(true);
                UIManager.instance.InvokeOpenUI();
            }
            else
            {
                crollView_N.SetActive(false);
                UIManager.instance.InvokeCloseUI();
            }
        }
    }
}
