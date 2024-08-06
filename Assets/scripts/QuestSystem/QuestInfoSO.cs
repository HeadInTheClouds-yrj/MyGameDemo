using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO",menuName = "ScriptableObjects/QuestInfoSO",order =1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; set; }

    [Header("任务简介")] 
    public string displayName;
    [Header("任务类型（主/支线）")]
    public QuestType questType;
    [Header("任务启动先决条件")] 
    public int levelRequirement;
    public int killEnimiesCont;
    public QuestInfoSO[] questPrerequisites;

    [Header("实例化的任务步骤预制体")] 
    public GameObject[] questStepPrefabs;

    [Header("奖励")]
    public int skillId;
    public string iteamId;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
