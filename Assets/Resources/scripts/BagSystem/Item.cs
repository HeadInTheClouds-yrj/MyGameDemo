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
        Food
    }
    public string id;
    public string itemname;
    public string prefeberPath;
    public int value;
    public Sprite Bagicon;
    public Sprite icon;
    public Itemkinde itemkinde;
}
