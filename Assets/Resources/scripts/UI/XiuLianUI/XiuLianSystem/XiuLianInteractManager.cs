using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XiuLianInteractManager : MonoBehaviour,IDataPersistence
{
    public static XiuLianInteractManager Instance;
    private CurrentXiuLianManu currentManu;
    private List<Item> items;
    private List<string> allInBagGongFaIds;
    private List<GongFaInfoSO> unLearnedInBagGongFas;
    private List<GongFaInfoSO> learndeGongFas;
    [Header("修炼界面的功法预制体")]
    [SerializeField] private GameObject gongFaUIPrefab;
    [Header("修炼界面功法预制体的父类")]
    [SerializeField] private Transform content;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    public void Initialize()
    {
        currentManu = CurrentXiuLianManu.GongFaStudy;
        items = InventoryManager.Instance.GetInBagItems();
        allInBagGongFaIds = GetGongFaIds();
        unLearnedInBagGongFas = GetUnLearnedInBagGongFas(PlayerManager.instance.playerData);
        learndeGongFas = GetlearnedGongFas(PlayerManager.instance.playerData);
    }
    private List<string> GetGongFaIds()
    {
        List<string> allInBagGongFaIds = new List<string>();
        if (items == null)
        {
            Debug.LogWarning("背包items为空！");
            return null;
        }
        foreach (Item item in items)
        {
            
            if (!allInBagGongFaIds.Contains(item.id)&& Item.Itemkinde.GongFa.Equals(item.itemkinde))
            {
                allInBagGongFaIds.Add(item.id);
            }
        }
        return allInBagGongFaIds;
    }
    private List<GongFaInfoSO> GetUnLearnedInBagGongFas(Data playerData)
    {
        List<GongFaInfoSO> ulibgf = new List<GongFaInfoSO>();
        if (playerData.learnedGongFas.Keys.Count == 0)
        {
            foreach (string itemId in allInBagGongFaIds)
            {
                GongFaInfoSO temp = GongFaManager.instance.GetInitGongFaById(itemId).gfInfo;
                if (!ulibgf.Contains(temp))
                {
                    ulibgf.Add(temp);
                }
            }
        }
        else
        {
            foreach (string learnedId in playerData.learnedGongFas.Keys)
            {
                foreach (string itemId in allInBagGongFaIds)
                {
                    if (!allInBagGongFaIds.Contains(learnedId))
                    {
                        GongFaInfoSO temp = GongFaManager.instance.GetInitGongFaById(itemId).gfInfo;
                        if (!ulibgf.Contains(temp))
                        {
                            ulibgf.Add(temp);
                        }
                    }
                }
            }
        }

        return ulibgf;
    }
    private List<GongFaInfoSO> GetlearnedGongFas(Data playerData)
    {
        List<GongFaInfoSO> libgf = new List<GongFaInfoSO>();
        foreach (string learnedId in playerData.learnedGongFas.Keys)
        {
            libgf.Add(GongFaManager.instance.GetInitGongFaById(learnedId).gfInfo);
        }
        return libgf;
    }
    private void InitializeGongFaToContent(List<GongFaInfoSO> gongFas)
    {
        foreach (GongFaInfoSO info in gongFas)
        {
            GameObject obj = Instantiate<GameObject>(gongFaUIPrefab, content);
            GongFaSelectHandler gfsh = obj.GetComponent<GongFaSelectHandler>();
            var gongFaSprite = obj.GetComponent<Image>();
            var gongFaName = obj.transform.Find("Name").GetComponent<TMP_Text>();
            gfsh.SetInfo(info);
            gfsh.UpdateCurrentManuState(currentManu);
            gongFaSprite.sprite = info.gongFaInBattleIcon;
            gongFaName.text = info.gongFaName;
        }
    }
    public void ListUnLearnedInBagGongFa()
    {
        foreach (Transform gfTransform in content)
        {
            Destroy(gfTransform.gameObject);
        }
        ChanGeManuStateGongFaToStudy();
        InitializeGongFaToContent(unLearnedInBagGongFas);
    }

    public void RevomUnLearnedInBagGongFaById(string gongFaId)
    {
        GongFaInfoSO info = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo;
        if (unLearnedInBagGongFas.Contains(info))
        {
            unLearnedInBagGongFas.Remove(info);
        }
    }

    public void ListlearnedGongFas()
    {
        foreach (Transform gfTransform in content)
        {
            Destroy(gfTransform.gameObject);
        }
        ChanGeManuStateToGongFaLevelUp();
        InitializeGongFaToContent(learndeGongFas);
    }
    public void AddLearnedGongFa(string gongFaId)
    {
        GongFaInfoSO info = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo;
        if (!learndeGongFas.Contains(info))
        {
            learndeGongFas.Add(info);
        }
    }
    public void AddLearnedGongFaToPlayerData(GongFaInfoSO info)
    {
        if (!PlayerManager.instance.playerData.learnedGongFas.ContainsKey(info.id))
        {
            PlayerManager.instance.playerData.learnedGongFas.Add(info.id, 1);
        }
    }
    public void GongFaLevelUPToPlayerData(GongFaInfoSO info)
    {
        //if (playerData.LearnedGongFa.ContainsKey(info.id))
        //{
        //    if (playerData.LearnedGongFa[info.id] < info.gongFaMaxLevel)
        //    {
        //        playerData.LearnedGongFa[info.id]++;
        //        if (playerData.InstaillGongFa.ContainsKey(info.id))
        //        {
        //            playerData.InstaillGongFa[info.id]++;
        //        }
        //    }
        //    else
        //    {
        //        playerData.LearnedGongFa[info.id]=info.gongFaMaxLevel;
        //        if (playerData.InstaillGongFa.ContainsKey(info.id))
        //        {
        //            playerData.InstaillGongFa[info.id] = info.gongFaMaxLevel;
        //        }
        //    }
        //}
        EventManager.Instance.gongFaEvent.GongFaLevelUP(info.id,PlayerManager.instance.transform);
    }
    public void ChanGeManuStateGongFaToStudy()
    {
        currentManu = CurrentXiuLianManu.GongFaStudy;
    }
    public void ChanGeManuStateToGongFaLevelUp()
    {
        currentManu = CurrentXiuLianManu.GongFaLevelUp;
    }

    public void LoadGame(GameData gameData)
    {
        Initialize();
    }

    public void SaveGame(GameData gameData)
    {

    }
}
