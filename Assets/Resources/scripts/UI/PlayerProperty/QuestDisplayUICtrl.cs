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
        questDisplayName.text = quest_ReadOnly.info.displayName+"\n"+"ÈÎÎñ×´Ì¬£º  "+ quest_ReadOnly.state;
        
        questAavanceState.text = quest_ReadOnly.GetQuestData().questStepStates[quest_ReadOnly.GetQuestData().currentQuestStepIndex].stepState;
    }
}
