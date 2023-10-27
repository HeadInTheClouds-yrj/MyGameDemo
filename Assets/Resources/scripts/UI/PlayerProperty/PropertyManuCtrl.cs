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
    [Header("��������ҳ��Ԫ��")]
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
    [Header("����ҳ��Ԫ��")]
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
    ///����Ϊ����ҳ��ʹ��
    public void ShowDataProperty()
    {
        base_Name.text = "����:" + data.Name;
        base_MaxAge.text = "��Ԫ:" + data.MaxAge.ToString();
        base_CurrentAge.text = "����:" + data.CurrentAge.ToString();
        base_HP.text = "Ѫ��:" + data.CurenttHealth + " / " + data.MaxHealth;
        base_LingQi.text = "����ֵ:" + data.CurrentLingQi + " / " + data.MaxLingQi;
        base_RegenerateLingQi.text = "�����ظ�:" + data.RegenerateLingQi + "/ÿ��";
        base_LingShi.text = "��ʯ:" + data.LingShi.ToString();
    }
    ///����Ϊ����ҳ��ʹ��
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
                questType.text = "����";
            }
            else
            {
                questType.text = "֧��";
            }
        }
    }
}
