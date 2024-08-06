using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO",menuName = "ScriptableObjects/QuestInfoSO",order =1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; set; }

    [Header("������")] 
    public string displayName;
    [Header("�������ͣ���/֧�ߣ�")]
    public QuestType questType;
    [Header("���������Ⱦ�����")] 
    public int levelRequirement;
    public int killEnimiesCont;
    public QuestInfoSO[] questPrerequisites;

    [Header("ʵ������������Ԥ����")] 
    public GameObject[] questStepPrefabs;

    [Header("����")]
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
