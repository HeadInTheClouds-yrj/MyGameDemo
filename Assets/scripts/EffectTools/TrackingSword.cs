using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class TrackingSword : MonoBehaviour
{
    private Rigidbody2D flySword;
    private Vector2 direction;
    private Transform currentTarget;
    private List<Transform> targets;
    [SerializeField]
    private float attackInterval = 2f;
    [SerializeField]private float flySpeed = 5f;
    [SerializeField]private float rotateSpeed = 100f;
    [SerializeField]private LayerMask targetLayerMask;
    [SerializeField]private VisualEffect effect;
    [SerializeField] private Material[] material;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int materialIndex = 0;
    private float material_DssolveFloat;
    private float lastHitTime = 2f;
    private Transform owner;
    private float notTargetCountTimeDestory = 0;
    private float noTargetTime = 4f;
    private float damage = 10f;
    private float remainingNumberOfAttacks = 3f;
    private float lateTime = 2f;
    private bool isUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        flySword = GetComponent<Rigidbody2D>();
    }
    public TrackingSword SetLaunchSword(Transform owner,List<Transform> targets,Vector3 startPosition,int materialIndex = 0, float damage = 10f,
        float attackInterval = 1f,float remainingNumberOfAttacks = 3f,float flySpeed = 5f,float rotateSpeed = 100f,
        float noTargetTime = 4f,float lateTime = 2f)
    {
        this.materialIndex = materialIndex;
        transform.position = startPosition;
        this.owner = owner;
        this.targets = targets;
        this.damage = damage;
        this.attackInterval = attackInterval;
        this.remainingNumberOfAttacks = remainingNumberOfAttacks;
        this.flySpeed = flySpeed;
        this.rotateSpeed = rotateSpeed;
        this.noTargetTime = noTargetTime;
        this.lateTime = lateTime;
        return this;
    }
    public void LaunchStep()
    {
        isUsed = true;
        gameObject.SetActive(true);
        StartCoroutine(BeginFlyInit(material[materialIndex]));
        InvokeRepeating("FlySwordToTarget", lateTime, 0.02f);
        //StartCoroutine(SwordFindEnemy(lateTime));
    }
    private IEnumerator SwordFindEnemy(float latetime)
    {
        while(lastHitTime<0)
        {
            lastHitTime -= Time.deltaTime;
            FlySwordToTarget();
            yield return null;
        }
    }
    IEnumerator BeginFlyInit(Material currentMaterial)
    {
        float prepareTiem = lateTime, countTime = 0;
        effect.Stop();
        spriteRenderer.material = currentMaterial;
        transform.position = new Vector3(transform.position.x + UnityEngine.Random.Range(-2.5f, 2.5f), transform.position.y + UnityEngine.Random.Range(-2.5f, 2.5f), transform.position.z);
        material_DssolveFloat = 1;
        while (prepareTiem > countTime)
        {
            material_DssolveFloat -= (Time.deltaTime / 3) * 2;
            currentMaterial.SetFloat("_Float", material_DssolveFloat);
            Vector2 tempTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = (Vector3)tempTarget - transform.position;
            countTime +=Time.deltaTime;
            yield return null;
        }
        effect.Play();
    }
    private void FlySwordToTarget()
    {
        if (owner.IsDestroyed())
        {
            Destroy(gameObject);
        }
        else if (!currentTarget.IsDestroyed() && currentTarget != owner && currentTarget != null)
        {
            Vector2 randomTarget = new Vector2(currentTarget.position.x + UnityEngine.Random.Range(-.5f, .5f), currentTarget.position.y + UnityEngine.Random.Range(-.5f, .5f));
            direction = randomTarget - flySword.position;
            Debug.DrawLine(flySword.position, randomTarget, Color.green, 1f);
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            flySword.angularVelocity = -rotateAmount * rotateSpeed;
            flySword.velocity = transform.right * flySpeed;
            Collider2D collider2d = Physics2D.OverlapCircle(transform.position, .1f, targetLayerMask);
            if (lastHitTime < attackInterval)
            {
                lastHitTime += Time.deltaTime;
            }
            if (collider2d != null)
            {
                if (collider2d.transform.TryGetComponent(out NpcCell npc) && lastHitTime >= attackInterval)
                {
                    if (npc.npcData.survival)
                    {
                        npc.NpcReduceHP(damage);
                        lastHitTime = 0;
                        remainingNumberOfAttacks++;
                        if (remainingNumberOfAttacks>=3)
                        {
                            isUsed = false;
                            CancelInvoke("FlySwordToTarget");
                            this.gameObject.SetActive(false);
                            //SkiilManager.Instance.RemoveFlySwordSkillDataByGObj(gameObject);
                            //Destroy(gameObject);
                        }
                    }
                }
            }
        }
        else if (targets.Count > 0)
        {
            float temp = 10f;
            int flag = 0;
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].IsDestroyed())
                {
                    targets.Remove(targets[i]);
                    i--;
                }
            }
            currentTarget = targets.OrderBy(target => (owner.position - target.position).magnitude).FirstOrDefault();
        }
        else
        {
            //currentTarget = owner;
            //direction = (Vector2)currentTarget.position - flySword.position;
            //direction.Normalize();
            //float rotateAmount = Vector3.Cross(direction, transform.right).z;
            //flySword.angularVelocity = -rotateAmount * rotateSpeed;
            //flySword.velocity = transform.right * flySpeed;
            //notTargetCountTimeDestory += Time.fixedDeltaTime;
            //if (notTargetCountTimeDestory > 10f)
            //{
            //    SkiilManager.Instance.RemoveFlySwordSkillDataByGObj(gameObject);
            //    Destroy(gameObject);
            //}
            //
            flySword.velocity = transform.right * flySpeed;
            notTargetCountTimeDestory += Time.fixedDeltaTime;
            if (notTargetCountTimeDestory > noTargetTime)
            {
                notTargetCountTimeDestory = 0;
                isUsed = false;
                CancelInvoke("FlySwordToTarget");
                this.gameObject.SetActive(false);
                //SkiilManager.Instance.RemoveFlySwordSkillDataByGObj(gameObject);
                //Destroy(gameObject);
            }
        }
    }
    public void SetNewTarget(Transform target)
    {
        this.currentTarget = target;
    }
}
