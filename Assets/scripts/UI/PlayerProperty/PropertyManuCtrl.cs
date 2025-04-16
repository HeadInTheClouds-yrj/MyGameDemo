using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PropertyManuCtrl : MonoBehaviour
{
    public static PropertyManuCtrl instance;
    private Data data;
    public Dictionary<string, Quest> questMap_ReadOnly;
    [Header("基本属性页面元素")]
    [SerializeField]
    private TMP_Text base_Name;
    [SerializeField]
    private TMP_Text base_MaxAge;
    [SerializeField]
    private TMP_Text base_CurrentAge;
    [SerializeField]
    private TMP_Text base_HP;
    [SerializeField]
    private TMP_Text base_LingQi;
    [SerializeField]
    private TMP_Text base_RegenerateLingQi;
    [SerializeField]
    private TMP_Text base_LingShi;
    [Header("任务页面元素")]
    [SerializeField]
    private Transform questContent;
    [SerializeField]
    private GameObject questContentPrefab;
    [Header("装备功法页面元素")]
    [SerializeField] private Transform installGongFaBagContent;
    [SerializeField] private GameObject installGongFaPfb;
    [SerializeField] private RectTransform zhuXiuGongFa;
    [SerializeField] private RectTransform GongFa_1;
    [SerializeField] private RectTransform GongFa_2;
    [SerializeField] private RectTransform GongFa_3;
    [SerializeField] private RectTransform GongFa_4;
    [SerializeField] private RectTransform GongFa_5;
    [SerializeField] private RectTransform GongFa_6;
    [SerializeField] private RectTransform GongFa_7;
    [SerializeField] private RectTransform GongFa_8;
    private List<RectTransform> gongFaButtons;
    [Header("装备神通页面元素")]
    [SerializeField] private Transform installSkillBagContent;
    [SerializeField] private GameObject installSkillPfb;
    [SerializeField] private RectTransform zhuXiuSkill;
    [SerializeField] private RectTransform skill_1;
    [SerializeField] private RectTransform skill_2;
    [SerializeField] private RectTransform skill_3;
    [SerializeField] private RectTransform skill_4;
    [SerializeField] private RectTransform skill_5;
    [SerializeField] private RectTransform skill_6;
    [SerializeField] private RectTransform skill_7;
    [SerializeField] private RectTransform skill_8;
    [SerializeField] private RectTransform skill_9;
    private List<RectTransform> skillButtons;
    [SerializeField]
    private Sprite defaultGongFaSprite;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Initialize();
        
    }
    private void Start()
    {
        SkiilManager.Instance.RegisteSkillKeyBind(skillButtons);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    public void Initialize()
    {
        EventManager.Instance.questEvent.GetQuestMapToPropertyUI();
        data = PlayerManager.instance.playerData;
        gongFaButtons = new List<RectTransform>
        {
            zhuXiuGongFa,
            GongFa_1,
            GongFa_2,
            GongFa_3,
            GongFa_4,
            //额外功法位置
            GongFa_5,
            GongFa_6,
            GongFa_7,
            GongFa_8
        };
        skillButtons = new List<RectTransform>
        {
            zhuXiuSkill,
            skill_1,
            skill_2,
            skill_3,
            skill_4,
            skill_5,
            skill_6,
            skill_7,
            skill_8,
            skill_9
        };
        InitStaticGongFaManuIndex();
    }
    ///以下为属性页面使用
    public void ShowDataProperty()
    {
        base_Name.text = "名字:" + data.name;
        base_MaxAge.text = "寿元:" + data.maxAge.ToString();
        base_CurrentAge.text = "年龄:" + data.currentAge.ToString();
        base_HP.text = "血量:" + data.curenttHealth + " / " + data.maxHealth;
        base_LingQi.text = "灵气值:" + data.currentLingQi + " / " + data.maxLingQi;
        base_RegenerateLingQi.text = "灵气回复:" + data.regenerateLingQi + "/每秒";
        base_LingShi.text = "灵石:" + data.lingShi.ToString();

        EventManager.Instance.gameStateEvent.ChangeGameState(State.UI);
    }
    ///以下为任务页面使用

    public void ListQuest1UI()
    {
        foreach (Transform item in questContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Quest quest in questMap_ReadOnly.Values)
        {
            GameObject obj = Instantiate<GameObject>(questContentPrefab, questContent);
            QuestDisplayUICtrl questUICtrl = obj.GetComponent<QuestDisplayUICtrl>();
            var questButton = obj.GetComponent<Button>();
            var questName = obj.transform.Find("QuestName").GetComponent<TMP_Text>();
            var questType = obj.transform.Find("QuestType").GetComponent<TMP_Text>();
            questUICtrl.SetQuest(quest);
            questButton.onClick.RemoveAllListeners();
            questButton.onClick.AddListener(questUICtrl.ShowQuestState);
            questName.text = quest.info.id;
            if (quest.info.questType.Equals(QuestType.MainQuest))
            {
                questType.text = "主线";
            }
            else
            {
                questType.text = "支线";
            }
        }
    }
    public void ListLearnedGongFaBag()
    {
        foreach (Transform item in installGongFaBagContent)
        {
            Destroy(item.gameObject);
        }
        foreach (string gongFaId in data.learnedGongFas.Keys)
        {
            GameObject obj = Instantiate<GameObject>(installGongFaPfb, installGongFaBagContent);
            InstallGongFaBagButton installGong = obj.GetComponent<InstallGongFaBagButton>();
            var gongFaButton = obj.GetComponent<Image>();
            var gongFaName = obj.transform.Find("Name").GetComponent<TMP_Text>();
            installGong.SetGongFaButton(gongFaId, data.learnedGongFas[gongFaId],gongFaButtons);
            gongFaButton.sprite = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo.gongFaInBattleIcon;
            gongFaName.text = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo.name;
        }
    }
    public void ListLearnedSkillBag()
    {
        foreach (Transform item in installSkillBagContent) { Destroy(item.gameObject); }
        foreach (string skillId in data.learnedSkills.Keys)
        {
            GameObject obj = Instantiate<GameObject>(installSkillPfb, installSkillBagContent);
            InstallSkillBagButton installSkill = obj.GetComponent<InstallSkillBagButton>();
            var skillButton = obj.GetComponent<Image>();
            var skillName = obj.transform.Find("Name").GetComponent<TMP_Text>();
            SkillInfoSO tempInfo = SkiilManager.Instance.GetSkillInfoSOById(skillId);
            installSkill.SetSkillMessage(tempInfo,skillButtons);
            skillButton.sprite = tempInfo.skillInBattleIcon;
            skillName.text = tempInfo.skillName;
        }
    }
    public void ListInstalledGongFaStaticImage()
    {
        for (int i = 0; i < data.installOrderGongFaIds.Length; i++)
        {
            if (data.installOrderGongFaIds[i] == null || data.installOrderGongFaIds[i] == "empty" || data.installOrderGongFaIds[i] == "")
            {
                gongFaButtons[i].GetComponent<Image>().sprite = defaultGongFaSprite;
                gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().GongFaId = null;
                continue;
            }
            gongFaButtons[i].GetComponent<Image>().sprite = GongFaManager.instance.GetInitGongFaById(data.installOrderGongFaIds[i]).gfInfo.gongFaInBattleIcon;
            gongFaButtons[i].Find("Name").GetComponent<TMP_Text>().text = GongFaManager.instance.GetInitGongFaById(data.installOrderGongFaIds[i]).gfInfo.gongFaName;
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().GongFaId = data.installOrderGongFaIds[i];
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().GongFaLevel = data.learnedGongFas[data.installOrderGongFaIds[i]];
        }
    }
    public void ListInstalledSkillStaticImage()
    {
        for (int i = 0; i < data.installOrderSkillIds.Length; i++)
        {
            if (data.installOrderSkillIds[i] == null || data.installOrderSkillIds[i] == "empty" || data.installOrderSkillIds[i] == "")
            {
                skillButtons[i].GetComponent<Image>().sprite = null;
                skillButtons[i].GetComponent<InstalledSkillUIHandler>().SetSkillInfoSO(null);
                skillButtons[i].GetComponent<InstalledSkillUIHandler>().SetDisplayKeyCode(SkiilManager.Instance.GetSkillBindKeyByIndex(i));
                continue;
            }
            skillButtons[i].GetComponent<Image>().sprite = SkiilManager.Instance.GetSkillInfoSOById(data.installOrderSkillIds[i]).skillInBattleIcon;
            skillButtons[i].Find("Name").GetComponent<TMP_Text>().text = SkiilManager.Instance.GetSkillInfoSOById(data.installOrderSkillIds[i]).skillName;
            skillButtons[i].GetComponent<InstalledSkillUIHandler>().SetSkillInfoSO(SkiilManager.Instance.GetSkillInfoSOById(data.installOrderSkillIds[i]));
            skillButtons[i].GetComponent<InstalledSkillUIHandler>().SetDisplayKeyCode(SkiilManager.Instance.GetSkillBindKeyByIndex(i));
        }
    }
    public void InitStaticGongFaManuIndex()
    {
        for (int i = 0; i < 9; i++)
        {
            Debug.Log(i);
            Debug.Log(gongFaButtons[i] == null);
            Debug.Log(gongFaButtons[i].GetComponent<InstallStaticGongFaUI>() == null); 
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().InStaticGongFaIndex = i;
        }
    }
    public List<RectTransform> GetSkillRectTransforms()
    {
        return skillButtons;
    }
    public void ListBag()
    {
        InventoryManager.Instance.ListItems();
        InventoryManager.Instance.setItemcontrol();
    }

    public void LoadGame(GameData gameData)
    {
    }

    public void SaveGame(GameData gameData)
    {
        
    }
}
