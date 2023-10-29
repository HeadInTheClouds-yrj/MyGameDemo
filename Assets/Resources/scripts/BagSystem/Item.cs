using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Create New Item")]
public class Item : ScriptableObject
{
    public enum Itemkinde
    {
        RangedWeapen,
        MeleeWeapen,
        Point,
        Book,
        Food,
        Skill,
        GongFa
    }
    public string id;
    public string itemname;
    public string prefeberPath;
    public int value;
    public int itemCont;
    public Sprite Bagicon;
    public Sprite icon;
    public Itemkinde itemkinde;
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
