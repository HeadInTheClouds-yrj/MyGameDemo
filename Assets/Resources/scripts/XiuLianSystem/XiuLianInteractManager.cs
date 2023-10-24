using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class XiuLianInteractManager : MonoBehaviour
{
    private CurrentXiuLianManu currentManu;
    private List<Item> items;
    private List<string> allInBagGongFaIds;
    private List<GongFaInfoSO> unLearnedInBagGongFas;
    private List<GongFaInfoSO> learndeGongFas;
    private Data playerData;
    [SerializeField] private GameObject gongFaUIPrefab;
    [SerializeField] private Transform content;
    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        currentManu = CurrentXiuLianManu.GongFaStudy;
        playerData = PlayerManager.instance.playerData;
        items = InventoryManager.Instance.GetInBagItems();
        allInBagGongFaIds = GetGongFaIds();
        unLearnedInBagGongFas = GetUnLearnedInBagGongFas(playerData);
        learndeGongFas = GetLearnedGongFas(playerData);
    }
    private List<string> GetGongFaIds()
    {
        List<string> allInBagGongFaIds = new List<string>();
        if (items == null)
        {
            Debug.LogWarning("±³°üitemsÎª¿Õ£¡");
            return null;
        }
        foreach (Item item in items)
        {
            if (!allInBagGongFaIds.Contains(item.id)&& "GongFa".Equals(item.itemkinde))
            {
                allInBagGongFaIds.Add(item.id);
            }
        }
        return allInBagGongFaIds;
    }
    private List<GongFaInfoSO> GetUnLearnedInBagGongFas(Data playerData)
    {
        List<GongFaInfoSO> ulibgf = new List<GongFaInfoSO>();
        foreach (string learnedId in playerData.LearnedGongFa.Keys)
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
        return ulibgf;
    }
    private List<GongFaInfoSO> GetLearnedGongFas(Data playerData)
    {
        List<GongFaInfoSO> libgf = new List<GongFaInfoSO>();
        foreach (string learnedId in playerData.LearnedGongFa.Keys)
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
            var gongFaSprite = obj.transform.Find("GongFa").GetComponent<Image>();
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

    public void ListLearnedGongFas()
    {
        foreach (Transform gfTransform in content)
        {
            Destroy(gfTransform.gameObject);
        }
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
}
