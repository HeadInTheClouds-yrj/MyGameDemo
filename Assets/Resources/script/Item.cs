using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Create New Item")]
public class Item : ScriptableObject
{
    public string id;
    public string itemname;
    public int value;
    public Sprite icon;
}
