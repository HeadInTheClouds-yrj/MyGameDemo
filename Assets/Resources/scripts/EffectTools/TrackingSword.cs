using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using static UnityEditor.Progress;

[RequireComponent(typeof(Rigidbody2D))]
public class TrackingSword : MonoBehaviour
{
    private Rigidbody2D flySword;
    private Vector2 direction;
    private Transform currentTarget;
    private List<Transform> targets;
    [SerializeField]
    private float attackCoolingTime = 2f;
    [SerializeField]private float flySpeed = 5f;
    [SerializeField]private float rotateSpeed = 100f;
    [SerializeField]private LayerMask targetLayerMask;
    [SerializeField]private VisualEffect effect;
    [SerializeField] private Material material;
    private float material_DssolveFloat;
    private float lastHitTime = 2;
    private Transform owner;
    private float notTargetCountTimeDestory = 0;
    // Start is called before the first frame update
    void Start()
    {
        flySword = GetComponent<Rigidbody2D>();
        owner = PlayerManager.instance.transform;
        targets = PlayerManager.instance.GetEnemies();
        StartCoroutine(BeginFlyInit());
        InvokeRepeating("FlySwordToTarget", 2f, 0.02f);
    }
    IEnumerator BeginFlyInit()
    {
        float prepareTiem = 2f,countTime = 0;
        effect.Stop();
        transform.position = new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), transform.position.z);
        material_DssolveFloat = 1;
        while (prepareTiem > countTime)
        {
            material_DssolveFloat -= (Time.deltaTime / 3) * 2;
            material.SetFloat("_Float", material_DssolveFloat);
            Vector2 tempTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = (Vector3)tempTarget - transform.position;
            countTime +=Time.deltaTime;
            yield return null;
        }
        effect.Play();
    }
    // Update is called once per frame
    private void FlySwordToTarget()
    {
        if (owner.IsDestroyed())
        {
            Destroy(gameObject);
        }
        else if (!currentTarget.IsDestroyed() && currentTarget != owner && currentTarget != null)
        {
            Vector2 randomTarget = new Vector2(currentTarget.position.x + Random.Range(-.5f, .5f), currentTarget.position.y + Random.Range(-.5f, .5f));
            direction = randomTarget - flySword.position;
            Debug.DrawLine(flySword.position, randomTarget, Color.green, 1f);
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            flySword.angularVelocity = -rotateAmount * rotateSpeed;
            flySword.velocity = transform.right * flySpeed;
            Collider2D collider2d = Physics2D.OverlapCircle(transform.position, .1f, targetLayerMask);
            if (lastHitTime < attackCoolingTime)
            {
                lastHitTime += Time.deltaTime;
            }
            if (collider2d != null)
            {
                if (collider2d.transform.TryGetComponent(out NpcCell npc) && lastHitTime >= attackCoolingTime)
                {
                    npc.NpcReduceHP(10);
                    lastHitTime = 0;
                }
            }
        }
        else if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i].IsDestroyed())
                {
                    targets.Remove(targets[i]);
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
            if (notTargetCountTimeDestory > 4f)
            {
                SkiilManager.Instance.RemoveFlySwordSkillDataByGObj(gameObject);
                Destroy(gameObject);
            }
        }
    }
    public void InitializedSword(Transform owner ,Transform target, LayerMask npcLayerMask, float flySpeed=100f, float rotateSpeed=500f)
    {
        this.owner = owner;
        this.currentTarget = target;
        this.flySpeed = flySpeed;
        this.rotateSpeed = rotateSpeed;
        this.targetLayerMask = npcLayerMask;
    }
    public void SetNewTarget(Transform target)
    {
        this.currentTarget = target;
    }
}
