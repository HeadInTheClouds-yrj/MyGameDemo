using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public Data[] datas;
    public SerializableDictionary<int, EnemiesDataGroup> enimies;

    public GameData()
    {
        datas = new Data[20];
        enimies = new SerializableDictionary<int, EnemiesDataGroup>();
    }
}
