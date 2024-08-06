using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class MagicRayVFX : MonoBehaviour
{
    private string PER_PARTICLE_LIFE_TIME = "PerParticleLifeTime";
    private string BEGING_TOTAL_TIME = "BegingTotalTime";
    private string MAIN_ATTACK_DIRECTION = "MainAttackDirection";
    private string ATTACK_DIRECTION_1 = "AttackDirection1";
    private string ATTACK_DIRECTION_2 = "AttackDirection2";
    private string START_SHOOT_POSITION = "StartShootPosition";
    private string PER_POWERFULL_START_POSITION = "PerPowerfullStartPosition";
    private string SHOOT_RAY_SPEED = "ShootRaySpeed";
    private string PER_START_ANGLE = "PerStartAngle";
    [SerializeField]
    private GameObject project;
    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private VisualEffect visualEffect;
    // Start is called before the first frame update
    [SerializeField]
    private float redius = 1f;
    private void OnEnable()
    {
        //EventManager.Instance.InputEvent.OnGetRightMouseDown += Shoot;
    }
    private void OnDisable()
    {
        //EventManager.Instance.InputEvent.OnGetRightMouseDown -= Shoot;
    }
    void Start()
    {
        mainCamera = Camera.main.transform;
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (counttime<.5f && tempflag == true)
        //{
        //    counttime += Time.deltaTime;
        //}
        //else if (tempHit && counttime>=.25f)
        //{
        //    RaycastHit2D[] all = Physics2D.RaycastAll(startPosition,endPosition1 - startPosition,100000f,LayerMask.GetMask("Npc"));
        //    foreach (RaycastHit2D item in all)
        //    {
        //        Debug.Log(item.transform.name);
        //    }
        //    tempHit = false;
        //}
        //else if(counttime>.5f&&tempflag == true)
        //{
        //    visualEffect.SetFloat("BegingTotalTime",9999999f);
        //    counttime= 0;
        //    tempflag = false;
        //}
    }
    public void PlayerShoot()
    {
        StartCoroutine(ShootRayCountTime(PlayerManager.instance.transform, visualEffect, Camera.main.ScreenToWorldPoint(Input.mousePosition), targetMask));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="transform">这个参数需要释放人的位置</param>
    /// <param name="targetPosition"></param>
    public void Shoot(Transform transform,Vector3 targetPosition)
    {
        //startPosition = transform.position;
        //Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //endPosition1 = end;
        //Vector2 direction = end - (Vector2)transform.position;
        //Vector3 endPosition = GetStartPosition(direction,transform.position,end, redius);
        //visualEffect.SetFloat("BegingTotalTime", 0f);
        //visualEffect.SetVector2("AttackDirection", direction.normalized);
        //visualEffect.SetVector3("StartShootPosition", endPosition);
        //visualEffect.Play();
        //tempHit= true;
        //tempflag = true;
        //counttime = 0;
        StartCoroutine(ShootRayCountTime(transform,visualEffect,targetPosition, targetMask));
    }
    IEnumerator SelfDestruct()
    {
        int targetTime = 1, countTime = 0;
        while (targetTime == countTime)
        {
            countTime++;
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }
    IEnumerator ShootRayCountTime(Transform transform,VisualEffect visualEffect,Vector3 targetPosition,LayerMask targetMask)
    {
        float counttime = 0;
        bool raycastHitFlag = false;
        //Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        InitializePropety(transform, visualEffect, targetPosition);
        while (true)
        {
            counttime += Time.deltaTime;
            if (counttime> visualEffect.GetFloat(PER_PARTICLE_LIFE_TIME)&&!raycastHitFlag)
            {
                ShootProjectDetector();
                raycastHitFlag=true;
            }
            if (counttime>visualEffect.GetFloat(PER_PARTICLE_LIFE_TIME)+0.3f)
            {
                visualEffect.SetFloat("BegingTotalTime", 9999999f);
                break;
            }
            yield return null;
        }
        yield return null;
    }
    Quaternion tempQuaternion;
    private void InitializePropety(Transform transform, VisualEffect visualEffect, Vector3 targetPosition)
    {
        Vector2 end = targetPosition;
        Vector2 direction = end - (Vector2)transform.position;
        Vector3 endPosition = GetStartPosition(direction, transform.position, end, redius);
        visualEffect.SetFloat(BEGING_TOTAL_TIME, 0f);
        visualEffect.SetVector2(ATTACK_DIRECTION_1, direction.normalized);
        visualEffect.SetVector2(ATTACK_DIRECTION_2, direction.normalized);
        visualEffect.SetVector2(MAIN_ATTACK_DIRECTION, direction.normalized);
        visualEffect.SetVector3(START_SHOOT_POSITION, endPosition);
        visualEffect.SetVector3(PER_POWERFULL_START_POSITION, new Vector3(transform.position.x - Camera.main.transform.position.x, transform.position.y - Camera.main.transform.position.y, 0));
        tempQuaternion = Quaternion.Euler(new Vector3(0, 0, GetAngleForMousePosition() + 90));
        visualEffect.SetVector3(PER_START_ANGLE, new Vector3(-GetAngleForMousePosition(), 80, 0));
        //

        //

        visualEffect.Play();
    }
    private void ShootProjectDetector()
    {
        //Quaternion tempQuaternion = Quaternion.Euler(new Vector3(0, 0, GetAngleForMousePosition() + 90));
        BowControl bowControl = Instantiate(project, visualEffect.GetVector3(START_SHOOT_POSITION), tempQuaternion).GetComponent<BowControl>();
        bowControl.swordVelocity = visualEffect.GetFloat(SHOOT_RAY_SPEED);
        bowControl.playerDamge = 6;
    }
    private float GetAngleForMousePosition()
    {
        Vector3 mouseposition = Input.mousePosition;
        Vector3 playerposition = Camera.main.WorldToScreenPoint(PlayerManager.instance.transform.position);
        mouseposition.x = mouseposition.x - playerposition.x;
        mouseposition.y = mouseposition.y - playerposition.y;
        float angle = Mathf.Atan2(mouseposition.y, mouseposition.x) * Mathf.Rad2Deg;
        return angle;
    }
    private Vector3 GetStartPosition(Vector2 dir,Vector2 owner, Vector2 endPosition, float redius = 2f)
    {
        float b, k,x = owner.x,y = owner.y,r = redius ,m,n,p;
        Vector2 target = dir.normalized;
        //The player is the zero point of the coordinate system.
        //The relative position of the mouse click is in the first or third quadrant, close to the y coordinate axis.
        if (target.y / target.x > float.MaxValue)
        {
            k = 999f;
        }
        //The player is the zero point of the coordinate system.
        //The relative position of the mouse click is in the second or fourth quadrant, close to the y coordinate axis.
        else if (target.y / target.x < float.MinValue)
        {
            k = -999f;
        }
        else
        {
            k = target.y / target.x;
        }
        //b= y - k*x
        if (endPosition.y - endPosition.x * k > float.MaxValue)
        {
            b = float.MaxValue;
        }
        else if (endPosition.y - endPosition.x * k < float.MinValue)
        {
            b = float.MinValue;
        }
        else
        {
            b = endPosition.y - endPosition.x * k;
        }
        m = k * k + 1;
        n = 2*(k * b - k * y - x);
        p = (b - y) * (b - y) - r * r + x * x;
        
        //Multiply the final direction position
        if (endPosition.y>owner.y&&endPosition.x > owner.x)
        {
            target.x = (-n - Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = -Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;

            target.y = k * target.x + b;
        }
        else if (endPosition.y<owner.y && endPosition.x < owner.x)
        {
            target.x = (-n + Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;
            target.y = k * target.x + b;
        }
        else if (endPosition.y>owner.y&&endPosition.x<owner.x)
        {
            target.x = (-n + Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = -Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;
        }
        else
        {
            target.x = (-n - Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;
        }
        return target;
    }
}
