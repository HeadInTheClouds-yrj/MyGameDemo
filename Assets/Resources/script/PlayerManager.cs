using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerData playerData;
    private AttackItems attackItems;
    private Transform playerTransform;
    public Transform PlayerTransform
    {
        get
        {
            return this.transform;
        }
    }
    private void Awake()
    {
        instance = this;
        playerData = new PlayerData();
        attackItems = new AttackItems();
    }
    public float PlayerReduceHP(NpcCell npcCell)
    {
        playerData.CurenttHealth -= npcCell.npcData.BaseDamage;
        return playerData.CurenttHealth;
    }
    public float meleeAttack()
    {
        Dictionary<string,NpcCell> allnpc = NpcManager.instance.getAllNpcCell();
        foreach (var npc in allnpc.Values)
        {
            Debug.Log(npc.name);
            if(attackItems.playerMeleeAttack(npc, playerData.AttackAngle, playerData.MeleeAttackRange))
            {
                Debug.Log(npc.NpcReduceHP(playerData.MeleeDamage));
            }
        }

        return 0f;
    }
}