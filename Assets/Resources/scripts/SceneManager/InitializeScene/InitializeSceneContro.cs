using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeSceneContro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.instance.playerData.scenceIndex == 0|| PlayerManager.instance.playerData.scenceIndex == 4)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync(PlayerManager.instance.playerData.scenceIndex, LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
