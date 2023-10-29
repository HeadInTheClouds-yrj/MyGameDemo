using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscipleBedroomScence : MonoBehaviour
{
    public void LeftRoomToZongMen()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }
}
