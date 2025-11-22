using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillEnimyQuestStep : QuestStep
{
    [Header("test")]
    [SerializeField]
    private int killEnimiesCount = 0;
    private int KillToComplete = 3;
    private bool isSpawned = false;
    private void Start()
    {
        if (!isSpawned)
        {
            for (int i = 0; i < KillToComplete - killEnimiesCount; i++)
            {
                NpcManager.instance.FactoryNpc().GetComponent<NpcCell>();
            }
            isSpawned = true;
            UpdateStepState();
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
        ChangeStepState(state,isSpawned);
    }
    protected override void SetStepState(string newState,bool boolState)
    {
        killEnimiesCount = System.Int32.Parse(newState);
        isSpawned= boolState;
        UpdateStepState();
    }
}
