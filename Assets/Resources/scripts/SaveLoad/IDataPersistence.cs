using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadGame(GameData gameData);
    void SaveGame(GameData gameData);
}
