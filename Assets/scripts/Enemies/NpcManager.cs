using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcManager : MonoBehaviour,IDataPersistence
{
    public static NpcManager instance;
    private int nameFanolyId = 0;
    public LayerMask tree;
    [SerializeField] private TimeCount timeCount;
    private Dictionary<int, List<EnemyData>> allSceneNPCData; 
    public void Awake()
    {
        instance= this;
        allNpcCell = new Dictionary<string, NpcCell>();
        allSceneNPCData = new Dictionary<int, List<EnemyData>>();

    }
    private void OnEnable()
    {
        EventManager.Instance.enimiesEvent.OnEnimyDie += Ondead;
        SceneManager.sceneLoaded += UpdateNPCList;
    }

    private void UpdateNPCList(Scene arg0, LoadSceneMode arg1)
    {
        allNpcCell.Clear();
        StartCoroutine(UpdateNPCDatas());
    }



    IEnumerator UpdateNPCDatas()
    {
        if (allSceneNPCData.ContainsKey(SceneManager.GetActiveScene().buildIndex))
        {
            InitializeNPC(allSceneNPCData[SceneManager.GetActiveScene().buildIndex]);
        }
        yield return null;
        
    }


    public List<NpcCell> GetNpcCells()
    {
        List<NpcCell> npcCells = new List<NpcCell>();
        foreach (var item in allNpcCell.Values)
        {
            npcCells.Add(item);
        }
        return npcCells;
    }

    private void Ondead(NpcCell npcCell)
    {
        PlayerManager.instance.playerData.killEnimiesCont++;
        RemoveNpcCell(npcCell);
        timeCount.SetTime(1.5f);
        RemoveNpcCell(npcCell);
        RemoveCurrentNPCData(npcCell.name);
        Destroy(npcCell.gameObject, 2f);
    }
   
    private Dictionary<string,NpcCell> allNpcCell;
    public GameObject factoryNpc(string path = "npcs/enemy",Vector3 position = new Vector3())
    {
        GameObject tempObj = (GameObject)Resources.Load(path);
        GameObject NPC = GameObject.Instantiate(tempObj);
        string npcname = NPC.name;
        string[] strnaem = npcname.Split("(");
        NPC.name = strnaem[0]+"_"+nameFanolyId++;
        NPC.transform.position = position;
        NPC.transform.AddComponent<NpcCell>();
        return NPC;
    }
    private void InitializeNPC(List<EnemyData> datas)
    {
        Debug.Log("初始化");
    }
    private EnemyData GetCurrentSceneNPCDataByID(string id)
    {
        foreach (EnemyData data in allSceneNPCData[SceneManager.GetActiveScene().buildIndex])
        {
            if (id.Equals(data.id))
            {
                return data;
            }
        }
        return null;
    }
    public void registeToManager(string npcName,NpcCell npcCell)
    {
        if (!allNpcCell.ContainsKey(npcName))
        {
            allNpcCell[npcName] = npcCell;
        }
        else
        {
            Debug.Log("已经存在此npc！！！");
        }
        if (!CurrentSceneIsExistEnemyData(npcName))
        {
            Debug.Log("registe");
            if (allSceneNPCData.ContainsKey(SceneManager.GetActiveScene().buildIndex))
            {
                allSceneNPCData[SceneManager.GetActiveScene().buildIndex].Add(npcCell.GetData());
            }
            else
            {
                allSceneNPCData.Add(SceneManager.GetActiveScene().buildIndex, new List<EnemyData>());
                allSceneNPCData[SceneManager.GetActiveScene().buildIndex].Add(npcCell.GetData());
            }
            
        }

    }
    private bool CurrentSceneIsExistEnemyData(string npcName)
    {
        bool flag = false;
        if (allSceneNPCData.ContainsKey(SceneManager.GetActiveScene().buildIndex))
        {
            foreach (EnemyData item in allSceneNPCData[SceneManager.GetActiveScene().buildIndex])
            {
                Debug.Log(item.id + "===" + npcName);
                if (item.id.Equals(npcName))
                {
                    flag = true;
                    return flag;
                }
            }
        }
        
        return flag;
    }
    public NpcCell GetNpcCell(string npcName)
    {
        if (allNpcCell.ContainsKey(npcName)) { return allNpcCell[npcName]; } else {Debug.Log("寻找的npc不存在！！！"); return null; }
    }
    public Dictionary<int,List<EnemyData>> GetAllSceneNPCData()
    {
        return allSceneNPCData;
    }
    public void RemoveNpcCell(NpcCell npcCell)
    {
        allNpcCell.Remove(npcCell.name);
    }
    public void RemoveCurrentNPCData(string id)
    {
        allSceneNPCData[SceneManager.GetActiveScene().buildIndex].Remove(GetCurrentSceneNPCDataByID(id));
    }
    public Dictionary<string,NpcCell> getAllNpcCell()
    {
        
        return allNpcCell; 
    }
    public void LoadGame(GameData gameData)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (gameData.enimies.ContainsKey(i))
            {
                if (allSceneNPCData[i] != null)
                {
                    allSceneNPCData[i].Clear();
                }
                allSceneNPCData.Add(i,gameData.enimies[i].GetScenceEnemiesGroup());
            }
        }
        Debug.Log("load");
    }

    public void SaveGame(GameData gameData)
    {
        foreach (NpcCell cell in allNpcCell.Values)
        {
            cell.UpdateData();
        }
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Debug.Log(SceneManager.sceneCountInBuildSettings);
            if (allSceneNPCData.ContainsKey(i))
            {
                if (!gameData.enimies.ContainsKey(i))
                {
                    gameData.enimies.Add(i, new EnemiesDataGroup());
                    gameData.enimies[i].ImportEnemyDataByObject(allSceneNPCData[i]);
                }
                else
                {
                    gameData.enimies[i].ImportEnemyDataByObject(allSceneNPCData[i]);
                }
                //else
                //{
                //    gameData.enimies[i] = allSceneNPCData[i][j].id;
                //}
                //gameData.enimies[i][j].id = allSceneNPCData[i][j].id;
                //Debug.Log("allsceneNpcData:===\t" + allSceneNPCData[i][j].id);
                //Debug.Log("gameData:====\t" + gameData.enimies[i][j].id);
                //foreach (var item in gameData.enimies[i])
                //{
                //    Debug.Log(item.currentLingQi + item.id + "\n");
                //}
                //gameData.enimies[i][j].curenttHealth = allSceneNPCData[i][j].curenttHealth;
                //gameData.enimies[i][j].maxHealth = allSceneNPCData[i][j].maxHealth;
                //gameData.enimies[i][j].currentPosition = allSceneNPCData[i][j].currentPosition;
                //gameData.enimies[i][j].moveSpeed = allSceneNPCData[i][j].moveSpeed;
            }
        }
    }
}
