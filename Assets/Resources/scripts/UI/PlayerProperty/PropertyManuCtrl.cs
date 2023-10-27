using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertyManuCtrl : MonoBehaviour
{
    public static PropertyManuCtrl instance;
    private Data data;
    private Dictionary<string, Quest> questMap_ReadOnly;
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
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Initialize();
    }
    private void Initialize()
    {
        data = PlayerManager.instance.playerData;
    }
    ///以下为属性页面使用
    public void ShowDataProperty()
    {
        base_Name.text = "名字:" + data.Name;
        base_MaxAge.text = "寿元:" + data.MaxAge.ToString();
        base_CurrentAge.text = "年龄:" + data.CurrentAge.ToString();
        base_HP.text = "血量:" + data.CurenttHealth + " / " + data.MaxHealth;
        base_LingQi.text = "灵气值:" + data.CurrentLingQi + " / " + data.MaxLingQi;
        base_RegenerateLingQi.text = "灵气回复:" + data.RegenerateLingQi + "/每秒";
        base_LingShi.text = "灵石:" + data.LingShi.ToString();
    }
    ///以下为任务页面使用
    public void SetQuestMap(Dictionary<string, Quest> questMap_OnlyRead)
    {
        this.questMap_ReadOnly = questMap_OnlyRead;
    }
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
}
