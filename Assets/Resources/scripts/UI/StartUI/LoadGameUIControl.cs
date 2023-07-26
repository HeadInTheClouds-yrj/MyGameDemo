using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameUIControl : MonoBehaviour
{
    public void ManuControl()
    {
        DataPersistenceManager.instance.ChangeDataSourceName(GetComponentInChildren<TMP_Text>().text);
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
