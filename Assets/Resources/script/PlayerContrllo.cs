using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerContrllo : MonoBehaviour
{
    public Camera maincamera;
    private bool isMoving = false;
    private bool isRoll = false;
    private float tempRollX = 0;
    private float tempRollY = 0;
    private float rollcooltime = 0;
    private int rollindex = 0;
    private Vector3 input;
    private float stateIndex_X;
    private float stateIndex_Y;
    private float tempfloatspeed = 2;
    private Animator animator;
    private Vector3 test;
    [SerializeField] LayerMask tree;
    [SerializeField] LayerMask interactive;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        meleeAttackAnimationContrllo();
        StateSet(stateIndex_X, stateIndex_Y);
        playmove();
        checkRollcool();
        PlayerRoll();
        cameraFllowPlayer();
        ToTalk();
    }

    private void ToTalk()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vector3 fsdalf = new Vector3(0,0,0);
            fsdalf.x = animator.GetFloat("idlex");
            fsdalf.y = animator.GetFloat("idley");
            var collider = Physics2D.OverlapCircle(transform.position + fsdalf, 0.1f, interactive);
            if (collider != null)
            {
                collider.GetComponent<Interactives>()?.ToTalk();
            }
        }
    }

    private void cameraFllowPlayer()
    {
        test = transform.position;
        test.z = -7;
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, test, tempfloatspeed * 6);
    }
    private void meleeAttackAnimationContrllo() //player look for mouse funcition
    {
        if (!PlayerManager.instance.isMelee)
        {
            
            Vector3 playerOnScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePosition = Input.mousePosition;
            if (mousePosition.x > playerOnScreenPosition.x && mousePosition.y >
                playerOnScreenPosition.y && mousePosition.y * Screen.width > mousePosition.x * Screen.height)
            {
                stateIndex_X = 1;
                stateIndex_Y = 1;
            }
            else if (mousePosition.x < playerOnScreenPosition.x && mousePosition.y >
                playerOnScreenPosition.y && MathF.Abs(mousePosition.y - playerOnScreenPosition.y) > MathF.Abs(mousePosition.x - playerOnScreenPosition.x))
            {
                stateIndex_X = -1;
                stateIndex_Y = 1;
            }
            else if (mousePosition.x > playerOnScreenPosition.x && mousePosition.y >
                playerOnScreenPosition.y && mousePosition.y * Screen.width < mousePosition.x * Screen.height ||
                mousePosition.x > playerOnScreenPosition.x && mousePosition.y <
                playerOnScreenPosition.y && MathF.Abs(mousePosition.y - playerOnScreenPosition.y) < MathF.Abs(mousePosition.x - playerOnScreenPosition.x))
            {
                stateIndex_X = 1;
                stateIndex_Y = 0;
            }
            else if (mousePosition.x < playerOnScreenPosition.x && mousePosition.y >
                playerOnScreenPosition.y && MathF.Abs(mousePosition.y - playerOnScreenPosition.y) * Screen.width < MathF.Abs(mousePosition.x - playerOnScreenPosition.x) * Screen.height ||
                mousePosition.x < playerOnScreenPosition.x && mousePosition.y <
                playerOnScreenPosition.y && mousePosition.y * Screen.width > mousePosition.x * Screen.height)
            {
                stateIndex_X = -1;
                stateIndex_Y = 0;
            }
            else if (mousePosition.x > playerOnScreenPosition.x && mousePosition.y <
                playerOnScreenPosition.y && MathF.Abs(mousePosition.y - playerOnScreenPosition.y) > MathF.Abs(mousePosition.x - playerOnScreenPosition.x))
            {
                stateIndex_X = 1;
                stateIndex_Y = -1;
            }
            else if (mousePosition.x < playerOnScreenPosition.x && mousePosition.y <
                playerOnScreenPosition.y && mousePosition.y * Screen.width < mousePosition.x * Screen.height)
            {
                stateIndex_X = -1;
                stateIndex_Y = -1;
            }
        }

    }

    private void checkRollcool()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (rollindex == 0)
            {
                isRoll = true;
                rollcooltime = 0;
                rollindex++;
            }
            else
            {
                if (rollcooltime > 3f)
                {
                    isRoll = true;
                    rollcooltime = 0;

                }
            }
        }

    }
    public void roll()
    {
        Vector3 targetposition = transform.position;
        targetposition.x += tempRollX * 4;
        targetposition.y += tempRollY * 4;
        if (Physics2D.OverlapCircle(transform.position, 0.1f, tree))
        {
            animator.SetBool("isroll", isRoll);
            return;
        }
        else if((transform.position - targetposition).magnitude > Mathf.Epsilon)//使用while效果更好，可惜了之前没想到，if凑合用吧！
        {
            transform.position = Vector3.Lerp(transform.position, targetposition, tempfloatspeed * Time.deltaTime*1f);
            animator.SetBool("isroll", isRoll);
        }

    }
    public void PlayerRoll()
    {
        if (isRoll)
        {
            roll();
            rollcooltime += Time.deltaTime;
        }

        if(rollcooltime > 0.15f&&isRoll)
        {
            isRoll = false;
            animator.SetBool("isroll", isRoll);

        }
        if (!isRoll&&rollcooltime < 3.1f)
        {
            rollcooltime += Time.deltaTime;
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
            if (input != Vector3.zero&&!isRoll)
            {
                Vector3 targetposition = transform.position;
                targetposition.x += input.x;
                targetposition.y += input.y;
                tempRollX = input.x;
                tempRollY = input.y;
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
    IEnumerator moving(Vector3 targetposition)
    {
        Vector3 temptarget = targetposition;
        temptarget.x -= tempRollX * 0.75f;
        temptarget.y -= tempRollY * 0.75f;
        Debug.DrawLine(transform.position, temptarget);
        if (Physics2D.OverlapCircle(temptarget, 0.1f, tree))
        {
            animator.SetBool("ismove", isMoving);
        }
        else if ((transform.position - targetposition).magnitude > Mathf.Epsilon)
        {
            isMoving = true;
            animator.SetBool("ismove", isMoving);
            transform.position = Vector3.MoveTowards(transform.position, targetposition, tempfloatspeed * Time.deltaTime);
            isMoving = false;
            yield return null;
        }
    }
}
