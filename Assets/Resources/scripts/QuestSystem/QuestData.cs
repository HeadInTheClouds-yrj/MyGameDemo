using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestData
{
    public string questId;
    public int currentQuestStepIndex;
    public QuestStepState[] questStepStates;
    public QuestState questState;
    public QuestData(string questId, int currentQuestStepIndex, QuestStepState[] questStepStates, QuestState questState)
    {
        this.questId = questId;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;
        this.questState = questState;
    }
}
