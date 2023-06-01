using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public static NpcManager instance;
    private int nameFanolyId = 0;
    private AttackItems attackItems;
    public void Awake()
    {
        instance= this;
        allNpcCell = new Dictionary<string, NpcCell>();

    }
    private Dictionary<string,NpcCell> allNpcCell;
    public void factoryNpc(string path,Transform parent)
    {
        GameObject tempObj = (GameObject)Resources.Load(path);
        GameObject NPC = GameObject.Instantiate(tempObj);
        string npcname = NPC.name;
        string[] strnaem = npcname.Split("(");
        NPC.name = strnaem[0]+"_"+nameFanolyId++;
        NPC.transform.SetParent(parent, false);
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
    public float AlMeleeAttack()
    {
        foreach (var cell in allNpcCell.Values)
        {
            if(attackItems.npcMeleeAttack(cell, cell.npcData.MeleeAttackRange))
            {
                PlayerManager.instance.PlayerReduceHP(cell);
            }
        }
        return 0f;
    }
    public void ReduceHP(NpcCell npcCell,float damage)
    {
        npcCell.NpcReduceHP(damage);
    }
}
