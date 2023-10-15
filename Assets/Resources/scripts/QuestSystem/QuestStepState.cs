using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStepState
{
    public string stepState;
    public QuestStepState()
    {
        stepState = "";
    }
    public QuestStepState(string stepState)
    {
        this.stepState = stepState;
    }
}
