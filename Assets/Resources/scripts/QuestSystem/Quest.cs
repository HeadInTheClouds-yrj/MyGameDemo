using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] stepStates;
    public Quest(QuestInfoSO infoSO)
    {
        this.info = infoSO;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        currentQuestStepIndex = 0;
        stepStates = new QuestStepState[infoSO.questStepPrefabs.Length];
        for (int i = 0; i < stepStates.Length; i++)
        {
            stepStates[i] = new QuestStepState();
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    //��ǰ�����Ƿ����
    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }
    public void InstantiateCurrentQuestStep(Transform parent)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parent).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, stepStates[currentQuestStepIndex].stepState);
        }
    }
    public GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("���񲻴���\n"+"��ǰ����������"+currentQuestStepIndex);
        }
        return questStepPrefab;
    }
    public void StoryQuestStepState(int stepIndex,QuestStepState questStepState)
    {
        if (stepIndex<stepStates.Length)
        {
            stepStates[stepIndex].stepState = questStepState.stepState;
        }
        else
        {
            Debug.LogWarning("�������������������Χ"+stepIndex);
        }
    }
    public QuestData GetQuestData()
    {
        return new QuestData(info.id, currentQuestStepIndex, stepStates, state);
    }
    public void LoadQuestData(QuestState state,int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.state = state;
        this.currentQuestStepIndex= currentQuestStepIndex;
        this.stepStates = questStepStates;
    }
}
