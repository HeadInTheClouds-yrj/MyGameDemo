using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
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
    private Transform sworte2;
    public LineRenderer lineRenderer;
    private float lrMaxX = 2.01f;
    private float lrX;
    private float relativetransformX = 1f;
    private float currentHearth;
    private float rectangleMaxLength = 50.9f;
    private float blackDonateLength = 10.0f;
    private float blackDonatespacing = 10f;
    private Vector2 blackDonateWidth;
    private Vector3 blackDonateScal;
    private Transform blackDonateScalTransform;
    private SpriteRenderer blackDonateWidthSpriteRenderer;
    private Transform playerTransform;
    private Sprite playerIcon;
    public Sprite PlayerIcon
    {
        get
        {
            if (playerIcon == null)
            {
                playerIcon = Resources.Load<Sprite>("ArtData/Art/CharacterAvatar/Beastmaster");
                return playerIcon;
            }
            else
            {
                return playerIcon;
            }
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            attackItems = new AttackItems();
            animator = GetComponent<Animator>();
            blackDonateScalTransform = transform.Find("LingQinGrid").GetComponent<Transform>();
            blackDonateWidthSpriteRenderer = transform.Find("LingQinGrid").GetComponent<SpriteRenderer>();
            blackDonateScal = blackDonateScalTransform.localScale;
            blackDonateWidth = blackDonateWidthSpriteRenderer.size;
            currentHearth = playerData.maxLingQi;
        }
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
        //BowPowerSlider.value = 0f;
        //BowPowerSlider.maxValue = maxSliderValue;
        //updateUI();
    }
    public void HandleUpdate()
    {
        HitAnimation();
        GetMouseKey();
        //chekAttackcool();
        PlayerMeleeAttack();
        updateUI();
    }
    private void PlayerReduceLingQi(float damage)
    {
        if (damage>0)
        {
            if (damage>playerData.currentLingQi)
            {
                float temp = damage - playerData.currentLingQi;
                playerData.currentLingQi = 0;
                EventManager.Instance.battleEvent.PlayerReduceHP(temp);
            }
            else
            {
                playerData.currentLingQi -= damage;
            }
        }
    }
    public float PlayerReduceHP(float damage)
    {
        isHit = true;
        playerData.curenttHealth -= damage;
        updateUI();
        ThrowDamageText.instance.ThrowReduceTextFactory(transform,damage);
        return playerData.curenttHealth;
    }
    public void updateUI()
    {
        lrX = (playerData.currentLingQi / playerData.maxLingQi) * lrMaxX;

        lineRenderer.SetPosition(0, new Vector3(transform.position.x - relativetransformX, transform.position.y + 0.8f, 0));
        lineRenderer.SetPosition(1, new Vector3(transform.position.x + lrX / 2, transform.position.y + 0.8f, 0));
        blackDonateWidth.x = currentHearth * 0.001f;
        blackDonateScal.x = rectangleMaxLength / (blackDonateWidth.x * 100);


        blackDonateScalTransform.localScale = blackDonateScal;
        blackDonateWidthSpriteRenderer.size = blackDonateWidth;
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
        playerData.curenttHealth +=value;
        if (playerData.curenttHealth >playerData.maxHealth)
        {
            playerData.curenttHealth = playerData.maxHealth;
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
    private void InstallGongFaOnLoad()
    {
        GongFaInvokeContro[] gongFaTransforms = GetComponentsInChildren<GongFaInvokeContro>();
        foreach (var item in gongFaTransforms)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in playerData.instaillGongFas.Keys)
        {
            GongFaManager.instance.InstantiateGongFa(item,transform);
        }
    }
    public void LoadGame(GameData gameData)
    {
        playerData.id = transform.name;
        playerData.name                     = transform.name;
        playerData.lingShi                  = gameData.datas[0].lingShi;
        playerData.maxAge                   = gameData.datas[0].maxAge;
        playerData.currentAge               = gameData.datas[0].currentAge;
        playerData.scenceIndex              = gameData.datas[0].scenceIndex;
        playerData.survival                 = gameData.datas[0].survival;
        playerData.maxLingQi                = gameData.datas[0].maxLingQi;
        playerData.currentLingQi            = gameData.datas[0].currentLingQi;
        playerData.regenerateLingQi         = gameData.datas[0].regenerateLingQi;
        playerData.maxHealth                = gameData.datas[0].maxHealth;
        playerData.curenttHealth            = gameData.datas[0].curenttHealth;
        playerData.moveSpeed                = gameData.datas[0].moveSpeed;
        playerData.killEnimiesCont          = gameData.datas[0].killEnimiesCont;
        playerData.maxGongFaInstall         = gameData.datas[0].maxGongFaInstall;
        transform.position                  = gameData.datas[0].currentPosition;
        foreach (var item in gameData.datas[0].pickupedItemGameObj)
        {
            bool flag = false;
            foreach (var item1 in playerData.pickupedItemGameObj)
            {
                if (item.Equals(item1))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                playerData.pickupedItemGameObj.Add(item);
            }
        }
        for (int i = 0; i < gameData.datas[0].installOrderGongFaIds.Length; i++)
        {
            playerData.installOrderGongFaIds[i] = gameData.datas[0].installOrderGongFaIds[i];
        }
        foreach (var item in gameData.datas[0].itemIds)
        {
            if (!playerData.itemIds.ContainsKey(item.Key))
            {
                playerData.itemIds.Add(item.Key, item.Value);
            }
        }
        foreach (var item in gameData.datas[0].instaillGongFas)
        {
            if (!playerData.instaillGongFas.ContainsKey(item.Key))
            {
                playerData.instaillGongFas.Add(item.Key, item.Value);
            }
        }
        foreach (var item in gameData.datas[0].learnedGongFas)
        {
            if (!playerData.learnedGongFas.ContainsKey(item.Key))
            {
                playerData.learnedGongFas.Add(item.Key, item.Value);
            }
        }
        foreach (var item in gameData.datas[0].learnedSkills)
        {
            if (!playerData.learnedSkills.ContainsKey(item.Key))
            {
                playerData.learnedSkills.Add(item.Key, item.Value);
            }
        }

        InstallGongFaOnLoad();
    }

    public void SaveGame(GameData gameData)
    {
        if (gameData.datas[0] == null)
        {
            return;
        }
        gameData.datas[0].id = playerData.id;
        gameData.datas[0].name = playerData.name;
        gameData.datas[0].lingShi = playerData.lingShi;
        gameData.datas[0].maxAge = playerData.maxAge;
        gameData.datas[0].currentAge = playerData.currentAge;
        gameData.datas[0].scenceIndex = SceneManager.GetActiveScene().buildIndex;
        gameData.datas[0].survival = playerData.survival;
        gameData.datas[0].maxLingQi = playerData.maxLingQi;
        gameData.datas[0].currentLingQi = playerData.currentLingQi;
        gameData.datas[0].regenerateLingQi = playerData.regenerateLingQi;
        gameData.datas[0].maxHealth = playerData.maxHealth;
        gameData.datas[0].curenttHealth = playerData.curenttHealth;
        gameData.datas[0].moveSpeed = playerData.moveSpeed;
        gameData.datas[0].killEnimiesCont = playerData.killEnimiesCont;
        gameData.datas[0].maxGongFaInstall = playerData.maxGongFaInstall;
        gameData.datas[0].currentPosition = transform.position;
        foreach (var item in playerData.pickupedItemGameObj)
        {
            bool flag = false;
            foreach (var item1 in gameData.datas[0].pickupedItemGameObj)
            {
                if (item.Equals(item1))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                gameData.datas[0].pickupedItemGameObj.Add(item);
            }
        }
        for (int i = 0; i < playerData.installOrderGongFaIds[i].Length; i++)
        {
            gameData.datas[0].installOrderGongFaIds[i] = playerData.installOrderGongFaIds[i];
        }
        gameData.datas[0].itemIds.Clear();
        foreach (var item in playerData.itemIds)
        {
            gameData.datas[0].itemIds.Add(item.Key,item.Value);
        }
        gameData.datas[0].instaillGongFas.Clear();
        foreach (var item in playerData.instaillGongFas)
        {
            gameData.datas[0].instaillGongFas.Add(item.Key,item.Value);
        }
        gameData.datas[0].learnedGongFas.Clear();
        foreach (var item in playerData.learnedGongFas)
        {
            gameData.datas[0].learnedGongFas.Add(item.Key,item.Value);
        }
        gameData.datas[0].learnedSkills.Clear();
        foreach (var item in playerData.learnedSkills)
        {
            gameData.datas[0].learnedSkills.Add(item.Key,item.Value);
        }
    }

    public Data GetData()
    {
        return playerData;
    }

    public Dictionary<string, Data> GetTeams()
    {
        return null;
    }

    public Dictionary<string, Data> GetEnimies()
    {
        return null;
    }
}