using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    private bool plyerIsNear = false;
    [SerializeField] private QuestInfoSO questInfoSO;
    [Header("任务npc是否拥有开始任务和结束任务功能")]
    [SerializeField] private bool startQuest = false;
    [SerializeField] private bool finishQuest = false;
    private string questId;
    private QuestState currentQuestState;
    private QuestIcon questIcon;
    private void Awake()
    {
        questId = questInfoSO.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }
    private void OnEnable()
    {
        EventManager.Instance.questEvent.OnQuestStateChange += QuestStateChange;
        EventManager.Instance.inputEvent.onSubmitPressed += SubmitPressed;
    }
    private void OnDisable()
    {
        EventManager.Instance.questEvent.OnQuestStateChange -= QuestStateChange;
        EventManager.Instance.inputEvent.onSubmitPressed += SubmitPressed;
    }

    private void SubmitPressed()
    {
        if (!plyerIsNear)
        {
            return;
        }
        if (startQuest&&currentQuestState.Equals(QuestState.CAN_START))
        {
            EventManager.Instance.questEvent.StartQuest(questId);
        }
        if (finishQuest&&currentQuestState.Equals(QuestState.CAN_FINISH))
        {
            EventManager.Instance.questEvent.FinishQuest(questId);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState,startQuest,finishQuest);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plyerIsNear = true;
            Debug.Log("near");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            plyerIsNear = false;
        }
    }
}
