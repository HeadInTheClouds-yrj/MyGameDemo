using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimiesTriger : MonoBehaviour
{
    public GameObject OgameObject;
    // Start is called before the first frame update
    void Start()
    {
        NpcManager.instance.factoryNpc("npcs/Skeleton", OgameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
