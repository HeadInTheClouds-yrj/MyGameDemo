using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableData", menuName = "ScriptableObjects/SaveLoadInfo", order = 1)]
public class ScriptableData : ScriptableObject
{
    public Data data;
    public List<QuestData> questDatas;
}
