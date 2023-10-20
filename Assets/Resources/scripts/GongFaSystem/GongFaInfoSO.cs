using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GongFaInfoSO",menuName = "ScriptableObjects/GongFaInfoSO",order = 1)]
public class GongFaInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; set; }
    [Header("功法名字")]
    public string gongFaName;
    [Header("功法简介")]
    public string gongFaDisplayName;
    [Header("功法品级：天 地 人 玄 黄\n黄：123\n玄：456\n人：789\n地：10,11,12\n天：13,14,15")]
    public int gongFaLevel;
    public Sprite gongFaInItemIcon;
    public Sprite gongFaInBattleIcon;
    public int gongFaXiuLianSpeek;
    public GongFaTypes gongFaTypes;
    [Header("功法实体功能预制体")]
    public GameObject gongFaPrefab;
    private void OnValidate()
    {
        #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
