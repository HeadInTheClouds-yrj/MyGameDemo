using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class XiuLianInteractManager : MonoBehaviour
{
    private List<Item> items;
    private List<string> allInBagGongFaIds;
    private List<GongFaInfoSO> unLearnedInBagGongFas;
    private Data playerData;
    [SerializeField] private GameObject gongFaUIPrefab;
    [SerializeField] private Transform content;
    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        playerData = PlayerManager.instance.playerData;
        items = InventoryManager.Instance.GetInBagItems();
        allInBagGongFaIds = GetGongFaIds();
        unLearnedInBagGongFas = GetUnLearnedInBagGongFas(playerData);
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
    public void ListAllGongFa()
    {
        foreach (Transform gfTransform in content)
        {
            Destroy(gfTransform.gameObject);
        }
        foreach (GongFaInfoSO info in unLearnedInBagGongFas)
        {
            GameObject obj = Instantiate<GameObject>(gongFaUIPrefab,content);
            var gongFaSprite = obj.transform.Find("GongFa").GetComponent<Image>();
            var gongFaName = obj.transform.Find("Name").GetComponent<TMP_Text>();
            gongFaSprite.sprite = info.gongFaInBattleIcon;
            gongFaName.text = info.gongFaName;
        }
    }
}
