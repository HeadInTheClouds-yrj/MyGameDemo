using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcCell : MonoBehaviour
{
    private Animator animator;
    public NpcData npcData;
    public bool isMoving=false;
    public bool isIdle=false;
    public bool isAttack = false;
    public bool isHit = false;
    public Vector3 targetMoving;
    public float skeleton01_stateY;
    public float skeleton01_stateX;
    private LayerMask tree;
    private float tmpmovetime;
    private AttackItems attackItems;
    private float idletmptime = 0;
    private Vector3 checkLayervector;
    Vector3 playertemp;
    private float hittmptime = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        npcData = new NpcData();
        NpcManager.instance.registeToManager(transform.name, this);
        attackItems = new AttackItems();
        tree = NpcManager.instance.tree;
    }

    // Update is called once per frame
    void Update()
    {
        
        npcStatecontrllo();
        if (!isMoving)
        {
            idletmptime += Time.deltaTime;
            if (idletmptime > 2f)
            {
                AlIdleMoveLogic();
                idletmptime = 0;
            }
            
        }
        AlMeleeAttack();
        hitContrllo();
    }
    public void hitContrllo()
    {
        if (isHit)
        {
            hittmptime += Time.deltaTime;
            animator.SetBool("skelenton01_isHit", isHit);
        }
        if (isHit && hittmptime > 0.1f)
        {
            hittmptime = 0;
            isHit = false;
            animator.SetBool("skelenton01_isHit", isHit);
        }
    }
    public float NpcReduceHP(float velue)
    {
        isHit = true;
        npcData.CurenttHealth -= velue;
        return npcData.CurenttHealth;
    }
    public bool AlIdleMoveLogic()
    {
        if (!isMoving&&!isIdle&&(PlayerManager.instance.transform.position - transform.position).magnitude > 5)
        {
            skeleton01_stateY = UnityEngine.Random.value < 0.33f ? -1f : UnityEngine.Random.value > 0.66f ? 1f : 0f;
            skeleton01_stateX = UnityEngine.Random.value < 0.33f ? -1f : UnityEngine.Random.value > 0.66f ? 1f : 0f;
            targetMoving = transform.position;
            targetMoving.x += skeleton01_stateX;
            targetMoving.y += skeleton01_stateY;
            StartCoroutine(AiIdleMove(targetMoving));
        }
        return false;
    }
    IEnumerator AiIdleMove(Vector3 targetMoving)
    {
        isIdle = true;
        animator.SetBool("skelenton01_isIdle",isIdle);
        while ((transform.position - targetMoving).magnitude > Mathf.Epsilon && !Physics2D.OverlapCircle(transform.position, 0.1f, tree))
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetMoving, npcData.Movespeed * Time.deltaTime*0.1f);
            
            yield return null;
        }
        transform.position = targetMoving;
        isIdle= false;
        animator.SetBool("skelenton01_isIdle", isIdle);
    }

    public float AlMeleeAttack()
    {
        if ((PlayerManager.instance.transform.position - transform.position).magnitude < 5&&!isIdle)
        {
            tmpmovetime += Time.deltaTime;
            if (!isMoving&&!isIdle&&(PlayerManager.instance.transform.position - transform.position).magnitude > 1f)
            {
                
                if (!isMoving&&!isIdle)
                {
                    animator.SetBool("skelenton01_isMoving", isMoving);
                    playertemp = transform.position;
                    playertemp.x += (PlayerManager.instance.transform.position - transform.position).x * 0.1f;
                    playertemp.y += (PlayerManager.instance.transform.position - transform.position).y * 0.1f;
                    if (!Physics2D.OverlapCircle(playertemp, 0.1f, tree))
                    {
                        StartCoroutine(moveToPlayer(playertemp));
                    }
                }
                
                
            }
            if ((PlayerManager.instance.transform.position - transform.position).magnitude < 1f)
            {
                if (attackItems.npcMeleeAttack(this, npcData.MeleeAttackRange))
                {
                    if (tmpmovetime > 0.5f)
                    {
                        tmpmovetime = 0;
                        isAttack = true;
                        animator.SetBool("skelenton01_isAttack", isAttack);
                        PlayerManager.instance.PlayerReduceHP(npcData.MeleeDamage);
                    }
                }
            }
            if (isAttack && tmpmovetime > 0.05f)
            {
                isAttack = false;
                PlayerManager.instance.isHit = false;
                animator.SetBool("skelenton01_isAttack", isAttack);
            }
        }
        return 0f;
    }
    IEnumerator moveToPlayer(Vector3 playertemp)//npc一多起来，就会停止移动，isMoving一直为True，里面的while是一直在运行的，但是会有两个npc是正常的！！！！！！！
    {
        isMoving = true;
        animator.SetBool("skelenton01_isMoving", isMoving);
        while ((transform.position - playertemp).magnitude > Mathf.Epsilon)
        {
            
            if (Camera.main.WorldToScreenPoint(PlayerManager.instance.PlayerTransform.position).x> Camera.main.WorldToScreenPoint(transform.position).x)
            {
                skeleton01_stateX = 1;
                skeleton01_stateY = 0;
            }
            else
            {
                skeleton01_stateX = -1;
                skeleton01_stateY = 0;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, playertemp, Time.deltaTime*1f);

            yield return null;
        }
        isMoving = false;
        animator.SetBool("skelenton01_isMoving", isMoving);
        transform.position = playertemp;



    }
    public void npcStatecontrllo()
    {

                
                //animator.SetBool("skelenton01_isMoving", isMoving);
                //animator.SetBool("skelenton01_isAttack", isAttack);
                //animator.SetBool("skelenton01_isHit", isHit);
                animator.SetFloat("skeleton01_stateX", skeleton01_stateX);
                animator.SetFloat("skeleton01_stateY", skeleton01_stateY);

    }

}
