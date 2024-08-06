using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XiuLianInteractManager : MonoBehaviour
{
    public static XiuLianInteractManager Instance;
    private CurrentXiuLianManu currentManu;
    private List<Item> items;
    private List<string> allInBagGongFaIds;
    private List<string> allInBagSkillIds;
    private List<GongFaInfoSO> unLearnedInBagGongFas;
    private List<SkillInfoSO> unLearnedInBagSkills;
    private List<GongFaInfoSO> learndeGongFas;
    private List<SkillInfoSO> learndeSkills;
    [Header("修炼界面的功法通用UI预制体")]
    [SerializeField] private GameObject gongFaUIPrefab;
    [Header("修炼界面的神通通用UI预制体")]
    [SerializeField]
    private GameObject skillUIPrefab;
    [Header("修炼界面功法,神通，阵法预制体的父类")]
    [SerializeField] private Transform content;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    private void Start()
    {
        Initialize();
    }
    private void OnEnable()
    {
        //SceneManager.sceneLoaded += Initialize;
    }
    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= Initialize;
    }

    public void Initialize()
    {
        currentManu = CurrentXiuLianManu.GongFaStudy;
        items = InventoryManager.Instance.GetInBagItems();
        GongFaInit();
        SkillInit();
    }
    private void GongFaInit()
    {

        allInBagGongFaIds = GetGongFaIds();
        learndeGongFas = GetLearnedGongFas(PlayerManager.instance.playerData);
        unLearnedInBagGongFas = GetUnLearnedInBagGongFas(PlayerManager.instance.playerData, allInBagGongFaIds);
    }
    private void SkillInit()
    {
        allInBagSkillIds = GetSkillIds();
        learndeSkills = GetLearnedSkill(PlayerManager.instance.playerData);
        unLearnedInBagSkills = GetUnLearnedInBagSkills(PlayerManager.instance.playerData,allInBagSkillIds);
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
    private List<string> GetSkillIds()
    {
        List<string> allInBagSkillIds = new List<string>();
        if (items == null)
        {
            Debug.LogWarning("背包items为空！");
            return null;
        }
        foreach (Item item in items)
        {

            if (!allInBagSkillIds.Contains(item.id) && Item.Itemkinde.Skill.Equals(item.itemkinde))
            {
                allInBagSkillIds.Add(item.id);
            }
        }
        return allInBagSkillIds;
    }
    private List<string> GetKindeItemIds(Item.Itemkinde itemkinde)
    {
        List<string> allInBagKindeIds = new List<string>();
        if (items == null)
        {
            Debug.LogWarning("背包items为空！");
            return null;
        }
        foreach (Item item in items)
        {

            if (!allInBagKindeIds.Contains(item.id) && itemkinde.Equals(item.itemkinde))
            {
                allInBagKindeIds.Add(item.id);
            }
        }
        return allInBagKindeIds;
    }
    private List<GongFaInfoSO> GetUnLearnedInBagGongFas(Data playerData, List<string> allInBagGongFaIds)
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
            foreach (string itemId in allInBagGongFaIds)
            {
                bool flag = false;
                foreach (GongFaInfoSO learned in learndeGongFas)
                {
                    if (itemId.Equals(learned.id))
                    {
                        flag= true;
                        break;
                    }
                }
                if (!flag)
                {
                    ulibgf.Add(GongFaManager.instance.GetInitGongFaById(itemId).gfInfo);
                }
            }
        }

        return ulibgf;
    }
    private List<SkillInfoSO> GetUnLearnedInBagSkills(Data playerData,List<string> allInBagSkillIds)
    {
        List<SkillInfoSO> ulibgf = new List<SkillInfoSO>();
        if (playerData.learnedSkills.Keys.Count == 0)
        {
            foreach (string itemId in allInBagSkillIds)
            {
                SkillInfoSO temp = SkiilManager.Instance.GetSkillInfoSOById(itemId);
                if (!ulibgf.Contains(temp))
                {
                    ulibgf.Add(temp);
                }
            }
        }
        else
        {
            foreach (string itemId in allInBagSkillIds)
            {
                bool flag = false;
                foreach (SkillInfoSO learned in learndeSkills)
                {
                    if (itemId.Equals(learned.id))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    ulibgf.Add(SkiilManager.Instance.GetSkillInfoSOById(itemId));
                }
            }
        }

        return ulibgf;
    }
    private List<GongFaInfoSO> GetLearnedGongFas(Data playerData)
    {
        List<GongFaInfoSO> libgf = new List<GongFaInfoSO>();
        foreach (string learnedId in playerData.learnedGongFas.Keys)
        {
            libgf.Add(GongFaManager.instance.GetInitGongFaById(learnedId).gfInfo);
        }
        return libgf;
    }
    private List<SkillInfoSO> GetLearnedSkill(Data playerData)
    {
        List<SkillInfoSO> learnedSkills = new List<SkillInfoSO>();
        foreach (string learnedId in playerData.learnedSkills.Keys)
        {
            learnedSkills.Add(SkiilManager.Instance.GetSkillInfoSOById(learnedId));
        }
        return learnedSkills;
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
    private void InitializeSkillToContent(List<SkillInfoSO> skillInfoSOs)
    {
        foreach (SkillInfoSO info in skillInfoSOs)
        {
            GameObject obj = Instantiate(skillUIPrefab,content);
            SkillXiuLianUISelectHandler skillUIHandler = obj.GetComponent<SkillXiuLianUISelectHandler>();
            Image skillSprite = obj.GetComponent<Image>();
            TMP_Text tmp_text = obj.transform.Find("Name").GetComponent<TMP_Text>();
            skillUIHandler.SetInfo(info);
            skillUIHandler.UpdateCurrentManuState(currentManu);
            skillSprite.sprite = info.skillInBattleIcon;
            tmp_text.text = info.skillName;
        }
    }
    public void ListUnLearnedInBagGongFa()
    {
        foreach (Transform gfTransform in content)
        {
            Destroy(gfTransform.gameObject);
        }
        ChangeCurrentManuState(CurrentXiuLianManu.GongFaStudy);
        InitializeGongFaToContent(unLearnedInBagGongFas);
    }
    public void ListUnLearnedInBagSkill()
    {
        foreach (Transform skill in content)
        {
            Destroy(skill.gameObject);
        }
        ChangeCurrentManuState(CurrentXiuLianManu.ShenTongStudy);
        InitializeSkillToContent(unLearnedInBagSkills);

    }
    public void RemoveUnLearnedInBagGongFaById(string gongFaId)
    {
        GongFaInfoSO info = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo;
        if (unLearnedInBagGongFas.Contains(info))
        {
            unLearnedInBagGongFas.Remove(info);
        }
    }
    public void RemoveUnlearnedInBagSkillById(string skillId)
    {
        SkillInfoSO info = SkiilManager.Instance.GetSkillInfoSOById(skillId);
        if (unLearnedInBagSkills.Contains(info))
        {
            unLearnedInBagSkills.Remove(info);
        }
    }
    public void ListlearnedGongFas()
    {
        foreach (Transform gfTransform in content)
        {
            Destroy(gfTransform.gameObject);
        }
        ChangeCurrentManuState(CurrentXiuLianManu.GongFaLevelUp);
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
    public void AddLearnedSkill(string skillId)
    {
        SkillInfoSO info = SkiilManager.Instance.GetSkillInfoSOById(skillId);
        if (!learndeSkills.Contains(info))
        {
            learndeSkills.Add(info);
        }
    }
    public void AddLearnedGongFaToPlayerData(GongFaInfoSO info)
    {
        if (!PlayerManager.instance.playerData.learnedGongFas.ContainsKey(info.id))
        {
            PlayerManager.instance.playerData.learnedGongFas.Add(info.id, 1);
        }
    }
    public void AddLearnedSkillToPlayerData(SkillInfoSO info)
    {
        if (!PlayerManager.instance.playerData.learnedSkills.ContainsKey(info.id))
        {
            PlayerManager.instance.playerData.learnedSkills.Add(info.id, 1);
        }
    }
    public void GongFaLevelUPToPlayerData(GongFaInfoSO info)
    {
        EventManager.Instance.gongFaEvent.GongFaLevelUP(info.id,PlayerManager.instance.transform);
    }
    public void ChangeCurrentManuState(CurrentXiuLianManu currentXiuLianManu)
    {
        currentManu = currentXiuLianManu;
    }
}
