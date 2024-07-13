using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillInfoSO", menuName ="ScriptableObjects/SkillInfoSO",order =3)]
public class SkillInfoSO : ScriptableObject
{
    public string itemId;
    public string id;
    public string skillName;
    public string skillDisplayName;
    public GameObject skillEffectPrefab;
    public Sprite skillInItemIcon;
    public Sprite skillInBattleIcon;
    [Header("神通品级：天 地 人 玄 黄(0，1，2，3，4)")]
    public int SkillRate;
    public List<string> skillEffectText;
    private void OnValidate()
    {
        #if UNITY_EDITOR
            itemId = this.id;
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
