using System;
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
    public float maxSliderValue = 5f;
    public float BowPower = 3f;
    private bool canfire = true;
    public Slider BowPowerSlider;
    public SpriteRenderer spriteRenderer;
    public GameObject SwordPrefer;
    [SerializeField]
    public Transform sworte2;
    public LineRenderer lineRenderer;
    public float lrMaxX = 2f;
    public float lrX = 2f;
    public float relativetransformX = 1f;
    private Transform playerTransform;
    public Transform PlayerTransform
    {
        get
        {
            return transform;
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
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = maxSliderValue;
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
        lineRenderer.SetPosition(0,new Vector3(transform.position.x- relativetransformX, transform.position.y + 0.8f, 0));
        lineRenderer.SetPosition(1, new Vector3(transform.position.x + lrX / 2, transform.position.y + 0.8f, 0));
        GetMouseKey();
        chekAttackcool();
        PlayerMeleeAttack();
    }
    public float PlayerReduceHP(float damage)
    {
        isHit = true;
        updateUI();
        playerData.CurenttHealth -= damage;
        lrX -= lrMaxX*(damage/playerData.MaxHealth)*2;
        if (lrX < -2f * relativetransformX)
        {
            lrX = -2f * relativetransformX;
        }
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
    public void changeWearpon(Sprite wearponIcon, float Damage, string path)
    {
        SwordPrefer = Resources.Load(path) as GameObject;
        spriteRenderer = Hand.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = wearponIcon;
        playerData.RangedDamage = Damage;

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
    private void chekAttackcool()
    {
        playerMeleeTimeControl();
        if (SwordPrefer != null)
        {
            PlayerBowSwordControl();
        }
    }

    private void PlayerBowSwordControl()
    {
        if (rightMouse&&canfire)
        {
            Accumulated();
        }
        else if (Input.GetMouseButtonUp(1) && canfire)
        {
            Shoot();
        }
        else
        {
            if (sliderValue>0f)
            {
                sliderValue -= Time.deltaTime * 1f;
            }
            else
            {
                sliderValue = 0f;
                canfire = true;
            }
            BowPowerSlider.value = sliderValue;
        }
    }

    private void Shoot()
    {
        if (sliderValue > maxSliderValue)
        {
            sliderValue = maxSliderValue;
        }
        float SwordSpeed = sliderValue + BowPower;
        float angle = AngleTowardsMouse();
        Quaternion rotation = Quaternion.Euler(new Vector3(0f,0f,angle+90));
        BowControl bowControl = Instantiate(SwordPrefer,sworte2.position, rotation).GetComponent<BowControl>();
        bowControl.swordVelocity = SwordSpeed;
        canfire = false;
        spriteRenderer.enabled = false;
    }

    private void Accumulated()
    {
        PlayerSwordBrow();
        sliderValue += Time.deltaTime;
        BowPowerSlider.value = sliderValue;
        if (sliderValue>maxSliderValue)
        {
            BowPowerSlider.value = maxSliderValue;
        }
    }

    public void playerMeleeTimeControl()
    {
        if (leftMouse)
        {
            if (meleeAttackindex == 0)
            {
                isMelee = true;

                meleeAttack(isMelee);
                meleeAttackcooltime = 0;
                meleeAttackindex++;
            }
            else
            {
                if (meleeAttackcooltime > 0.4f)
                {
                    isMelee = true;

                    meleeAttack(isMelee);
                    meleeAttackcooltime = 0;
                }

            }

        }
    }
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

}