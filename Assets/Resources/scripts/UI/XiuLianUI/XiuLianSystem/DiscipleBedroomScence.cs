using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscipleBedroomScence : MonoBehaviour,IDataPersistence
{
    public void LeftRoomToZongMen()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
    }

    public void LoadGame(GameData gameData)
    {
        PlayerManager.instance.playerData.scenceIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void SaveGame(GameData gameData)
    {
        
    }
}
