using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class GameData
{
    private Vector3 playerPosition;
    private Vector3 foxPosition;
    private List<Vector3> npcsPosition;
    private Dictionary<int,int> playerBagItems;
    private int foxDialogindex;
    private PlayerData playerData;
    private NpcData npcData;
    public GameData()
    {
        PlayerPosition = new Vector3(1,0,0);
        FoxPosition = new Vector3(-2.84f,3.69f,0);
        NpcsPosition = new List<Vector3>();
        PlayerBagItems= new Dictionary<int,int>();
        FoxDialogindex = 0;
        NpcData = new NpcData();
        PlayerData = new PlayerData();
    }

    public Vector3 PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public Vector3 FoxPosition { get => foxPosition; set => foxPosition = value; }
    public List<Vector3> NpcsPosition { get => npcsPosition; set => npcsPosition = value; }
    public Dictionary<int, int> PlayerBagItems { get => playerBagItems; set => playerBagItems = value; }
    public int FoxDialogindex { get => foxDialogindex; set => foxDialogindex = value; }
    public PlayerData PlayerData { get => playerData; set => playerData = value; }
    public NpcData NpcData { get => npcData; set => npcData = value; }
}
