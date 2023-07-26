using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadSenceControl : MonoBehaviour
{
    public static LoadSenceControl Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    public IEnumerator OnLoadSenceEnd(int sceneIndex,UnityAction action)
    {
        StartCoroutine(Fuction(action));
        yield return new WaitForSeconds(0.1f) ;
        SceneManager.LoadScene(sceneIndex,LoadSceneMode.Single);
    }
    public IEnumerator Fuction(UnityAction action)
    {
        yield return new WaitForSeconds(0.2f);
        action();
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
