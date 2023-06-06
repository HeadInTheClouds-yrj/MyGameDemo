using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerData playerData;
    private AttackItems attackItems;
    private Transform playerTransform;
    public Animator animator;
    private float hittmptime = 0;
    public bool isHit = false;
    private Image uI;
    private TMP_Text playerhpui;
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
    private void Start()
    {
        uI = UIManager.instance.GetUI("Panel", "Image_N").transform.GetComponent<Image>();
        playerhpui = UIManager.instance.GetUI("Panel", "Text (TMP)_N").transform.GetComponent<TMP_Text>();
        updateUI();
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
        updateUI();
        playerData.CurenttHealth -= damage;
        return playerData.CurenttHealth;
    }
    public void updateUI()
    {
        uI.fillAmount = playerData.CurenttHealth/playerData.MaxHealth;
        playerhpui.text = playerData.CurenttHealth.ToString()+"/"+playerData.MaxHealth.ToString();
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