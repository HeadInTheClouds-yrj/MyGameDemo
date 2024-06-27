using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour,IDataPersistence
{
    private Dictionary<string, Quest> questMap;
    [Header("test")]
    [SerializeField]
    private int kiiEnimyCount = 0;
    private void Awake()
    {
        questMap = CreateQuestMap();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += UpdateAllQuestsState;
        EventManager.Instance.questEvent.OnStartQuest += StartQuest;
        EventManager.Instance.questEvent.OnAdvanceQuest += AdvanceQuest;
        EventManager.Instance.questEvent.OnFinishQuest += FinishQuest;
        EventManager.Instance.questEvent.OnQuestStepStateChange += QuestStepStateChange;
        EventManager.Instance.questEvent.OnGetQuestMapToPropertyUI += SetQuestMap;
        EventManager.Instance.enimiesEvent.OnEnimyDie += EnimyDie;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= UpdateAllQuestsState;
        EventManager.Instance.questEvent.OnStartQuest -= StartQuest;
        EventManager.Instance.questEvent.OnAdvanceQuest -= AdvanceQuest;
        EventManager.Instance.questEvent.OnFinishQuest -= FinishQuest;
        EventManager.Instance.questEvent.OnQuestStepStateChange -= QuestStepStateChange;
        EventManager.Instance.questEvent.OnGetQuestMapToPropertyUI -= SetQuestMap;
        EventManager.Instance.enimiesEvent.OnEnimyDie -= EnimyDie;
    }
    private void UpdateAllQuestsState(Scene scene, LoadSceneMode loadSceneMode)
    {
        foreach (Quest quest in questMap.Values)
        {
            EventManager.Instance.questEvent.QuestStepStateChange(quest.info.id, quest.GetCurrentQuestStepIndex(), quest.GetCurrentQuestStepState());
        }
    }
    private void EnimyDie(NpcCell obj)
    {
        kiiEnimyCount++;
    }
    private void Start()
    {
        //foreach (Quest quest in questMap.Values)
        //{
        //    EventManager.Instance.questEvent.QuestStateChange(quest);
        //}
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventManager.Instance.enimiesEvent.EnimyDie(null);
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
        quest.StoreQuestStepState(currentQuestStepIndex,questStepState);
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
    private bool IsExistsInstantiateQuestStep(QuestData questData)
    {
        QuestStep[] questSteps = GetComponentsInChildren<QuestStep>();
        Dictionary<string, int> currentInstantiates = new Dictionary<string, int>();
        foreach (QuestStep step in questSteps)
        {
            currentInstantiates.Add(step.GetCurrentQuestId(), step.GetCurrentQuestStepIndex());
        }
        if (currentInstantiates.ContainsKey(questData.questId))
        {
            if (currentInstantiates[questData.questId] == questData.currentQuestStepIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public void SetQuestMap()
    {
        PropertyManuCtrl.instance.questMap_ReadOnly = questMap;
    }
    public void LoadGame(GameData gameData)
    {
        kiiEnimyCount = gameData.datas[0].killEnimiesCont;
        foreach (QuestData questData in gameData.datas[0].questDatas)
        {
            questMap[questData.questId].LoadQuestData(questData.questState,questData.currentQuestStepIndex,questData.questStepStates);
            if (questMap[questData.questId].state.Equals(QuestState.IN_PROGRESS)&&!IsExistsInstantiateQuestStep(questData))
            {
                questMap[questData.questId].InstantiateCurrentQuestStep(this.transform);
            }

            EventManager.Instance.questEvent.QuestStateChange(GetQuestById(questData.questId));
            EventManager.Instance.questEvent.QuestStepStateChange(GetQuestById(questData.questId).info.id, questData.currentQuestStepIndex, questData.questStepStates[questData.currentQuestStepIndex]);
        }


    }

    public void SaveGame(GameData gameData)
    {
        bool flag = false;
        if (gameData.datas[0] == null)
        {
            gameData.datas[0] = new Data();
        }
        foreach (Quest quest in questMap.Values)
        {
            flag = false;
            QuestData questData = quest.GetQuestData();
            
            for (int i = 0; i < gameData.datas[0].questDatas.Count; i++)
            {
                if (questData.questId.Equals(gameData.datas[0].questDatas[i].questId))
                {
                    gameData.datas[0].questDatas[i] = questData;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                gameData.datas[0].questDatas.Add(questData);
            }
        }
        gameData.datas[0].killEnimiesCont = kiiEnimyCount;
    }
}
