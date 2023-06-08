using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackItems
{
    public bool playerMeleeAttack(NpcCell attacked,float attackAngle,float attackRange)
    {
        Vector3 enimy_player = attacked.transform.position- PlayerManager.instance.PlayerTransform.position;//玩家到敌人的向量
        Vector3 player_mouse = Input.mousePosition - Camera.main.WorldToScreenPoint(PlayerManager.instance.PlayerTransform.position);//鼠标到玩家的向量
        Debug.DrawLine(Camera.main.WorldToScreenPoint(PlayerManager.instance.PlayerTransform.position), Input.mousePosition);

        Vector3 attackVectorRange = player_mouse.normalized * attackRange;
        float mt_enimy_player = enimy_player.magnitude;
        float halfAngle = MathF.Acos(Vector3.Dot(attackVectorRange.normalized, enimy_player.normalized))*180f/MathF.PI;
        if (halfAngle <= attackAngle/2)
        {
            if (mt_enimy_player <= attackRange)
            {
                return true;
            }
        }
        return false;
    }
    public bool npcMeleeAttack(NpcCell attack, float attackRange)
    {
        Vector3 enimy_player = PlayerManager.instance.PlayerTransform.position - attack.transform.position;//敌人到玩家的向量
        float mt_enimy_player = enimy_player.magnitude;
        if (mt_enimy_player <= attackRange)
        {
            return true;
        }
        return false;
    }

}
