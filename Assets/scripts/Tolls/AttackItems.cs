using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItems
{
    public bool playerMeleeAttack(NpcCell attacked,float attackAngle,float attackRange)
    {
        Vector3 enimy_player = attacked.transform.position- PlayerManager.instance.transform.position;//玩家到敌人的向量
        Vector3 player_mouse = Input.mousePosition - Camera.main.WorldToScreenPoint(PlayerManager.instance.transform.position);//鼠标到玩家的向量
        Vector3 attackVectorRange = player_mouse.normalized * attackRange;//攻击距离
        float mt_enimy_player = enimy_player.magnitude;//玩家到敌人的距离
        float halfAngle = MathF.Acos(Vector3.Dot(attackVectorRange.normalized, enimy_player.normalized))*180f/MathF.PI;//玩家鼠标之间的夹角
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
        Vector3 enimy_player = PlayerManager.instance.transform.position - attack.transform.position;//敌人到玩家的向量
        float mt_enimy_player = enimy_player.magnitude;
        if (mt_enimy_player <= attackRange)
        {
            return true;
        }
        return false;
    }

}
