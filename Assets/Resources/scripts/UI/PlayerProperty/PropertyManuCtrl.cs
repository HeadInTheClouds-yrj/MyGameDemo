using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertyManuCtrl : MonoBehaviour,IDataPersistence
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
    public void ListInstallGongFaBag()
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
    public void ListInstalledGongFaStaticImage()
    {
        for (int i = 0; i < data.installOrderGongFaIds.Length; i++)
        {
            if (data.installOrderGongFaIds[i] == null || data.installOrderGongFaIds[i] == "empty" || data.installOrderGongFaIds[i] == "")
            {
                continue;
            }
            gongFaButtons[i].GetComponent<Image>().sprite = GongFaManager.instance.GetInitGongFaById(data.installOrderGongFaIds[i]).gfInfo.gongFaInBattleIcon;
            gongFaButtons[i].Find("Name").GetComponent<TMP_Text>().text = GongFaManager.instance.GetInitGongFaById(data.installOrderGongFaIds[i]).gfInfo.gongFaName;
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().GongFaId = data.installOrderGongFaIds[i];
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().GongFaLevel = data.learnedGongFas[data.installOrderGongFaIds[i]];
        }
    }
    public void InitStaticGongFaManuIndex()
    {
        for (int i = 0; i < 9; i++)
        {
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().InStaticGongFaIndex = i;
        }
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
