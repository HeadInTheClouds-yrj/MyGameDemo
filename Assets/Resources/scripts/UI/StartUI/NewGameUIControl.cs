using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NewGameUIControl : UIBase
{
    public void LoadScenes()
    {
        StartCoroutine(NewCharacterLoadScene());
    }
    public IEnumerator NewCharacterLoadScene()
    {
        
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    // Start is called before the first frame update
    void Start()
    {
        addlicen("Beastmaster_N", LoadScenes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
