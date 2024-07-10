using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemsQuestStep : QuestStep
{
    private int pickUpItem = 0;
    protected override void SetStepState(string newState)
    {
        pickUpItem = System.Int32.Parse(newState);
    }
    private void OnEnable()
    {
        EventManager.Instance.questEvent.OnPickUpItem += PickUpItemCount;
    }

    private void PickUpItemCount()
    {
        pickUpItem++;
        ChangeStepState(pickUpItem.ToString());
        if (pickUpItem >=2)
        {
            FinishQuestStep();
        }
    }
}
