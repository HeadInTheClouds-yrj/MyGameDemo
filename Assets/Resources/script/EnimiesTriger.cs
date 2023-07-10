using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimiesTriger : MonoBehaviour
{
    public GameObject OgameObject;
    public Transform[] gamobjList;
    public Vector3[] vector3s;
    // Start is called before the first frame update
    void Start()
    {
        gamobjList = OgameObject.GetComponentsInChildren<Transform>();
        vector3s = new Vector3[gamobjList.Length];
        for (int i = 0; i < gamobjList.Length; i++)
        {
            vector3s[i] = new Vector3(Random.Range(-4,4), Random.Range(-4, 4),0);
        }
        for (int i = 0;i < gamobjList.Length;i++)
        {
            if (i!=0)
            {
                //NpcManager.instance.factoryNpc("npcs/Skeleton", gamobjList[i].transform, vector3s[i]);
            }
            
            
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
