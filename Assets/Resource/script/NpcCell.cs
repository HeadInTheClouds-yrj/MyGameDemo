using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCell : MonoBehaviour
{
    public NpcData npcData;
    // Start is called before the first frame update
    void Start()
    {
        npcData= new NpcData();
        NpcManager.instance.registeToManager(transform.name, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float NpcReduceHP(float velue)
    {
        npcData.CurenttHealth -= velue;
        return npcData.CurenttHealth;
    }
}
