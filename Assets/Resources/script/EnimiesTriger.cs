using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimiesTriger : MonoBehaviour
{
    public GameObject OgameObject;
    public Transform[] gamobjList;
    // Start is called before the first frame update
    void Start()
    {
        gamobjList = OgameObject.GetComponentsInChildren<Transform>();
        for (int i = 0;i < gamobjList.Length;i++)
        {
            NpcManager.instance.factoryNpc("npcs/Skeleton", gamobjList[i].transform);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
