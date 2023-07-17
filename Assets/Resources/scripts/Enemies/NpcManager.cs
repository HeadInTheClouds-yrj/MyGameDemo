using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public static NpcManager instance;
    private int nameFanolyId = 0;
    private AttackItems attackItems;
    public LayerMask tree;
    public void Awake()
    {
        instance= this;
        attackItems= new AttackItems();
        allNpcCell = new Dictionary<string, NpcCell>();

    }
    private void Update()
    {

    }
    private Dictionary<string,NpcCell> allNpcCell;
    public void factoryNpc(string path,Transform parent,Vector3 position)
    {
        GameObject tempObj = (GameObject)Resources.Load(path);
        GameObject NPC = GameObject.Instantiate(tempObj);
        string npcname = NPC.name;
        string[] strnaem = npcname.Split("(");
        NPC.name = strnaem[0]+"_"+nameFanolyId++;
        NPC.transform.SetParent(parent,false);
        NPC.transform.position = position;
        NPC.transform.AddComponent<NpcCell>();
    }
    public void registeToManager(string npcName,NpcCell npcCell)
    {
        if (!allNpcCell.ContainsKey(npcName))
        {
            allNpcCell[npcName] = npcCell;
        }
        else
        {
            Debug.Log("已经存在此npc！！！");
        }
    }
    public NpcCell GetNpcCell(string npcName)
    {
        if (allNpcCell.ContainsKey(npcName)) { return allNpcCell[npcName]; } else {Debug.Log("寻找的npc不存在！！！"); return null; }
    }
    public Dictionary<string,NpcCell> getAllNpcCell() { return allNpcCell; }

}
