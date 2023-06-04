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
    public bool isIdle=true;
    public bool isAttack = false;
    public bool isHit = false;
    public Vector3 targetMoving;
    public float skeleton01_stateY;
    public float skeleton01_stateX;
    [SerializeField] LayerMask tree;
    private float tmphittimee;
    private AttackItems attackItems;
    private float idletmptime = 0;
    private Vector3 checkLayervector;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        npcData = new NpcData();
        NpcManager.instance.registeToManager(transform.name, this);
        attackItems = new AttackItems();
    }

    // Update is called once per frame
    void Update()
    {
        idletmptime += Time.deltaTime;
        npcStatecontrllo();
        if (idletmptime>4f)
        {
            AlIdleMoveLogic();
            idletmptime= 0;
        }
        AlMeleeAttack();

    }
    public float NpcReduceHP(float velue)
    {
        npcData.CurenttHealth -= velue;
        return npcData.CurenttHealth;
    }
    public bool AlIdleMoveLogic()
    {
        if (isIdle)
        {
            skeleton01_stateY = UnityEngine.Random.value < 0.33f ? -1f : UnityEngine.Random.value > 0.66f ? 1f : 0f;
            skeleton01_stateX = UnityEngine.Random.value < 0.33f ? -1f : UnityEngine.Random.value > 0.66f ? 1f : 0f;
            targetMoving = transform.position;
            checkLayervector = transform.position;
            checkLayervector.x -= 0.75f;
            checkLayervector.y -= 0.75f;
            targetMoving.x += skeleton01_stateX;
            targetMoving.y += skeleton01_stateY;
            if (!Physics2D.OverlapCircle(checkLayervector, 0.1f,tree))
            {
                StartCoroutine(AiIdleMove(targetMoving));
            }
        }
        return false;
    }
    IEnumerator AiIdleMove(Vector3 targetMoving)
    {
        isIdle = false;
        while ((transform.position - targetMoving).magnitude > Mathf.Epsilon)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetMoving, npcData.Movespeed * Time.deltaTime*0.1f);
            
            yield return null;
        }
        transform.position = targetMoving;
    }

    public float AlMeleeAttack()
    {
        if ((PlayerManager.instance.transform.position - transform.position).magnitude < 5)
        {
            isIdle = false;
            var playertemp = transform.position;
            playertemp.x += (PlayerManager.instance.transform.position - transform.position).x*0.1f;
            playertemp.y += (PlayerManager.instance.transform.position - transform.position).y * 0.1f;
            if (!isAttack&&!Physics2D.OverlapCircle(playertemp, 0.1f, tree))
            {
                StartCoroutine(moveToPlayer());
            }
            if ((PlayerManager.instance.transform.position - transform.position).magnitude < 1)
            {
                isMoving = false;
                tmphittimee += Time.deltaTime;
                if (attackItems.npcMeleeAttack(this, npcData.MeleeAttackRange))
                {

                    if (tmphittimee > 1f)
                    {
                        tmphittimee = 0;
                        isAttack = true;
                        animator.SetBool("skelenton01_isAttack", isAttack);
                        PlayerManager.instance.PlayerReduceHP(this);
                    }
                }
            }
            if (isAttack && tmphittimee > 0.05f)
            {
                isAttack = false;
                animator.SetBool("skelenton01_isAttack", isAttack);
            }
        }
        else
        {
            isIdle = true; 
        }
        return 0f;
    }
    IEnumerator moveToPlayer()
    {
        isMoving = true;
        animator.SetBool("skelenton01_isMoving", isMoving);
        Vector3 playertemp = transform.position;
        playertemp.x = (PlayerManager.instance.transform.position - transform.position).x*0.5f;
        playertemp.y = (PlayerManager.instance.transform.position - transform.position).y*0.5f;
        while ((transform.position - PlayerManager.instance.PlayerTransform.position).magnitude > 1)
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
            
            transform.position = Vector3.Lerp(transform.position, playertemp, Time.deltaTime*0.001f);
            yield return null;
        }
        isMoving = false;
        animator.SetBool("skelenton01_isMoving", isMoving);

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
