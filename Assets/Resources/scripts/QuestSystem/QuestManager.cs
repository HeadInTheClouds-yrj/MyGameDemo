using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;
    private void Awake()
    {
        questMap = CreateQuestMap();
    }
    private void OnEnable()
    {
        EventManager.Instance.questEvent.OnStartQuest += StartQuest;
        EventManager.Instance.questEvent.OnAdvanceQuest += AdvanceQuest;
        EventManager.Instance.questEvent.OnFinishQuest += FinishQuest;
    }
    private void OnDisable()
    {
        EventManager.Instance.questEvent.OnStartQuest -= StartQuest;
        EventManager.Instance.questEvent.OnAdvanceQuest -= AdvanceQuest;
        EventManager.Instance.questEvent.OnFinishQuest -= FinishQuest;
    }
    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            EventManager.Instance.questEvent.QuestStateChange(quest);
        }
    }
    private void StartQuest(string id)
    {
        Debug.Log(id);
    }

    private void AdvanceQuest(string id)
    {
        Debug.Log(id);
    }

    private void FinishQuest(string id)
    {
        Debug.Log(id);
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
