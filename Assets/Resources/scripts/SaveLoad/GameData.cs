using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public Data[] datas;
    public List<QuestData> questDatas;
    public GameData()
    {
        datas = new Data[20];
        questDatas = new List<QuestData>();
    }
}
