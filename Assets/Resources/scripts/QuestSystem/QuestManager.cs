using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;
    private int kiiEnimyCount = 0;
    private void Awake()
    {
        questMap = CreateQuestMap();
    }
    private void OnEnable()
    {
        EventManager.Instance.questEvent.OnStartQuest += StartQuest;
        EventManager.Instance.questEvent.OnAdvanceQuest += AdvanceQuest;
        EventManager.Instance.questEvent.OnFinishQuest += FinishQuest;
        EventManager.Instance.enimiesEvent.OnEnimyDie += EnimyDie;
    }

    private void EnimyDie(NpcCell obj)
    {
        kiiEnimyCount++;
    }

    private void OnDisable()
    {
        EventManager.Instance.questEvent.OnStartQuest -= StartQuest;
        EventManager.Instance.questEvent.OnAdvanceQuest -= AdvanceQuest;
        EventManager.Instance.questEvent.OnFinishQuest -= FinishQuest;
        EventManager.Instance.enimiesEvent.OnEnimyDie -= EnimyDie;
    }
    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            EventManager.Instance.questEvent.QuestStateChange(quest);
        }
    }
    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }
    private void ChangeQuestState(string id , QuestState questState)
    {
        Quest quest = GetQuestById(id);
        if (quest != null)
        {
            quest.state = questState;
            EventManager.Instance.questEvent.QuestStateChange(quest);
        }
    }
    private void StartQuest(string id)
    {
        
    }

    private void AdvanceQuest(string id)
    {
        
    }

    private void FinishQuest(string id)
    {
        
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetRequirements = true;
        if (kiiEnimyCount < quest.info.killEnimiesCont)
        {
            meetRequirements = false;
        }
        foreach (QuestInfoSO questInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(questInfo.id).state != QuestState.FINISHED)
            {
                meetRequirements = false;
            }
        }
        return meetRequirements;
    }
    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuest = Resources.LoadAll<QuestInfoSO>("Quests");
        Dictionary<string,Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuest)
        {
            if (!idToQuestMap.ContainsKey(questInfo.id))
            {
                idToQuestMap.Add(questInfo.id,new Quest(questInfo));
            }
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogWarning("任务不在QuestMap中");
        }
        return quest;
    }
}
