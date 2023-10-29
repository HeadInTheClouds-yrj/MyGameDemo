using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour,IDataPersistence
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
        EventManager.Instance.questEvent.OnQuestStepStateChange += QuestStepStateChange;
        EventManager.Instance.questEvent.OnGetQuestMapToPropertyUI += SetQuestMap;
        EventManager.Instance.enimiesEvent.OnEnimyDie += EnimyDie;
    }



    private void OnDisable()
    {
        EventManager.Instance.questEvent.OnStartQuest -= StartQuest;
        EventManager.Instance.questEvent.OnAdvanceQuest -= AdvanceQuest;
        EventManager.Instance.questEvent.OnFinishQuest -= FinishQuest;
        EventManager.Instance.questEvent.OnQuestStepStateChange -= QuestStepStateChange;
        EventManager.Instance.questEvent.OnGetQuestMapToPropertyUI -= SetQuestMap;
        EventManager.Instance.enimiesEvent.OnEnimyDie -= EnimyDie;
    }
    private void EnimyDie(NpcCell obj)
    {
        kiiEnimyCount++;
    }
    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            EventManager.Instance.questEvent.QuestStateChange(quest);
        }
        //这只是为页面提供任务信息而传递的map。只读。。
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
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(id,QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        // move on to the next step
        quest.MoveToNextStep();

        // if there are more steps, instantiate the next one
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        // if there are no more steps, then we've finished all of them for this quest
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        Reward(id);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }
    private void Reward(string id)
    {
        InventoryManager.Instance.AddItem(InventoryManager.Instance.GetItemById(GetQuestById(id).info.iteamId));
    }
    private void QuestStepStateChange(string questId, int currentQuestStepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(questId);
        quest.StoryQuestStepState(currentQuestStepIndex,questStepState);
        ChangeQuestState(questId,quest.state);
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
    public void SetQuestMap()
    {
        PropertyManuCtrl.instance.questMap_ReadOnly = questMap;
    }
    public void LoadGame(GameData gameData)
    {
        foreach (QuestData questData in gameData.questDatas)
        {
            questMap[questData.questId].LoadQuestData(questData.questState,questData.currentQuestStepIndex,questData.questStepStates);
            if (questMap[questData.questId].state.Equals(QuestState.IN_PROGRESS))
            {
                questMap[questData.questId].InstantiateCurrentQuestStep(this.transform);
            }
        }
    }

    public void SaveGame(GameData gameData)
    {
        bool flag = false;
        foreach (Quest quest in questMap.Values)
        {
            flag = false;
            QuestData questData = quest.GetQuestData();
            for (int i = 0; i < gameData.questDatas.Count; i++)
            {
                if (questData.questId.Equals(gameData.questDatas[i].questId))
                {
                    gameData.questDatas[i] = questData;
                    flag = true;
                }
            }
            if (!flag)
            {
                gameData.questDatas.Add(questData);
            }
        }
    }
}
