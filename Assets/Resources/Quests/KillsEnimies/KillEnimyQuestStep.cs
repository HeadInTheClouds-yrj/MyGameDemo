using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnimyQuestStep : QuestStep
{
    private int killEnimiesCont = 0;
    private int KillToComplete = 4;
    private void OnEnable()
    {
        EventManager.Instance.enimiesEvent.CountWhenTheEnemyDies += Dead;
    }
    private void OnDisable()
    {
        EventManager.Instance.enimiesEvent.CountWhenTheEnemyDies -= Dead;
    }
    private void Dead()
    {
        if (killEnimiesCont < KillToComplete)
        {
            killEnimiesCont++;
        }
        else
        {
            FinishQuestStep();
        }
    }
}
