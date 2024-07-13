using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GongFaInfoSO",menuName = "ScriptableObjects/GongFaInfoSO",order = 1)]
public class GongFaInfoSO : ScriptableObject
{
    [Header("�ڱ����е�ID")]
    [field: SerializeField]
    public string itemId;
    [field: SerializeField] public string id { get; set; }
    [Header("��������")]
    public string gongFaName;
    [Header("�������")]
    public string gongFaDisplayName;
    [Header("����Ʒ������ �� �� �� ��\n�ƣ�123\n����456\n�ˣ�789\n�أ�10,11,12\n�죺13,14,15")]
    public int gongFaRate;
    [Header("�������ȼ�")]
    public int gongFaMaxLevel;
    public Sprite gongFaInItemIcon;
    public Sprite gongFaInBattleIcon;
    public int gongFaXiuLianSpeek;
    public GongFaTypes gongFaTypes;
    [Header("����ʵ�幦��Ԥ����")]
    public GameObject gongFaPrefab;
    public List<string> gongFaEffectText;
    private void OnValidate()
    {
        #if UNITY_EDITOR
            itemId = this.id;
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
