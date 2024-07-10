using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDisplayUICtrl : MonoBehaviour
{
    private Quest quest_ReadOnly;
    private TMP_Text questDisplayName;
    private TMP_Text questAavanceState;
    public void SetQuest(Quest quest_ReadOnly)
    {
        this.quest_ReadOnly = quest_ReadOnly;
    }
    private void Initialize()
    {
        questDisplayName = UIManager.instance.GetUI("Property", "QuestDisplayName_N").GetComponent<TMP_Text>();
        questAavanceState = UIManager.instance.GetUI("Property", "AdvanceState_N").GetComponent<TMP_Text>();
    }
    public void ShowQuestState()
    {
        Initialize();
        string questState = "";
        switch (quest_ReadOnly.state)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                questState = "未满足任务开启条件";
                break;
            case QuestState.CAN_START:
                questState = "任务可以开启";
                break;
            case QuestState.IN_PROGRESS:
                questState = "任务正在进行中";
                break;
            case QuestState.CAN_FINISH:
                questState = "可以完成";
                break;
            case QuestState.FINISHED:
                questState = "已完成";
                break;
            default:
                break;
        }
        questDisplayName.text = quest_ReadOnly.info.displayName+"\n"+"任务状态：  "+ questState;

        if (quest_ReadOnly.CurrentStepExists())
        {
            questAavanceState.text = quest_ReadOnly.GetQuestData().questStepStates[quest_ReadOnly.GetQuestData().currentQuestStepIndex].stepState;
        }
        else
        {
            questAavanceState.text = quest_ReadOnly.GetQuestData().questStepStates[quest_ReadOnly.GetQuestData().currentQuestStepIndex-1].stepState;
        }
        
    }
}
