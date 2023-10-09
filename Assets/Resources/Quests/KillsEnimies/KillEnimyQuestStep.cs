using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnimyQuestStep : QuestStep
{
    private int killEnimiesCont = 0;
    private int KillToComplete = 2;
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
        if (killEnimiesCont < KillToComplete)
        {
            killEnimiesCont++;
            if (killEnimiesCont>=KillToComplete)
            {
                FinishQuestStep();
            }
        }
    }
}
