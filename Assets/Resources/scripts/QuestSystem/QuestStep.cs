using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
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
}
