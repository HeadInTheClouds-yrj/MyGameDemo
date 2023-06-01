using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerData playerData;
    private Transform playerTransform;
    public Transform PlayerTransform
    {
        get
        {
            return this.transform;
        }
    }
    private void Awake()
    {
        instance = this;
        playerData = new PlayerData();
    }
    public float PlayerReduceHP(NpcCell npcCell)
    {
        playerData.CurenttHealth -= npcCell.npcData.BaseDamage;
        return playerData.CurenttHealth;
    }
}