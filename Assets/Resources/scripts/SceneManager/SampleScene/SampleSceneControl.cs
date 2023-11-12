using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleSceneControl : MonoBehaviour, IDataPersistence
{
    public void LoadGame(GameData gameData)
    {
        PlayerManager.instance.playerData.scenceIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void SaveGame(GameData gameData)
    {
        
    }
}
