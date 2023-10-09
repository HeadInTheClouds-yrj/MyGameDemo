using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    public void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            Destroy(gameObject);
        }
    }
}
