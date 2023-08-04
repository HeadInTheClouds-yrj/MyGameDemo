using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderKeyForUI : MonoBehaviour
{
    [SerializeField] GameObject option;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }
}
