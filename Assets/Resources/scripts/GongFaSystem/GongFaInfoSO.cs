using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GongFaInfoSO",menuName = "ScriptableObjects/GongFaInfoSO",order = 1)]
public class GongFaInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; set; }
    [Header("��������")]
    public string gongFaName;
    [Header("�������")]
    public string gongFaDisplayName;
    [Header("����Ʒ������ �� �� �� ��\n�ƣ�123\n����456\n�ˣ�789\n�أ�10,11,12\n�죺13,14,15")]
    public int gongFaLevel;
    public Sprite gongFaInItemIcon;
    public Sprite gongFaInBattleIcon;
    public int gongFaXiuLianSpeek;
    public GongFaTypes gongFaTypes;
    [Header("����ʵ�幦��Ԥ����")]
    public GameObject gongFaPrefab;
    private void OnValidate()
    {
        #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
