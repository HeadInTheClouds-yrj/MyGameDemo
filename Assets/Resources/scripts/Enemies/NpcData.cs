using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NpcData
{
    private string npcName = "enimy";
    private float maxHealth = 100;
    private float moveSpeed = 2;
    private float baseDamage = 5;
    private float curenttHealth = 100;
    private float meleeDamage = 5;
    private float meleeAttackRange = 1f;

    public string NpcName { get => npcName; set => npcName = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Movespeed { get => moveSpeed; set => moveSpeed = value; }
    public float BaseDamage { get => baseDamage; set => baseDamage = value; }
    public float CurenttHealth { get => curenttHealth; set => curenttHealth = value; }
    public float MeleeDamage { get => meleeDamage; set => meleeDamage = value; }
    public float MeleeAttackRange { get => meleeAttackRange; set => meleeAttackRange = value; }
}