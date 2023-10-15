using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;//ÕÊº“Œª÷√
    private Vector3 cameraPosition;
    public Vector3 foxPosition;
    public List<Vector3> npcsPosition;
    public List<string> bagItemsId;
    //private Dictionary<int,int> playerBagItems;
    public int foxDialogindex;
    //private PlayerData playerData;
    //private NpcData npcData;
    public string playerName = "beastmaster";
    public float maxHealth;
    public float baseDamage;
    public float curenttHealth;
    public float meleeDamage;
    public float attackAngle;
    public float meleeAttackRange;
    public float rangedDamage;
    public float playerMoveSpeed;
    public float lrX;
    public List<QuestData> questDatas;
    public GameData()
    {
        PlayerPosition = new Vector3(1,0,0);
        FoxPosition = new Vector3(-2.84f,3.69f,0);
        NpcsPosition = new List<Vector3>();
        bagItemsId = new List<string>();
        //PlayerBagItems= new Dictionary<int,int>();
        FoxDialogindex = 0;
        //NpcData = new NpcData();
        //PlayerData = new PlayerData();
        playerName = "beastmaster";
        maxHealth = 100;
        baseDamage = 5;
        curenttHealth = 100;
        meleeDamage = 10;
        attackAngle = 60;
        meleeAttackRange = 1.5f;
        rangedDamage = 5f;
        playerMoveSpeed = 2;
        lrX= 2.01f;
        questDatas = new List<QuestData>();
}

    public Vector3 PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public Vector3 FoxPosition { get => foxPosition; set => foxPosition = value; }
    public List<Vector3> NpcsPosition { get => npcsPosition; set => npcsPosition = value; }
    //public Dictionary<int, int> PlayerBagItems { get => playerBagItems; set => playerBagItems = value; }
    public int FoxDialogindex { get => foxDialogindex; set => foxDialogindex = value; }
    public Vector3 CameraPosition { get => cameraPosition; set => cameraPosition = value; }
    //public PlayerData PlayerData { get => playerData; set => playerData = value; }
    //public NpcData NpcData { get => npcData; set => npcData = value; }
    public string PlayerName { get => playerName; set => playerName = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float BaseDamage { get => baseDamage; set => baseDamage = value; }
    public float CurenttHealth { get => curenttHealth; set => curenttHealth = value; }
    public float MeleeDamage { get => meleeDamage; set => meleeDamage = value; }
    public float MeleeAttackRange { get => meleeAttackRange; set => meleeAttackRange = value; }
    public float AttackAngle { get => attackAngle; set => attackAngle = value; }
    public float RangedDamage { get => rangedDamage; set => rangedDamage = value; }
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }
    public List<string> BagItemsId { get => bagItemsId; set => bagItemsId = value; }
}
