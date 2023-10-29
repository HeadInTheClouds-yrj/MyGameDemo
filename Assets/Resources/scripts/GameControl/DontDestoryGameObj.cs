using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryGameObj : MonoBehaviour
{
    private static DontDestoryGameObj Instance;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
