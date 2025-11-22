using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStepState
{
    public string stepState;
    public bool stepStateBool;
    public QuestStepState()
    {
        stepState = "";
        stepStateBool = false;
    }
    public QuestStepState(string stepState,bool stepStateBool)
    {
        this.stepState = stepState;
        this.stepStateBool = stepStateBool;
    }
}
