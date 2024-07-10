using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnimyQuestStep : QuestStep
{
    [Header("test")]
    [SerializeField]
    private int killEnimiesCount = 0;
    private int KillToComplete = 3;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            NpcManager.instance.factoryNpc();
        }
    }
    private void OnEnable()
    {
        EventManager.Instance.enimiesEvent.OnEnimyDie += Dead;
    }
    private void OnDisable()
    {
        EventManager.Instance.enimiesEvent.OnEnimyDie -= Dead;
    }
    private void Dead(NpcCell cell)
    {
        if (killEnimiesCount < KillToComplete)
        {
            killEnimiesCount++;
            UpdateStepState();
            if (killEnimiesCount >=KillToComplete)
            {
                FinishQuestStep();
            }
        }
    }
    private void UpdateStepState()
    {
        string state = killEnimiesCount.ToString();
        ChangeStepState(state);
    }
    protected override void SetStepState(string newState)
    {
        killEnimiesCount = System.Int32.Parse(newState);
        UpdateStepState();
    }
}
