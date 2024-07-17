using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NpcCell : MonoBehaviour,XingHeng
{
    private Animator animator;
    public EnemyData npcData;
    public bool isMoving=false;
    public bool isIdle=false;
    public bool isAttack = true;
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
    private float lrMaxX = 1.38f;
    private float lrX = 1.38f;
    private float relativetransformX = 0.69f;
    private LineRenderer lineRenderer;
    private Rigidbody2D rb2;
    [SerializeField]
    private float moveSpeed = 1f;
    private EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        npcData = new EnemyData();
        npcData.id = this.name;
        NpcManager.instance.registeToManager(transform.name, this);
        attackItems = new AttackItems();
        tree = NpcManager.instance.tree;
        rb2 = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        lineRenderer= GetComponent<LineRenderer>();
        npcData.survival = true;
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        enemyAI.HandleUpdate();
        npcStatecontrllo();
        lineRenderer.SetPosition(0, new Vector3(transform.position.x - relativetransformX, transform.position.y + 0.4f, 0));
        lineRenderer.SetPosition(1, new Vector3(transform.position.x + lrX / 2, transform.position.y + 0.4f, 0));
        //AlMeleeAttack();
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

        if (npcData.survival)
        {
            isHit = true;
            npcData.curenttHealth -= velue;
            lrX -= lrMaxX * (velue / npcData.maxHealth) * 2;
            if (lrX < -2f * relativetransformX)
            {
                lrX = -2f * relativetransformX;
            }
            ThrowDamageText.instance.ThrowReduceTextFactory(transform, velue);
        }
        if (npcData.curenttHealth <= 0 && npcData.survival)
        {
            npcData.survival = false;
            EventManager.Instance.enimiesEvent.EnimyDie(this);
            npcData.curenttHealth = 0;
            moveSpeed = 0;
            npcData.moveSpeed = 0;
            isAttack = false;
        }
        return npcData.curenttHealth;
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
            
            transform.position = Vector3.MoveTowards(transform.position, targetMoving, npcData.moveSpeed * Time.deltaTime*0.1f);
            
            yield return null;
        }
        transform.position = targetMoving;
        isIdle= false;
        animator.SetBool("skelenton01_isIdle", isIdle);
    }

    //public float AlMeleeAttack()
    //{
    //    if ((PlayerManager.instance.transform.position - transform.position).magnitude < 5&&!isIdle)
    //    {
    //        tmpmovetime += Time.deltaTime;
    //        if (!isMoving&&!isIdle&&(PlayerManager.instance.transform.position - transform.position).magnitude > 1f)
    //        {
                
    //            if (!isMoving&&!isIdle)
    //            {
    //                animator.SetBool("skelenton01_isMoving", isMoving);
    //                playertemp = transform.position;
    //                playertemp.x += (PlayerManager.instance.transform.position - transform.position).x * 0.1f;
    //                playertemp.y += (PlayerManager.instance.transform.position - transform.position).y * 0.1f;
    //                if (!Physics2D.OverlapCircle(playertemp, 0.1f, tree))
    //                {
    //                    StartCoroutine(moveToPlayer(playertemp));
    //                }
    //            }
                
                
    //        }
    //        if ((PlayerManager.instance.transform.position - transform.position).magnitude < 1f)
    //        {
    //            //if (attackItems.npcMeleeAttack(this, npcData.MeleeAttackRange))
    //            //{
    //            //    if (tmpmovetime > 0.5f)
    //            //    {
    //            //        tmpmovetime = 0;
    //            //        isAttack = true;
    //            //        animator.SetBool("skelenton01_isAttack", isAttack);
    //            //        PlayerManager.instance.PlayerReduceHP(npcData.MeleeDamage);
    //            //    }
    //            //}
    //        }
    //        if (isAttack && tmpmovetime > 0.05f)
    //        {
    //            isAttack = false;
    //            PlayerManager.instance.isHit = false;
    //            animator.SetBool("skelenton01_isAttack", isAttack);
    //        }
    //    }
    //    return 0f;
    //}
    public void Attack()
    {
        if (isAttack)
        {
            attackItems.npcMeleeAttack(this, 5f);
            tmpmovetime = 0;
            animator.SetBool("skelenton01_isAttack", isAttack);
            StartCoroutine(AttackCountTime());
            PlayerManager.instance.PlayerReduceHP(5f);
        }
        
    }
    IEnumerator AttackCountTime()
    {
        yield return new WaitForSeconds(0.1f);
        isAttack = false;
        animator.SetBool("skelenton01_isAttack", isAttack);
        isAttack = true;

    }
    public void MovePointInput(Vector2 playertemp)
    {
        isMoving = true;
        animator.SetBool("skelenton01_isMoving", isMoving);
        Vector2 tempVector2 = playertemp - (Vector2)transform.position;
        tempVector2 = tempVector2.normalized;
        skeleton01_stateX = tempVector2.x;
        skeleton01_stateY = tempVector2.y;
        transform.position = Vector2.MoveTowards(transform.position, playertemp, Time.deltaTime * moveSpeed);
    }
    public void MovementInput(Vector2 playertemp)
    {
        if (playertemp != Vector2.zero)
        {
            isMoving = true;
            animator.SetBool("skelenton01_isMoving", isMoving);
            Vector2 tempTarget = transform.position;
            tempTarget.x += playertemp.x;
            tempTarget.y += playertemp.y;
            skeleton01_stateX = playertemp.x;
            skeleton01_stateY = playertemp.y;
            transform.position = Vector2.MoveTowards(transform.position, tempTarget, Time.deltaTime * moveSpeed);
        }
        else
        {
            isMoving = false;
            animator.SetBool("skelenton01_isMoving", isMoving);
        }
        
    }
    public void npcStatecontrllo()
    {


        //animator.SetBool("skelenton01_isMoving", isMoving);
        //animator.SetBool("skelenton01_isAttack", isAttack);
        //animator.SetBool("skelenton01_isHit", isHit);
        animator.SetFloat("skeleton01_stateX", skeleton01_stateX);
        animator.SetFloat("skeleton01_stateY", skeleton01_stateY);

    }

    public void UpdateData()
    {
        npcData.currentPosition = transform.position;
    }
    public void UpdateToCell()
    {
        transform.position = npcData.currentPosition;
        
    }
    public EnemyData GetData()
    {
        return new EnemyData(this.name,SceneManager.GetActiveScene().buildIndex,true,npcData.currentLingQi,npcData.maxLingQi,npcData.maxHealth,npcData.curenttHealth,npcData.moveSpeed,transform.position);
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
