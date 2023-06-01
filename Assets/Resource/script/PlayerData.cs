using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private string playerName = "beastmaster";
    private float maxHealth = 100;
    private float movespeed = 2;
    private float baseDamage = 5;
    private float curenttHealth = 100;
    private float meleeDamage = 10;
    private float attackAngle = 60;
    private float meleeAttackRange = 15;

    public string PlayerName { get => playerName; set => playerName = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Movespeed { get => movespeed; set => movespeed = value; }
    public float BaseDamage { get => baseDamage; set => baseDamage = value; }
    public float CurenttHealth { get => curenttHealth; set => curenttHealth = value; }
    public float MeleeDamage { get => meleeDamage; set => meleeDamage = value; }
    public float MeleeAttackRange { get => meleeAttackRange; set => meleeAttackRange = value; }
    public float AttackAngle { get => attackAngle; set => attackAngle = value; }
}
