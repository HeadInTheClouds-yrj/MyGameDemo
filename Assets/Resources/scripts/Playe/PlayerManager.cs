using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour,IDataPersistence,Humanoid
{
    public static PlayerManager instance;
    public Data playerData;
    private AttackItems attackItems;
    public Animator animator;
    private float hittmptime = 0;
    public bool isHit = false;
    public bool leftMouse;
    private float meleeAttackcooltime;
    private int meleeAttackindex = 0;
    public Sprite currentSwordIcon;
    private Image uI;
    private TMP_Text playerhpui;
    [SerializeField]
    public Transform Hand;
    public bool rightMouse;
    public bool isMelee = false;
    public float sliderValue = 0f;
    public float maxSliderValue = 6f;
    public float BowPower = 6f;
    private bool canfire = true;
    public Slider BowPowerSlider;
    public SpriteRenderer spriteRenderer;
    public GameObject SwordPrefer;
    [SerializeField]
    public Transform sworte2;
    public LineRenderer lineRenderer;
    public float lrMaxX = 2.01f;
    public float lrX = 2.01f;
    public float relativetransformX = 1f;
    private Transform playerTransform;
    private Sprite playerIcon;
    public Sprite PlayerIcon
    {
        get
        {
            if (playerIcon == null)
            {
                playerIcon = Resources.Load<Sprite>("data/Art/CharacterAvatar/Beastmaster");
                return playerIcon;
            }
            else
            {
                return playerIcon;
            }
        }
    }
    public Transform PlayerTransform
    {
        get
        {
            return transform;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerData = new Data(this.transform);
        attackItems = new AttackItems();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(instance);
        playerData.LearnedGongFas.Add("ChangShenFa", 1);
    }
    private void OnEnable()
    {
        EventManager.Instance.battleEvent.OnPlayerReduceLingQi += PlayerReduceLingQi;
    }
    private void OnDisable()
    {
        EventManager.Instance.battleEvent.OnPlayerReduceLingQi -= PlayerReduceLingQi;
    }


    private void Start()
    {
        uI = UIManager.instance.GetUI("Panel", "Image_N").transform.GetComponent<Image>();
        playerhpui = UIManager.instance.GetUI("Panel", "Text (TMP)_N").transform.GetComponent<TMP_Text>();
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = maxSliderValue;
        updateUI();
    }
    public void HandleUpdate()
    {
        HitAnimation();
        PlayerInGameHP();
        GetMouseKey();
        //chekAttackcool();
        PlayerMeleeAttack();
    }
    private void PlayerReduceLingQi(float damage)
    {
        if (damage>0)
        {
            if (damage>playerData.CurenttHealth)
            {
                float temp = damage - playerData.CurenttHealth;
                playerData.CurenttHealth = 0;
                EventManager.Instance.battleEvent.PlayerReduceHP(temp);
            }
            else
            {
                playerData.CurrentLingQi -= damage;
            }
        }
    }
    public float PlayerReduceHP(float damage)
    {
        isHit = true;
        playerData.CurenttHealth -= damage;
        lrX -= lrMaxX*(damage/playerData.MaxHealth)*2;
        if (lrX < -2f * relativetransformX)
        {
            lrX = -2f * relativetransformX;
        }
        updateUI();
        ThrowDamageText.instance.ThrowReduceTextFactory(transform,damage);
        return playerData.CurenttHealth;
    }
    public void updateUI()
    {
        uI.fillAmount = playerData.CurenttHealth/playerData.MaxHealth;
        playerhpui.text = playerData.CurenttHealth.ToString()+"/"+playerData.MaxHealth.ToString();
    }
    //public void meleeAttack(bool isAttack)
    //{
    //    if (isAttack)
    //    {
    //        Dictionary<string, NpcCell> allnpc = NpcManager.instance.getAllNpcCell();
    //        NpcCell[] npcs = new NpcCell[allnpc.Count];
    //        int j = 0;
    //        foreach (NpcCell npc in allnpc.Values)
    //        {
    //            npcs[j] = npc;
    //            j++;
    //        }
    //        for (int i = 0; i < npcs.Length; i++)
    //        {
    //            if (npcs[i] != null && attackItems.playerMeleeAttack(npcs[i], playerData.AttackAngle, playerData.MeleeAttackRange))
    //            {
    //                npcs[i].NpcReduceHP(playerData.MeleeDamage);
    //            }
    //        }
    //    }
    //}
    public void PlayerSwordBrow()
    {
        spriteRenderer.enabled = true;
        float angle = AngleTowardsMouse();
        Hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
    }
    public float AngleTowardsMouse()
    {
        Vector3 mouseposition = Input.mousePosition;
        Vector3 playerposition = Camera.main.WorldToScreenPoint(transform.position);
        mouseposition.x = mouseposition.x - playerposition.x;
        mouseposition.y = mouseposition.y - playerposition.y;
        float angle = Mathf.Atan2(mouseposition.y, mouseposition.x) * Mathf.Rad2Deg;
        return angle;
    }
    //public void changeWearpon(Sprite wearponIcon, float Damage, string path)
    //{
    //    SwordPrefer = Resources.Load(path) as GameObject;
    //    spriteRenderer = Hand.GetComponentInChildren<SpriteRenderer>();
    //    spriteRenderer.sprite = wearponIcon;
    //    playerData.RangedDamage = Damage;

    //}
    public void UsePoint(int value)
    {
        playerData.CurenttHealth+=value;
        if (playerData.CurenttHealth>playerData.MaxHealth)
        {
            playerData.CurenttHealth = playerData.MaxHealth;
        }
        updateUI();
    }
    public void GetMouseKey()
    {
        rightMouse = Input.GetMouseButton(1);
        leftMouse = Input.GetMouseButtonDown(0);
        if (rightMouse)
        {
            leftMouse = false;
        }
    }
    //private void chekAttackcool()
    //{
    //    playerMeleeTimeControl();
    //    if (SwordPrefer != null)
    //    {
    //        PlayerBowSwordControl();
    //    }
    //}
    private void HitAnimation()
    {
        if (isHit)
        {
            hittmptime += Time.deltaTime;
            animator.SetBool("isHitt", isHit);
        }
        if (isHit && hittmptime > 0.05f)
        {
            hittmptime = 0;
            isHit = false;
            animator.SetBool("isHitt", isHit);
        }
    }
    //private void PlayerBowSwordControl()
    //{
    //    if (rightMouse&&canfire)
    //    {
    //        Accumulated();
    //    }
    //    else if (Input.GetMouseButtonUp(1) && canfire)
    //    {
    //        Shoot();
    //    }
    //    else
    //    {
    //        if (sliderValue>0f)
    //        {
    //            sliderValue -= Time.deltaTime * 5f;
    //        }
    //        else
    //        {
    //            sliderValue = 0f;
    //            canfire = true;
    //        }
    //        BowPowerSlider.value = sliderValue;
    //    }
    //}
    private void PlayerInGameHP()
    {
        lineRenderer.SetPosition(0, new Vector3(transform.position.x - relativetransformX, transform.position.y + 0.8f, 0));
        lineRenderer.SetPosition(1, new Vector3(transform.position.x + lrX / 2, transform.position.y + 0.8f, 0));
    }
    //private void Shoot()
    //{
    //    if (sliderValue > maxSliderValue)
    //    {
    //        sliderValue = maxSliderValue;
    //    }
    //    float SwordSpeed = sliderValue + BowPower;
    //    float angle = AngleTowardsMouse();
    //    Quaternion rotation = Quaternion.Euler(new Vector3(0f,0f,angle+90));
    //    BowControl bowControl = Instantiate(SwordPrefer,sworte2.position, rotation).GetComponent<BowControl>();
    //    bowControl.swordVelocity = SwordSpeed;
    //    bowControl.playerDamge = playerData.BaseDamage + playerData.RangedDamage * 5 * (sliderValue / maxSliderValue);
    //    canfire = false;
    //    spriteRenderer.enabled = false;
    //}

    private void Accumulated()
    {
        PlayerSwordBrow();
        sliderValue += Time.deltaTime*2.5f;
        BowPowerSlider.value = sliderValue;
        if (sliderValue>maxSliderValue)
        {
            BowPowerSlider.value = maxSliderValue;
        }
    }

    //public void playerMeleeTimeControl()
    //{
    //    if (leftMouse)
    //    {
    //        if (meleeAttackindex == 0)
    //        {
    //            isMelee = true;

    //            meleeAttack(isMelee);
    //            meleeAttackcooltime = 0;
    //            meleeAttackindex++;
    //        }
    //        else
    //        {
    //            if (meleeAttackcooltime > 0.4f)
    //            {
    //                isMelee = true;

    //                meleeAttack(isMelee);
    //                meleeAttackcooltime = 0;
    //            }

    //        }

    //    }
    //}
    public void PlayerMeleeAttack()
    {
        if (isMelee)
        {
            animator.SetBool("isAttack", isMelee);
            meleeAttackcooltime += Time.deltaTime;
        }
        if (meleeAttackcooltime > 0.15f && isMelee)
        {
            isMelee = false;

            animator.SetBool("isAttack", isMelee);
        }
        if (meleeAttackcooltime < 0.4f && !isMelee)
        {
            meleeAttackcooltime += Time.deltaTime;
        }
    }

    public void LoadGame(GameData gameData)
    {
        //playerData.RangedDamage = gameData.RangedDamage;
        //playerData.MeleeDamage = gameData.MeleeDamage;
        //playerData.MeleeAttackRange = gameData.MeleeAttackRange;
        //playerData.MaxHealth = gameData.MaxHealth;
        //playerData.CurenttHealth = gameData.CurenttHealth;
        //playerData.AttackAngle = gameData.AttackAngle;
        //playerData.BaseDamage = gameData.BaseDamage;
        //playerData.PlayerMoveSpeed = gameData.PlayerMoveSpeed;
        //playerData.PlayerName = gameData.PlayerName;
        //lrX = gameData.lrX;
    }

    public void SaveGame(GameData gameData)
    {
        //gameData.RangedDamage = playerData.RangedDamage;
        //gameData.MeleeDamage = playerData.MeleeDamage;
        //gameData.MeleeAttackRange = playerData.MeleeAttackRange;
        //gameData.MaxHealth = playerData.MaxHealth;
        //gameData.CurenttHealth= playerData.CurenttHealth;
        //gameData.AttackAngle = playerData.AttackAngle;
        //gameData.BaseDamage = playerData.BaseDamage;
        //gameData.playerMoveSpeed= playerData.PlayerMoveSpeed;
        //gameData.PlayerName = playerData.PlayerName;
        //gameData.lrX = lrX;
    }

    public Data GetData()
    {
        return playerData;
    }
}