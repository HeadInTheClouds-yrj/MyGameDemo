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
    [Header("װ������ҳ��Ԫ��")]
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
    private void Initialize()
    {
        data = PlayerManager.instance.playerData;
        gongFaButtons = new List<RectTransform>
        {
            zhuXiuGongFa,
            GongFa_1,
            GongFa_2,
            GongFa_3,
            GongFa_4,
            //���⹦��λ��
            GongFa_5,
            GongFa_6,
            GongFa_7,
            GongFa_8
        };
        InitStaticGongFaManuIndex();
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
    public void ListInstallGongFaBag()
    {
        foreach (Transform item in installGongFaBagContent)
        {
            Destroy(item.gameObject);
        }
        foreach (string gongFaId in data.LearnedGongFas.Keys)
        {
            GameObject obj = Instantiate<GameObject>(installGongFaPfb, installGongFaBagContent);
            InstallGongFaBagButton installGong = obj.GetComponent<InstallGongFaBagButton>();
            var gongFaButton = obj.GetComponent<Image>();
            var gongFaName = obj.transform.Find("Name").GetComponent<TMP_Text>();
            installGong.SetGongFaButton(gongFaId, data.LearnedGongFas[gongFaId],gongFaButtons);
            gongFaButton.sprite = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo.gongFaInBattleIcon;
            gongFaName.text = GongFaManager.instance.GetInitGongFaById(gongFaId).gfInfo.name;
        }
    }
    public void ListInstalledGongFaStaticImage()
    {
        for (int i = 0; i < data.InstallOrderGongFaIds.Length; i++)
        {
            if (data.InstallOrderGongFaIds[i] == null || data.InstallOrderGongFaIds[i] == "")
            {
                continue;
            }
            gongFaButtons[i].GetComponent<Image>().sprite = GongFaManager.instance.GetInitGongFaById(data.InstallOrderGongFaIds[i]).gfInfo.gongFaInBattleIcon;
            gongFaButtons[i].Find("Name").GetComponent<TMP_Text>().text = GongFaManager.instance.GetInitGongFaById(data.InstallOrderGongFaIds[i]).gfInfo.gongFaName;
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().GongFaId = data.InstallOrderGongFaIds[i];
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().GongFaLevel = data.LearnedGongFas[data.InstallOrderGongFaIds[i]];
        }
    }
    public void InitStaticGongFaManuIndex()
    {
        for (int i = 0; i < 9; i++)
        {
            gongFaButtons[i].GetComponent<InstallStaticGongFaUI>().InStaticGongFaIndex = i;
        }
    }
}
