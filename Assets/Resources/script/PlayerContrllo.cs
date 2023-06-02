using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerContrllo : MonoBehaviour
{
    public Camera maincamera;
    private bool isMoving = false;
    private bool isRoll=false;
    private float exitrolltime;
    private float rollcooltime;
    private int rollindex = 0;
    private Vector2 input;
    private float stateIndex_X;
    private float stateIndex_Y;
    private float tempfloatspeed = 2;
    private Animator animator;
    private Vector3 test;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        exitrolltime += Time.deltaTime;
        animator.SetBool("isroll", isRoll);
        StateSet(stateIndex_X, stateIndex_Y);
        checkState();
        playmove();
        checkRollcool();
        PlayerRoll();
        test = transform.position;
        test.z = -7;
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position,test,tempfloatspeed * 6);
        //Debug.Log(Input.mousePosition+"======="+Camera.main.WorldToScreenPoint(transform.position));
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(MouseButton.Left);
            PlayerManager.instance.meleeAttack();
        }
    }
    private void checkRollcool()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (rollindex == 0)
            {
                isRoll = true;
                rollcooltime = exitrolltime;
                rollindex++;
            }
            else
            {
                if (exitrolltime - rollcooltime > 4f)
                {
                    isRoll = true;
                    rollcooltime = exitrolltime;
                }
            }
        }
        
    }
    public void roll()
    {
        Vector3 targetposition = transform.position;
        targetposition.x += animator.GetFloat("rollx") * 4;
        targetposition.y += animator.GetFloat("rolly") * 4;
        if ((transform.position - targetposition).magnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.Lerp(transform.position, targetposition, tempfloatspeed * Time.deltaTime*1f);
        }
        if (exitrolltime - rollcooltime > 0.15f)
        {
            isRoll = false;
        }
    }
    public void PlayerRoll()
    {
        if (isRoll)
        {
            roll();
        }
    }
    public void playerHiting(PlayerManager playerManager)
    {

    }
    public void playmove()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            //if (input.x != 0)
            //{
            //    input.y = 0;
            //}
            animator.SetBool("ismove", isMoving);
            if (input != Vector2.zero&&!isRoll)
            {
                Vector3 targetposition = transform.position;
                targetposition.x += input.x;
                targetposition.y += input.y;
                stateIndex_X = input.x;
                stateIndex_Y = input.y;
                if (!isRoll)
                {
                    StartCoroutine(moving(targetposition));
                }

            }
        }
    }
    public void StateSet(float x,float y)
    {
        animator.SetFloat("idlex", x);
        animator.SetFloat("idley", y);
        animator.SetFloat("walkx", x);
        animator.SetFloat("walky", y);
        animator.SetFloat("rollx", x);
        animator.SetFloat("rolly", y);
    }
    public void checkState()
    {
        animator.SetBool("isroll", isRoll);
    }
    IEnumerator moving(Vector3 targetposition)
    {
        if ((transform.position - targetposition).magnitude > Mathf.Epsilon)
        {
            isMoving = true;
            animator.SetBool("ismove", isMoving);
            transform.position = Vector3.MoveTowards(transform.position, targetposition, tempfloatspeed * Time.deltaTime);
            isMoving = false;
            yield return null;
        }
    }
}
