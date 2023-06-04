using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerData playerData;
    private AttackItems attackItems;
    private Transform playerTransform;
    private Animator animator;
    private float tmphittimee=0;
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
        animator = GetComponent<Animator>();
    }
    public float PlayerReduceHP(NpcCell npcCell)
    {
        playerData.CurenttHealth -= npcCell.npcData.BaseDamage;
        animator.SetBool("isHit", true);
        while (true)
        {
            tmphittimee += Time.deltaTime;
            if (tmphittimee > 0.1f)
            {
                tmphittimee = 0;
                animator.SetBool("isHit", false);
                break;
            }
        }

        return playerData.CurenttHealth;
    }
    public void meleeAttack(bool isAttack)
    {
        if (isAttack)
        {
            Dictionary<string, NpcCell> allnpc = NpcManager.instance.getAllNpcCell();
            foreach (var npc in allnpc.Values)
            {
                if (attackItems.playerMeleeAttack(npc, playerData.AttackAngle, playerData.MeleeAttackRange))
                {
                    NpcManager.instance.ReduceHP(npc, playerData.MeleeDamage + playerData.BaseDamage);
                    Debug.Log(npc.NpcReduceHP(playerData.MeleeDamage)+"======"+npc.name);
                    
                }
            }
        }
    }
}