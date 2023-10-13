using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;
    
    public Quest(QuestInfoSO infoSO)
    {
        this.info = infoSO;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    //当前任务是否存在
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
            questStep.InitializeQuestStep(info.id);
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
            Debug.LogWarning("任务不存在\n"+"当前任务索引："+currentQuestStepIndex);
        }
        return questStepPrefab;
    }
}
