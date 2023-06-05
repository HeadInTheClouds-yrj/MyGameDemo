using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerData playerData;
    private AttackItems attackItems;
    private Transform playerTransform;
    public Animator animator;
    private float hittmptime = 0;
    public bool isHit = false;
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
    private void Update()
    {
        if (isHit)
        {
            hittmptime += Time.deltaTime;
            animator.SetBool("isHitt",isHit);
        }
        if (isHit&&hittmptime>0.05f)
        {
            hittmptime = 0;
            isHit = false;
            animator.SetBool("isHitt", isHit);
        }
    }
    public float PlayerReduceHP(float damage)
    {
        isHit = true;
        playerData.CurenttHealth -= damage;
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
                    npc.NpcReduceHP(playerData.MeleeDamage);

                }
            }
        }
    }
}