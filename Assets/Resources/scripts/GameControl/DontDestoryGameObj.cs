using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryGameObj : MonoBehaviour,IDataPersistence
{
    [SerializeField] private Canvas canvas;

    public void LoadGame(GameData gameData)
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
    }

    public void SaveGame(GameData gameData)
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
