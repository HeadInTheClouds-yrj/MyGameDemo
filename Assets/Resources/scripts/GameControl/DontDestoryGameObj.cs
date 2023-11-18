using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestoryGameObj : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public void InitallCanvas(Scene scene, LoadSceneMode mode)
    {
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
        }
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += InitallCanvas;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= InitallCanvas;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
