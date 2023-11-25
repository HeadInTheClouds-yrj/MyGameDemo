using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    private int questStepIndex;
    private string questStepState;
    public void InitializeQuestStep(string questId,int questStepIndex,string questStepState)
    {
        this.questId = questId;
        this.questStepIndex = questStepIndex;
        if (questStepState != null && questStepState != "")
        {
            SetStepState(questStepState);
        }
    }
    public void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            EventManager.Instance.questEvent.AdvanceQuest(questId);
            Destroy(gameObject);
        }
    }
    public string GetCurrentQuestId()
    {
        return questId;
    }
    public int GetCurrentQuestStepIndex()
    {
        return questStepIndex;
    }
    public void ChangeStepState(string newState)
    {
        EventManager.Instance.questEvent.QuestStepStateChange(questId,questStepIndex,new QuestStepState(newState));
    }
    protected abstract void SetStepState(string newState);
}
